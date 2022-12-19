using VacunassistBackend.Entities.Vaccines;

namespace VacunassistBackend.Models.Filters
{
    public class VaccinesFilterRequest
    {
        public VaccineTypeEnum? Type { get; set; }
        public bool? WithStock { get; set; }
    }
}