namespace VacunassistBackend.Models
{
    public class NewBatchVaccineRequest
    {
        public string BatchNumber { get; set; }
        public DateTime DueDate { get; set; }
        public int DevelopedVaccineId { get; set; }
        public int Quantity { get; set; }
    }
}