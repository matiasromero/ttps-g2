using VacunnasistBackend.Entities;

namespace VacunassistBackend.Entities.Vaccines.Seasonal;

public class P_Antigripal : Vaccine
{
    public P_Antigripal()
    : base(3000, "Antigripal", VaccineTypeEnum.Seasonal)
    {
        VaccineType = VaccineTypes.Vector;
        Doses = new[] {
            new VaccineDose(3001, 0, 0, 365), //dosis anual
        };
    }

    protected override Tuple<int?, string> internalValidation(Patient patient)
    {
        var alreadyApplied = patient.GetAlreadyAppliedVaccines(Id);
        if (alreadyApplied.Any(x => x.AppliedDate.Year == DateTime.Now.Year))
            return new Tuple<int?, string>(null, "Aun no se puede dar la dosis anual");
        return new Tuple<int?, string>(3001, "Primera dosis aplicada");
    }
}
