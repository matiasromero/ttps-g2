using Microsoft.EntityFrameworkCore;
using VacunassistBackend.Entities;
using VacunassistBackend.Models;
using VacunassistBackend.Models.Filters;

namespace VacunassistBackend.Services
{
    public interface ILocalBatchVaccinesService
    {
        LocalBatchVaccine[] GetAll(LocalBatchVaccinesFilterRequest filter);
        LocalBatchVaccine Get(int id);
        bool New(NewLocalBatchVaccineRequest model);
    }

    public class LocalBatchVaccinesService : ILocalBatchVaccinesService
    {
        private DataContext _context;

        public LocalBatchVaccinesService(DataContext context)
        {
            this._context = context;
        }

        public LocalBatchVaccine[] GetAll(LocalBatchVaccinesFilterRequest filter)
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

            var query = _context.LocalBatchVaccines.Include(x => x.BatchVaccine).AsQueryable();

            if (string.IsNullOrEmpty(filter.BatchNumber) == false)
                query = query.Where(x => x.BatchVaccine.BatchNumber.ToUpper() == filter.BatchNumber.ToUpper());

            if (string.IsNullOrEmpty(filter.Province) == false)
                query = query.Where(x => x.Province == filter.Province);

            query = query.Where(x => allDevVaccines.Contains(x.BatchVaccine.DevelopedVaccine));

            return query.ToArray();
        }

        public bool New(NewLocalBatchVaccineRequest model)
        {
            return true;
        }

        public LocalBatchVaccine Get(int id)
        {
            return _context.LocalBatchVaccines.Include(x => x.BatchVaccine).ThenInclude(x => x.DevelopedVaccine).First(x => x.Id == id);
        }
    }
}