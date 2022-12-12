using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using VacunassistBackend.Entities.Vaccines;

namespace VacunassistBackend.Entities
{

    public class DevelopedVaccine
    {
        public DevelopedVaccine()
        {
            IsActive = true;
        }

        public DevelopedVaccine(string name)
            : this()
        {
            Name = name;
        }

        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;
        public Laboratory Laboratory { get; set; }
        public int LaboratoryId { get; set; }
        public bool IsActive { get; set; } = true;

        public DevelopedVaccineType Type { get; set; }

        public int DaysToDelivery { get; set; }

        [NotMapped]
        public Vaccine Vaccine { get; set; }

        public string VaccineText
        {
            get
            {
                return JsonSerializer.Serialize(Vaccine);
            }
            set
            {
                Vaccine = JsonSerializer.Deserialize<Vaccine>(value);
            }
        }
    }

    public enum DevelopedVaccineType
    {
        ARNM,
        Vector_viral,
        Subunidades_proteicas
    }
}