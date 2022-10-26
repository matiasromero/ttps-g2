namespace VacunassistBackend.Entities;

public class Vaccine
{
    public Vaccine(string name, VaccineType type)
    {
        this.Name = name;
        this.Type = type;
    }
    public int Id { get; set; }
    public string Name { get; set; }
    public VaccineType Type { get; set; }

    public VaccineDose[] Doses { get; set; } // [{id: 1, number: 1, edadmin: 0, distancia: null}, {id: 2, number: 2, edadmin: 0, distancia: 60}, {id: 3, number: 3, edadmin: 1800, esRefuerzo: true}]
}

public class VaccineDose
{
    public int DoseId { get; set; }
    public int Number { get; set; }
    public bool IsReinforcement { get; set; } = false;
    public int? MinMonthsOfAge { get; set; } // Meses minimo
    public int? DaysAfterPreviousDose { get; set; } // Distancia entre dosis
}

public enum VaccineType
{
    Calendar,
    Pandemic,
    Seasonal
}
