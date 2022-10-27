namespace VacunassistBackend.Entities.Vaccines.Seasonal;

public class P_Antigripal : Vaccine
{
    public P_Antigripal()
    : base(3000, "Antigripal", VaccineType.Seasonal)
    {
        Doses = new[] {
            new VaccineDose(3001, 0, 0, 365), //dosis anual
        };
    }

    protected override bool internalValidation()
    {
        return true;
    }
}
