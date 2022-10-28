namespace VacunassistBackend.Models
{
    public class NewPurchaseOrderRequest
    {
        public int DevelopedVaccineId { get; set; }
        public int Quantity { get; set; }
    }
}