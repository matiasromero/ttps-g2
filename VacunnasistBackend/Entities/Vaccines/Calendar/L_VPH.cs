namespace VacunassistBackend.Entities.Vaccines.Calendar;

public class L_VPH : Vaccine
{
    public L_VPH()
    : base(1200, "VPH", VaccineType.Calendar)
    {
        Doses = new[] {
            new VaccineDose(1201, 0, 132), //11 años
            new VaccineDose(1202, 1, 132, 180) //11 años
        };
    }

    protected override bool internalValidation()
    {
        return true;
    }
}
