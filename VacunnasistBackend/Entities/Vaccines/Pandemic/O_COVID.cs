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

    protected override Tuple<int?, string> internalValidation(Patient patient)
    {
        var alreadyApplied = patient.AppliedVaccines.Where(x => x.LocalBatchVaccine.BatchVaccine.DevelopedVaccine.Vaccine.Id == Id).ToArray();
        if (alreadyApplied.Any())
        {
            if (alreadyApplied.Any(x => x.AppliedDate.AddDays(120) < DateTime.Now)) // tiene una dosis y la distancia es menor a 120 dias.
                return new Tuple<int?, string>(null, "Aun no se puede dar la segunda dosis");
            else
                return new Tuple<int?, string>(2002, "Segunda dosis aplicada"); // tiene una dosis, y la distancia es mayor a 120 dias.
        }
        return new Tuple<int?, string>(2001, "Primera dosis aplicada"); // no tiene ninguna dosis y es la primera.
    }
}
