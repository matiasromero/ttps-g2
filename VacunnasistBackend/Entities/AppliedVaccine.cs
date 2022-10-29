using System.ComponentModel.DataAnnotations;
using VacunnasistBackend.Entities;

namespace VacunassistBackend.Entities
{
    public class AppliedVaccine
    {
        public AppliedVaccine()
        {
            AppliedDate = DateTime.Now.Date;
        }

        [Key]
        public int Id { get; set; }
        public DateTime AppliedDate { get; set; }

        public int PatientId { get; set; }
        public Patient Patient { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public LocalBatchVaccine LocalBatchVaccine { get; set; }
        public int LocalBatchVaccineId { get; set; }
    }
}