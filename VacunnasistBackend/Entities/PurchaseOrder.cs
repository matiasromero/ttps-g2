using System.ComponentModel.DataAnnotations;

namespace VacunassistBackend.Entities
{
    public class PurchaseOrder
    {
        public PurchaseOrder(int quantity, int developedVaccineId)
        {
            this.Status = PurchaseStatus.Pending;
            this.Quantity = quantity;
            this.PurchaseDate = DateTime.Now;
            this.DevelopedVaccineId = developedVaccineId;
        }

        [Key]
        public int Id { get; set; }
        public DateTime PurchaseDate { get; set; }
        public DateTime? ETA { get; set; }
        public string? BatchNumber { get; set; }
        public DateTime? DeliveredTime { get; set; }
        public DevelopedVaccine DevelopedVaccine { get; set; }
        public int DevelopedVaccineId { get; set; }
        public int Quantity { get; set; }
        public PurchaseStatus Status { get; set; }

        public void ChangeStatus(PurchaseStatus newStatus, string batchNumber)
        {
            this.Status = newStatus;
            if (newStatus == PurchaseStatus.Confirmed)
            {
                this.ETA = PurchaseDate.AddDays(DevelopedVaccine.DaysToDelivery);
                this.BatchNumber = batchNumber;
            }

            if (newStatus == PurchaseStatus.Delivered)
            {
                this.DeliveredTime = DateTime.Now;
            }
        }
    }

    public enum PurchaseStatus
    {
        Pending, // Pendiente
        Confirmed,  // Confirmada
        Delivered // Entregada
    }
}