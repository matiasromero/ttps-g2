namespace VacunassistBackend.Models.Filters
{
    public class DevelopedVaccinesFilterRequest
    {

        public string? Name { get; set; }
        public bool? IsActive { get; set; }
        public bool? CanBeRequested { get; set; }
    }
}