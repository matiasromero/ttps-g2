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
            throw new NotImplementedException();
        }

        public AppliedVaccine[] GetAll(AppliedVaccinesFilterRequest filter)
        {
            throw new NotImplementedException();
            /*var query = _context.AppliedVaccines.AsQueryable();
            if (!string.IsNullOrEmpty(filter.Province))
                query = query.Where(x => x.User.)*/
        }

        public bool New(NewAppliedVaccineRequest model)
        {
            throw new NotImplementedException();
        }
    }
}
