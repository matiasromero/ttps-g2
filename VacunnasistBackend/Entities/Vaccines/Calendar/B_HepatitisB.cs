namespace VacunassistBackend.Entities.Vaccines.Calendar;

public class B_HepatitisB : Vaccine
{
    public B_HepatitisB()
    : base(200, "Hepatitis B (HB)", VaccineType.Calendar)
    {
        Doses = new[] {
            new VaccineDose(201, 0, 0),
            new VaccineDose(202, 1, 132),
            new VaccineDose(203, 2, 132, 60),
            new VaccineDose(204, 3, 132, 180)
        }; // se puede llegar a dar las 4, si cumplio 11 años, arranca con las 3 dosis
    }

    protected override bool internalValidation()
    {
        return true;
    }
}
