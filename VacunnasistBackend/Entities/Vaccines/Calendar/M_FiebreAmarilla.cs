namespace VacunassistBackend.Entities.Vaccines.Calendar;

public class M_FiebreAmarilla : Vaccine
{
    public M_FiebreAmarilla()
    : base(1300, "Fiebre Amarilla", VaccineType.Calendar)
    {
        Doses = new[] {
            new VaccineDose(1301, 0, 18),
            new VaccineDose(1302, 1, 132, null, true) //11 años
        };
    }

    protected override bool internalValidation()
    {
        return true;
    }
}
