using Microsoft.EntityFrameworkCore;
using VacunassistBackend.Entities;
using VacunnasistBackend.Models;
using VacunnasistBackend.Models.Filters;

namespace VacunnasistBackend.Services
{
    public interface IAppliedVaccineService
    {
        AppliedVaccine[] GetAll(AppliedVaccinesFilterRequest filter);
        AppliedVaccine Get(int id);
        bool New(NewAppliedVaccineRequest model);

    }
    public class AppliedVaccineService : IAppliedVaccineService
    {
        private DataContext _context;

        public AppliedVaccineService(DataContext context)
        {
            _context = context;
        }

        public AppliedVaccine Get(int id)
        {
            return _context.AppliedVaccines.Include(x => x.LocalBatchVaccine)
                .ThenInclude(x => x.BatchVaccine)
                .ThenInclude(x => x.DevelopedVaccine)
                .Include(x => x.User)
                .First(x => x.Id == id);
        }

        public AppliedVaccine[] GetAll(AppliedVaccinesFilterRequest filter)
        {
            var allDevVaccines = _context.DevelopedVaccines.ToArray();
            if(filter.DevelopedVaccineId.HasValue)
                allDevVaccines = allDevVaccines.Where(x => x.Id == filter.DevelopedVaccineId).ToArray();
            else if(filter.VaccineId.HasValue)
                allDevVaccines = allDevVaccines.Where(x => x.Vaccine.Id == filter.VaccineId.Value).ToArray();


            var query = _context.AppliedVaccines.Include(x => x.LocalBatchVaccine).ThenInclude(x => x.BatchVaccine).Include(x => x.User).AsQueryable();
            if (!string.IsNullOrEmpty(filter.BatchNumber))
                query = query.Where(x => x.LocalBatchVaccine.BatchVaccine.BatchNumber.ToUpper() == filter.BatchNumber.ToUpper());
            if (!string.IsNullOrEmpty(filter.Province))
                query = query.Where(x => x.LocalBatchVaccine.Province == filter.Province);
            if (filter.AppliedById.HasValue)
                query = query.Where(x => x.User.Id == filter.AppliedById);
            if (filter.AppliedDate.HasValue)
                query = query.Where(x => x.AppliedDate == filter.AppliedDate);

            query = query.Where(x => allDevVaccines.Contains(x.LocalBatchVaccine.BatchVaccine.DevelopedVaccine));

            return query.ToArray();
        }

        public bool New(NewAppliedVaccineRequest model)
        {
            throw new NotImplementedException();
        }
    }
}
