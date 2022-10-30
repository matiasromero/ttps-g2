using VacunassistBackend.Entities.Vaccines;

namespace VacunassistBackend.Models.Filters
{
    public class VaccinesFilterRequest
    {
        public VaccineType? Type { get; set; }
        public bool? WithStock { get; set; }
    }
}