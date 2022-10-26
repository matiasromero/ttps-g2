namespace VacunassistBackend.Entities.Vaccines.Calendar;

public class K_TripleBacteriana : Vaccine
{
    public K_TripleBacteriana()
    : base(1100, "Triple Bacteriana (DTP)", VaccineType.Calendar)
    {
        Doses = new[] {
            new VaccineDose(1101, 0, 60),
        };
    }

    protected override bool internalValidation()
    {
        return true;
    }
}
