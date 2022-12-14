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
        public bool IsActive { get; set; } = true;

        public string VaccineType { get; set; }

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

    public static class DevelopedVaccineTypes
    {
        public static string Arnm = "ARNM";
        public static string Vector = "Vector viral";
        public static string Subunidades = "subunidades proteicas";
    }
}