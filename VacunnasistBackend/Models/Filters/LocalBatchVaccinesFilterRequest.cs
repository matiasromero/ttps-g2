using VacunassistBackend.Entities.Vaccines;

namespace VacunassistBackend.Models.Filters
{
    public class LocalBatchVaccinesFilterRequest
    {
        public int? DevelopedVaccineId { get; set; }
        public int? VaccineId { get; set; }
        public string? BatchNumber { get; set; }
        public string? Province { get; set; }
    }
}