namespace VacunassistBackend.Models
{
    public class NewDevelopedVaccineRequest
    {
        public string Name { get; set; }
        public int VaccineId { get; set; }
        public int DaysToDelivery { get; set; }
    }
}