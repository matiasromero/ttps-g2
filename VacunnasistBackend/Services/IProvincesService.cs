using Microsoft.EntityFrameworkCore;
using VacunassistBackend.Entities;
using VacunassistBackend.Infrastructure;
using VacunassistBackend.Models;
using VacunassistBackend.Models.Filters;
using VacunassistBackend.Utils;

namespace VacunassistBackend.Services
{
    public interface IProvincesService
    {
        Province[] GetAll();
        Province Get(int id);
        Province Get(string name);
    }

    public class ProvincesService : IProvincesService
    {
        private DataContext _context;

        public ProvincesService(DataContext context)
        {
            this._context = context;
        }

        public Province[] GetAll()
        {
            return _context.Provinces.ToArray();
        }

        public Province Get(int id)
        {
            return _context.Provinces.First(x => x.Id == id);
        }

        public Province Get(string name)
        {
            return _context.Provinces.First(x => x.Name.ToUpper() == name.ToUpper());
        }
    }
}