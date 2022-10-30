using VacunnasistBackend.Entities;

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

    protected override int? internalValidation(Patient patient)
    {
        var alreadyApplied = patient.AppliedVaccines.Where(x => x.LocalBatchVaccine.BatchVaccine.DevelopedVaccine.Vaccine.Id == Id).ToArray();
        if (alreadyApplied.Any(x => x.AppliedDate.Year == DateTime.Now.Year))
            return null;
        return 3001;
    }
}
