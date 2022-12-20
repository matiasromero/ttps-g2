using Microsoft.EntityFrameworkCore;
using Quartz;
using Dapper;
using System.Data;
using Microsoft.Data.SqlClient;
using VacunassistBackend.Services;

namespace VacunassistBackend.Jobs
{
    public class PersistStockHistoryJob : IJob
    {
        private readonly DataContext _context;
        private readonly IDepartmentsService _departmentsService;
        private readonly IConfiguration _configuration;

        public PersistStockHistoryJob(DataContext context, IConfiguration configuration)
        {
            this._context = context;
            this._configuration = configuration;
        }

        public Task Execute(IJobExecutionContext context)
        {
            var year = DateTime.Now.Year;
            var month = DateTime.Now.Month;

            var localBatchVaccines = _context.LocalBatchVaccines
                .Include(x => x.BatchVaccine).ThenInclude(x => x.DevelopedVaccine)
                .Where(x => x.DistributionDate.Year == year && x.DistributionDate.Month == month)
                .ToArray();
            var appliedVaccines = _context.AppliedVaccines
                .Include(x => x.LocalBatchVaccine).ThenInclude(x => x.BatchVaccine).ThenInclude(x => x.DevelopedVaccine)
                .Include(x => x.Patient).ThenInclude(x => x.Department).ThenInclude(x => x.Province)
                .Where(x => x.AppliedDate.Year == year && x.AppliedDate.Month == month)
                .ToArray();

            using (IDbConnection db = new SqlConnection(_configuration.GetConnectionString("DatawarehouseDefaultConnection")))
            {
                db.Open();

                var list = new List<StockHistoryModel>();

                foreach (var batch in localBatchVaccines)
                {
                    var model = new StockHistoryModel();
                    model.Province = batch.Province;
                    model.DevelopedVaccine = batch.BatchVaccine.DevelopedVaccine.Vaccine.Name + " - " + batch.BatchVaccine.DevelopedVaccine.Name;
                    model.Year = batch.DistributionDate.Year;
                    model.Month = batch.DistributionDate.Month;

                    model.AppliedQuantity = appliedVaccines.Count(x => x.LocalBatchVaccineId == batch.Id);
                    model.DistributedQuantity = batch.Quantity;
                    model.OverdueQuantity = batch.OverdueQuantity;
                    model.CurrentStock = batch.RemainingQuantity;

                    list.Add(model);
                }

                var groupedList = list.GroupBy(x => new { x.Province, x.DevelopedVaccine, x.Year, x.Month }).ToArray();
                foreach (var group in groupedList)
                {
                    var key = group.Key;
                    CreateOrUpdateYearMonthEntry(db, key.Province, key.DevelopedVaccine, key.Year, key.Month, group.Sum(x => x.DistributedQuantity), group.Sum(x => x.CurrentStock), group.Sum(x => x.OverdueQuantity), group.Sum(x => x.AppliedQuantity));
                }
            }
            return Task.CompletedTask;
        }

        public void CreateOrUpdateYearMonthEntry(IDbConnection db, string province, string developedVaccine, int year, int month, int distributedQuantity, int currentStock, int overdueQuantity, int appliedQuantity)
        {
            var exist = db.ExecuteScalar<bool>("SELECT count(1) FROM StockHistoryDetails WHERE province = @province AND developedVaccine = @developedVaccine AND year = @year AND month = @month",
                    new { province, developedVaccine, year, month });
            if (!exist)
            {
                db.Execute("insert into StockHistoryDetails values (@province, @developedVaccine, @year, @month, @distributedQuantity, @appliedQuantity, @currentStock, @overdueQuantity)",
                new { province, developedVaccine, year, month, distributedQuantity, appliedQuantity, currentStock, overdueQuantity });
            }
            else
            {
                db.Execute("update StockHistoryDetails set distributedQuantity = @distributedQuantity, appliedQuantity = @appliedQuantity, currentStock = @currentStock, overdueQuantity = @overdueQuantity WHERE province = @province AND developedVaccine = @developedVaccine AND year = @year AND month = @month",
                new { province, developedVaccine, year, month, distributedQuantity, appliedQuantity, currentStock, overdueQuantity });
            }
        }

        public class StockHistoryModel
        {
            public string Province { get; set; }
            public string DevelopedVaccine { get; set; }
            public int Year { get; set; }
            public int Month { get; set; }

            public int DistributedQuantity { get; set; }
            public int AppliedQuantity { get; set; }
            public int CurrentStock { get; set; }
            public int OverdueQuantity { get; set; }
        }
    }
}