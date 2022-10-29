using System.ComponentModel.DataAnnotations;

namespace VacunassistBackend.Entities
{
    public class LocalBatchVaccine
    {
        public LocalBatchVaccine(int quantity, string province, int batchVaccineId)
        {
            this.Province = province;
            this.DistributionDate = DateTime.Now;
            this.Quantity = quantity;
            this.RemainingQuantity = quantity;
            this.BatchVaccineId = batchVaccineId;
        }

        [Key]
        public int Id { get; set; }
        public BatchVaccine BatchVaccine { get; set; }
        public int BatchVaccineId { get; set; }
        public int Quantity { get; set; }
        public int RemainingQuantity { get; set; }
        public int OverdueQuantity { get; set; }
        public string Province { get; set; }
        public DateTime DistributionDate { get; set; }

        public void checkOverdue()
        {
            if (BatchVaccine.DueDate < DateTime.Now.Date)
            {
                OverdueQuantity = RemainingQuantity;
                RemainingQuantity = 0;
            }

        }
    }
}