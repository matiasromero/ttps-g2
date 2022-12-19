using Microsoft.EntityFrameworkCore;
using Quartz;
using Dapper;
using System.Data;
using Microsoft.Data.SqlClient;

namespace VacunassistBackend.Jobs
{
    public class SyncronizeDataJob : IJob
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;
        public SyncronizeDataJob(DataContext context, IConfiguration configuration)
        {
            this._context = context;
            this._configuration = configuration;
        }
        public Task Execute(IJobExecutionContext context)
        {
            var batchVaccines = _context.BatchVaccines.Where(x => x.Synchronized == false).ToArray();
            var localBatchVaccines = _context.LocalBatchVaccines.Where(x => x.Synchronized == false).ToArray();
            var appliedVaccines = _context.AppliedVaccines.Where(x => x.Synchronized == false).ToArray();

            using (IDbConnection db = new SqlConnection(_configuration.GetConnectionString("DatawarehouseDefaultConnection")))
            {
                db.Open();

                foreach (var batch in batchVaccines)
                {
                    // Sync
                    //batch.Synchronized = true;
                }

                foreach (var localBatch in localBatchVaccines)
                {
                    // Sync
                    //localBatch.Synchronized = true;
                }

                foreach (var appliedVaccine in appliedVaccines)
                {
                    // Sync
                    var idFecha = db.Query<int?>("SELECT idFecha FROM DFecha WHERE year = @year AND mes = @mes AND dia = @dia",
                    new { year = (int?)appliedVaccine.AppliedDate.Year, mes = (int?)appliedVaccine.AppliedDate.Month, dia = (int?)appliedVaccine.AppliedDate.Day })
                    .SingleOrDefault();
                    if (idFecha.HasValue)
                    {

                    }

                    appliedVaccine.Synchronized = true;
                }

            }

            _context.SaveChanges();

            //Write your custom code here
            return Task.CompletedTask;
        }
    }
}