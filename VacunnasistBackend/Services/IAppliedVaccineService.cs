using Microsoft.EntityFrameworkCore;
using VacunassistBackend.Entities;
using VacunassistBackend.Infrastructure;
using VacunnasistBackend.Entities;
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
                .Include(x => x.Patient)
                .First(x => x.Id == id);
        }

        public AppliedVaccine[] GetAll(AppliedVaccinesFilterRequest filter)
        {
            var allDevVaccines = _context.DevelopedVaccines.ToArray();
            if (filter.DevelopedVaccineId.HasValue)
                allDevVaccines = allDevVaccines.Where(x => x.Id == filter.DevelopedVaccineId).ToArray();
            else if (filter.VaccineId.HasValue)
                allDevVaccines = allDevVaccines.Where(x => x.Vaccine.Id == filter.VaccineId.Value).ToArray();


            var query = _context.AppliedVaccines.Include(x => x.LocalBatchVaccine).ThenInclude(x => x.BatchVaccine).Include(x => x.User).Include(p => p.Patient).AsQueryable();
            if (!string.IsNullOrEmpty(filter.Province))
                query = query.Where(x => x.LocalBatchVaccine.Province == filter.Province);
            if (filter.AppliedById.HasValue)
                query = query.Where(x => x.User.Id == filter.AppliedById);
            if (filter.DNI.HasValue)
                query = query.Where(x => x.Patient.DNI.Equals(filter.DNI.ToString()));
            query = query.Where(x => allDevVaccines.Contains(x.LocalBatchVaccine.BatchVaccine.DevelopedVaccine));

            return query.ToArray();
        }

        public bool New(NewAppliedVaccineRequest model)
        {
            try
            {
                var patient = new Patient()
                {
                    Name = model.Name,
                    Surname = model.Surname,
                    DNI = model.DNI,
                    BirthDate = model.BirthDate,
                    Gender = model.Gender,
                    Province = model.Province,
                    Pregnant = model.Pregnant,
                    HealthWorker = model.HealthWorker
                };

                var user = _context.Users.ToArray().First(x => x.Id == model.ApplyBy);
                var provinceBatch = _context.LocalBatchVaccines.ToArray().First(b => b.Province == user.Province && b.BatchVaccine.DevelopedVaccineId == model.DevelopedVaccineId);

                var validationApplied = provinceBatch.BatchVaccine.DevelopedVaccine.Vaccine.CanApply(patient);

                if (!validationApplied.HasValue)
                {
                    throw new HttpResponseException(400, message: "La persona '" + model.Name + " " + model.Surname + " con DNI " + model.DNI + "' no puede aplicarse la vacuna.");
                }

                provinceBatch.RemainingQuantity = provinceBatch.RemainingQuantity--;

                var appliedVaccine = new AppliedVaccine()
                {
                    Patient = patient,
                    User = user,
                    LocalBatchVaccine = provinceBatch,
                    AppliedDose = validationApplied.Value
                };

                _context.AppliedVaccines.Add(appliedVaccine);
                patient.AppliedVaccines.Add(appliedVaccine);
                _context.Patients.Update(patient);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


    }
}
