namespace VacunassistBackend.Models
{
    public class NewLocalBatchVaccineRequest
    {
        public int BatchVaccineId { get; set; }
        public int Quantity { get; set; }
        public string Province { get; set; }
    }
}