using VacunnasistBackend.Entities;

namespace VacunassistBackend.Entities.Vaccines.Pandemic;

public class O_COVID : Vaccine
{
    public O_COVID()
    : base(2000, "COVID-19", VaccineType.Pandemic)
    {
        Doses = new[] {
            new VaccineDose(2001, 0, 0),
            new VaccineDose(2002, 1, 0, 120) // 120 dias segunda dosis
        };
    }

    protected override int? internalValidation(Patient patient)
    {
        var alreadyApplied = patient.AppliedVaccines.Where(x => x.LocalBatchVaccine.BatchVaccine.DevelopedVaccine.Vaccine.Id == Id).ToArray();
        if (alreadyApplied.Any())
        {
            if (alreadyApplied.Any(x => x.AppliedDate.AddDays(120) < DateTime.Now)) // tiene una dosis y la distancia es menor a 120 dias.
                return null;
            else
                return 2002; // tiene una dosis, y la distancia es mayor a 120 dias.
        }
        return 2001; // no tiene ninguna dosis y es la primera.
    }
}
