using System.Text;
using Microsoft.EntityFrameworkCore;
using VacunassistBackend.Entities;
using VacunassistBackend.Models;
using VacunassistBackend.Models.Filters;

namespace VacunassistBackend.Services
{
    public interface IBatchVaccinesService
    {
        BatchVaccine[] GetAll(BatchVaccinesFilterRequest filter);
        BatchVaccine Get(int id);
        bool Exist(int id);

        string NewDistribution(NewDistributionRequest model);

    }

    public class BatchVaccinesService : IBatchVaccinesService
    {
        private DataContext _context;

        public BatchVaccinesService(DataContext context)
        {
            this._context = context;
        }

        public BatchVaccine[] GetAll(BatchVaccinesFilterRequest filter)
        {
            var allDevVaccines = _context.DevelopedVaccines.ToArray();
            if (filter.DevelopedVaccineId.HasValue)
            {
                allDevVaccines = allDevVaccines.Where(x => x.Id == filter.DevelopedVaccineId.Value).ToArray();
            }
            else if (filter.VaccineId.HasValue)
            {
                allDevVaccines = allDevVaccines.Where(x => x.Vaccine.Id == filter.VaccineId.Value).ToArray();
            }

            var query = _context.BatchVaccines.AsQueryable();

            if (string.IsNullOrEmpty(filter.BatchNumber) == false)
                query = query.Where(x => x.BatchNumber.ToUpper() == filter.BatchNumber.ToUpper());

            query = query.Where(x => allDevVaccines.Contains(x.DevelopedVaccine));

            var result = query.Include(x => x.Distributions).ToArray();

            return result;
        }

        public BatchVaccine Get(int id)
        {
            return _context.BatchVaccines.Include(x => x.DevelopedVaccine).Include(x => x.Distributions).First(x => x.Id == id);
        }

        public bool Exist(int id)
        {
            return _context.BatchVaccines.Any(x => x.Id == id);
        }

        public string NewDistribution(NewDistributionRequest model)
        {
            var devVaccines = _context.DevelopedVaccines.ToArray().Where(x => x.Vaccine.Id == model.VaccineId).ToArray();
            var existingVaccines = _context.BatchVaccines.Where(x => x.Status == BatchStatus.Valid && x.RemainingQuantity > 0 && devVaccines.Contains(x.DevelopedVaccine)).OrderBy(x => x.DueDate).ToArray();

            var message = new StringBuilder();
            var remaining = model.Quantity;
            message.Append("Distribución: ");
            foreach (var batch in existingVaccines)
            {
                if (remaining > 0)
                {
                    var quantity = Math.Min(batch.RemainingQuantity, remaining);
                    remaining -= quantity;
                    batch.RemainingQuantity -= quantity;
                    var newDistribution = new LocalBatchVaccine(quantity, model.Province, batch.Id);
                    _context.LocalBatchVaccines.Add(newDistribution);
                    message.AppendLine(quantity + " del lote # " + batch.BatchNumber + (remaining > 0 ? ", " : ""));
                }
            }
            _context.SaveChanges();

            return message.ToString();
        }
    }
}