using VacunassistBackend.Entities;

namespace VacunassistBackend.Models
{
    public class UpdatePurchaseOrderRequest
    {
        public PurchaseStatus? Status { get; set; }
    }
}