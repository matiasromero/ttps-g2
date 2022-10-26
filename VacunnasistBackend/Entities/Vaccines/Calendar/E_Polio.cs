namespace VacunassistBackend.Entities.Vaccines.Calendar;

public class E_Polio : Vaccine
{
    public E_Polio()
    : base(500, "Polio (IPV)", VaccineType.Calendar)
    {
        Doses = new[] {
            new VaccineDose(501, 0, 2),
            new VaccineDose(502, 1, 4, 60),
            new VaccineDose(503, 2, 6, 60),
            new VaccineDose(504, 3, 66, null, true) // 5 años y medio refuerzo
        };
    }

    protected override bool internalValidation()
    {
        return true;
    }
}
