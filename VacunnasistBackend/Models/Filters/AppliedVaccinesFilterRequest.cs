namespace VacunnasistBackend.Models.Filters
{
    public class AppliedVaccinesFilterRequest
    {
        public DateTime? AppliedDate { get; set; }

        public int? DevelopedVaccineId { get; set; }

        public int? VaccineId { get; set; }

        public string? Province { get; set; }

        public string? AppliedBy { get; set; }


    }
}
