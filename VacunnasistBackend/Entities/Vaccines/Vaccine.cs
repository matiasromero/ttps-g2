using Microsoft.EntityFrameworkCore;

namespace VacunassistBackend.Entities.Vaccines;

[Serializable]
public class Vaccine
{
    public Vaccine(int id, string name, VaccineType type)
    {
        this.Name = name;
        this.Type = type;
        this.Id = id;
        this.Doses = new VaccineDose[] { };
    }
    public int Id { get; set; }
    public string Name { get; set; }
    public VaccineType Type { get; set; }

    public VaccineDose[] Doses { get; set; }

    public bool CanApply()
    {
        var isValid = internalValidation();

        return isValid;
    }

    protected virtual bool internalValidation()
    {
        return true;
    }
}

[Serializable]
public class VaccineDose
{
    public VaccineDose(int id, int number, int? minMonthsOfAge = null, int? daysAfterPreviousDose = null, bool IsReinforcement = false)
    {
        this.Id = id;
        this.Number = number;
        this.MinMonthsOfAge = minMonthsOfAge;
        this.DaysAfterPreviousDose = daysAfterPreviousDose;
        this.IsReinforcement = IsReinforcement;
    }

    public int Id { get; set; }
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
