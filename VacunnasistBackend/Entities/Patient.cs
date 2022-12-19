using System.ComponentModel.DataAnnotations;
using VacunassistBackend.Entities;

namespace VacunnasistBackend.Entities
{
    public class Patient
    {
        public Patient()
        {
            AppliedVaccines = new List<AppliedVaccine>();
        }

        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string DNI { get; set; }
        public string Gender { get; set; }
        public Department Department { get; set; }
        public int DepartmentId { get; set; }
        public string BirthDate { get; set; }
        public bool Pregnant { get; set; }
        public bool HealthWorker { get; set; }

        public virtual AppliedVaccine[] GetAlreadyAppliedVaccines(int id)
        {
            return AppliedVaccines.Where(x => x.LocalBatchVaccine.BatchVaccine.DevelopedVaccine.Vaccine.Id == id).ToArray();
        }

        public virtual List<AppliedVaccine> AppliedVaccines { get; set; }
    }
}
