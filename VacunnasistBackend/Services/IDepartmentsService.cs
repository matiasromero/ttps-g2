using Microsoft.EntityFrameworkCore;
using VacunassistBackend.Entities;
using VacunassistBackend.Infrastructure;
using VacunassistBackend.Models;
using VacunassistBackend.Models.Filters;
using VacunassistBackend.Utils;

namespace VacunassistBackend.Services
{
    public interface IDepartmentsService
    {
        Department[] GetAll();
        Department Get(int id);
        Department Get(string name);
        Department GetRandomFromProvince(string province);
    }

    public class DepartmentsService : IDepartmentsService
    {
        private DataContext _context;

        public DepartmentsService(DataContext context)
        {
            this._context = context;
        }

        public Department[] GetAll()
        {
            return _context.Departments.ToArray();
        }

        public Department Get(int id)
        {
            return _context.Departments.First(x => x.Id == id);
        }

        public Department Get(string name)
        {
            return _context.Departments.First(x => x.Name.ToUpper() == name.ToUpper());
        }

        public Department GetRandomFromProvince(string province)
        {
            var departments = _context.Departments.Include(x => x.Province).Where(x => x.Province.Name.ToUpper() == province.ToUpper()).ToArray();
            Random random = new Random();
            int i = random.Next(0, departments.Length);
            return departments[i];
        }
    }
}