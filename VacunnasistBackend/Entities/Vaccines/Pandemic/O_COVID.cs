namespace VacunassistBackend.Entities.Vaccines.Pandemic;

public class O_COVID : Vaccine
{
    public O_COVID()
    : base(2000, "COVID-19", VaccineType.Pandemic)
    {
        Doses = new[] {
            new VaccineDose(2001, 0, 0),
            new VaccineDose(2002, 1, 0, 120) // 4 meses segunda dosis
        };
    }

    protected override bool internalValidation()
    {
        return true;
    }
}
