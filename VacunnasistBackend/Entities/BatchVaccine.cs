using System.ComponentModel.DataAnnotations;

namespace VacunassistBackend.Entities
{
    public class BatchVaccine
    {
        public BatchVaccine(string batchNumber, int quantity)
        {
            this.Status = BatchStatus.Valid;
            this._batchNumber = batchNumber;
            this.OverdueQuantity = 0;
            this.Quantity = quantity;
            this.RemainingQuantity = quantity;
        }

        [Key]
        public int Id { get; set; }
        public DateTime DueDate { get; set; }

        private string _batchNumber;
        public string BatchNumber => _batchNumber;

        public DevelopedVaccine DevelopedVaccine { get; set; }
        public int DevelopedVaccineId { get; set; }

        // public PurchaseOrder PurchaseOrder { get; set; }
        // public int PurchaseOrderId { get; set; }

        public int Quantity { get; set; }
        public int RemainingQuantity { get; set; }
        public int OverdueQuantity { get; set; }
        public int DistributedQuantity
        {
            get
            {
                return Quantity - RemainingQuantity - OverdueQuantity;
            }
        }
        public BatchStatus Status { get; set; }
    }

    public enum BatchStatus
    {
        Valid, // Válida
        Overdue // Vencida
    }
}