namespace VacunassistBackend.Models
{
    public class UpdateDevelopedVaccineRequest
    {
        public string? Name { get; set; }
        public bool? CanBeRequested { get; set; }
        public bool? IsActive { get; set; }
    }
}