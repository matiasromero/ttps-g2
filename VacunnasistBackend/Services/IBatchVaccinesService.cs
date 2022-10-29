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
        bool New(NewBatchVaccineRequest model);
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

        public bool New(NewBatchVaccineRequest model)
        {
            return true;
            // validate
            // if (_context.DevelopedVaccines.Any(x => x.Name == model.Name))
            //     throw new ApplicationException("Nombre de vacuna desarrollada '" + model.Name + "' en uso");

            // try
            // {
            //     var vaccine = new DevelopedVaccine()
            //     {
            //         Name = model.Name,
            //         DaysToDelivery = model.DaysToDelivery,
            //         Vaccine = Vaccines.Get(model.VaccineId)
            //     };

            //     // save vaccine
            //     _context.DevelopedVaccines.Add(vaccine);
            //     _context.SaveChanges();
            //     return true;
            // }
            // catch (Exception)
            // {
            //     return false;
            // }
        }

        public BatchVaccine Get(int id)
        {
            return _context.BatchVaccines.Include(x => x.DevelopedVaccine).Include(x => x.Distributions).First(x => x.Id == id);
        }

        public bool Exist(int id)
        {
            return _context.BatchVaccines.Any(x => x.Id == id);
        }
    }
}