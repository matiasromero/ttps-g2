using VacunassistBackend.Entities;

namespace VacunassistBackend.Models.Filters
{
    public class PurchaseOrdersFilterRequest
    {
        public PurchaseStatus? Status { get; set; }
        public DateTime? ETA { get; set; }
        public DateTime? PurchaseDate { get; set; }
        public string? BatchNumber { get; set; }
    }
}