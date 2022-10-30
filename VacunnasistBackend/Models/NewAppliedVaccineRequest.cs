namespace VacunnasistBackend.Models
{
    public class NewAppliedVaccineRequest
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string DNI { get; set; }
        public string Gender { get; set; }
        public string BirthDate { get; set; }
        public string Province { get; set; }
        public bool Pregnant { get; set; }
        public bool HealthWorker { get; set; }
        public int DevelopedVaccineId { get; set; }
        public int ApplyBy { get; set; }
    }
}
