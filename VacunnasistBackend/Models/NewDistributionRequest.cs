namespace VacunassistBackend.Models
{
    public class NewDistributionRequest
    {
        public int VaccineId { get; set; }
        public string Province { get; set; }
        public int Quantity { get; set; }
    }
}