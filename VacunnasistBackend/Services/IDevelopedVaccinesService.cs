using Microsoft.EntityFrameworkCore;
using VacunassistBackend.Entities;
using VacunassistBackend.Entities.Vaccines;
using VacunassistBackend.Infrastructure;
using VacunassistBackend.Models;
using VacunassistBackend.Models.Filters;

namespace VacunassistBackend.Services
{
    public interface IDevelopedVaccinesService
    {
        DevelopedVaccine[] GetAll(DevelopedVaccinesFilterRequest filter);
        DevelopedVaccine Get(int id);
        bool ExistsApplied(int id);
        void Update(int id, UpdateDevelopedVaccineRequest model);
        bool CanBeDeleted(int id);
        bool Exist(int id);
        bool AlreadyExist(string name, int vaccineId);
        bool New(NewDevelopedVaccineRequest model);
        AppliedVaccine GetApplied(int id);
    }

    public class DevelopedVaccinesService : IDevelopedVaccinesService
    {
        private DataContext _context;

        public DevelopedVaccinesService(DataContext context)
        {
            this._context = context;
        }

        public bool ExistsApplied(int id)
        {
            return _context.AppliedVaccines.Any(x => x.Id == id);
        }

        public DevelopedVaccine[] GetAll(DevelopedVaccinesFilterRequest filter)
        {
            var query = _context.DevelopedVaccines.AsQueryable();
            if (filter.IsActive.HasValue)
                query = query.Where(x => x.IsActive == filter.IsActive);
            if (string.IsNullOrEmpty(filter.Name) == false)
                query = query.Where(x => x.Name.Contains(filter.Name));
            if (filter.VaccineId.HasValue)
                query = query.ToArray().Where(x => x.Vaccine.Id == filter.VaccineId.Value).AsQueryable();
            return query.ToArray();
        }

        public bool New(NewDevelopedVaccineRequest model)
        {
            var vaccines = _context.DevelopedVaccines.Where(x => x.Name == model.Name).ToArray();
            // validate
            if (vaccines.Any(x => x.Vaccine.Id == model.VaccineId))
                throw new ApplicationException("Ya existe una vacuna desarrollada con el mismo laboratorio / vacuna");

            try
            {
                var vaccine = new DevelopedVaccine()
                {
                    Name = model.Name,
                    DaysToDelivery = model.DaysToDelivery,
                    Vaccine = Vaccines.Get(model.VaccineId)
                };

                // save vaccine
                _context.DevelopedVaccines.Add(vaccine);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public AppliedVaccine GetApplied(int id)
        {
            return _context.AppliedVaccines.Include(l => l.LocalBatchVaccine).ThenInclude(b => b.BatchVaccine).ThenInclude(dv => dv.DevelopedVaccine).ThenInclude(v => v.Vaccine).First(x => x.Id == id);
        }

        public DevelopedVaccine Get(int id)
        {
            return _context.DevelopedVaccines.First(x => x.Id == id);
        }

        public void Update(int id, UpdateDevelopedVaccineRequest model)
        {
            var user = _context.DevelopedVaccines.FirstOrDefault(x => x.Id == id);
            if (user == null)
                throw new HttpResponseException(400, message: "Vacuna no encontrada");

            if (string.IsNullOrEmpty(model.Name) == false && model.Name != user.Name)
            {
                var existOther = _context.DevelopedVaccines.Any(x => x.Name == model.Name && x.Id != id);
                if (existOther)
                {
                    throw new HttpResponseException(400, message: "Nombre de vacuna desarrollada '" + model.Name + "' en uso");
                }
                user.Name = model.Name;

            }
            if (model.IsActive.HasValue && model.IsActive != user.IsActive)
            {
                user.IsActive = model.IsActive.Value;
            }

            _context.SaveChanges();
        }

        private static void CheckIfExists(DevelopedVaccine? vaccine)
        {
            if (vaccine == null)
                throw new HttpResponseException(400, "Vacuna desarollada no encontrada");
        }

        public bool CanBeDeleted(int id)
        {
            var vaccine = _context.DevelopedVaccines.FirstOrDefault(x => x.Id == id);
            CheckIfExists(vaccine);
            // TODO: check if there is any invoice/purchase order
            return true;
        }

        public bool Exist(int id)
        {
            return _context.DevelopedVaccines.Any(x => x.Id == id);
        }

        public bool AlreadyExist(string name, int vaccineId)
        {
            var vaccines = _context.DevelopedVaccines.Where(x => x.Name == name).ToArray();
            return vaccines.Any(x => x.Vaccine.Id == vaccineId);
        }
    }
}