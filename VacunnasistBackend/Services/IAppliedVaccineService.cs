using Microsoft.EntityFrameworkCore;
using VacunassistBackend.Entities;
using VacunassistBackend.Entities.Vaccines;
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
        Tuple<bool, string> New(NewAppliedVaccineRequest model);

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
                .ThenInclude(x => x.Department)
                .ThenInclude(x => x.Province)
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

        public Tuple<bool, string> New(NewAppliedVaccineRequest model)
        {
            try
            {
                var department = _context.Departments.First(x => x.Name == model.Department);
                var patient = _context.Patients.Include(x => x.AppliedVaccines).Any(x => x.DNI == model.DNI.ToString()) ?
                    _context.Patients.Include(x => x.AppliedVaccines).First(x => x.DNI == model.DNI.ToString()) :
                    new Patient()
                    {
                        Name = model.Name,
                        Surname = model.Surname,
                        DNI = model.DNI.ToString(),
                        BirthDate = model.BirthDate,
                        Gender = model.Gender,
                        Department = department,
                        Pregnant = model.Pregnant,
                        HealthWorker = model.HealthWorker
                    };

                var user = _context.Users.ToArray().First(x => x.Id == model.ApplyBy);

                var provinceBatch = _context.LocalBatchVaccines.Include(x => x.BatchVaccine).ThenInclude(x => x.DevelopedVaccine)
                .Where(b => b.RemainingQuantity > 0 && b.Province == user.Province && b.BatchVaccine.DevelopedVaccineId == model.DevelopedVaccineId)
                .OrderBy(x => x.BatchVaccine.DueDate)
                .FirstOrDefault();

                if (provinceBatch == null)
                {
                    throw new HttpResponseException(400, message: "No se posee stock de la vacuna desarrollada seleccionada.");
                }

                var vaccine = Vaccines.Get(provinceBatch.BatchVaccine.DevelopedVaccine.Vaccine.Id);
                var validationApplied = vaccine.CanApply(patient);

                if (!validationApplied.Item1.HasValue)
                {
                    throw new HttpResponseException(400, message: "La persona '" + model.Name + " " + model.Surname + " con DNI " + model.DNI.ToString() + "' no puede aplicarse la vacuna. " + validationApplied.Item2);
                }

                provinceBatch.RemainingQuantity--;

                var appliedVaccine = new AppliedVaccine()
                {
                    Patient = patient,
                    User = user,
                    LocalBatchVaccine = provinceBatch,
                    AppliedDose = validationApplied.Item1.Value
                };

                _context.AppliedVaccines.Add(appliedVaccine);
                patient.AppliedVaccines.Add(appliedVaccine);
                _context.SaveChanges();
                return new Tuple<bool, string>(true, validationApplied.Item2);
            }
            catch (Exception e)
            {
                return new Tuple<bool, string>(false, e.Message);
            }
        }


    }
}
