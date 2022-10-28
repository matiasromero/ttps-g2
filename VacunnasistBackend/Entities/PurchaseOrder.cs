using System.ComponentModel.DataAnnotations;

namespace VacunassistBackend.Entities
{
    public class PurchaseOrder
    {
        public PurchaseOrder(string batchNumber, int quantity, int developedVaccineId)
        {
            this.Status = PurchaseStatus.Pending;
            this._batchNumber = batchNumber;
            this.Quantity = quantity;
            this.PurchaseDate = DateTime.Now;
            this.DevelopedVaccineId = developedVaccineId;
        }

        [Key]
        public int Id { get; set; }
        public DateTime PurchaseDate { get; set; }
        public DateTime? ETA { get; set; }
        public DateTime? DeliveredTime { get; set; }
        private string _batchNumber;
        public string BatchNumber => _batchNumber;
        public DevelopedVaccine DevelopedVaccine { get; set; }
        public int DevelopedVaccineId { get; set; }
        public int Quantity { get; set; }
        public PurchaseStatus Status { get; set; }

        public void ChangeStatus(PurchaseStatus newStatus)
        {
            this.Status = newStatus;
            if (newStatus == PurchaseStatus.Confirmed)
            {
                this.ETA = PurchaseDate.AddDays(DevelopedVaccine.DaysToDelivery);
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