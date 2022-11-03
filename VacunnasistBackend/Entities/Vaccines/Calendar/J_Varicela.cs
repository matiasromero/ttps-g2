using System.Globalization;
using VacunnasistBackend.Entities;

namespace VacunassistBackend.Entities.Vaccines.Calendar;

public class J_Varicela : Vaccine
{
    public J_Varicela()
    : base(1000, "Varicela", VaccineType.Calendar)
    {
        Doses = new[] {
            new VaccineDose(1001, 0, 15),
            new VaccineDose(1002, 1, 60),
        };
    }

    protected override Tuple<int?, string> internalValidation(Patient patient)
    {
        var alreadyApplied = patient.AppliedVaccines.Where(x => x.LocalBatchVaccine.BatchVaccine.DevelopedVaccine.Vaccine.Id == Id).ToArray();
        if (alreadyApplied.Any())
        {
            if (alreadyApplied.Any(x => x.AppliedDate.AddMonths(60) < DateTime.Now))
                return new Tuple<int?, string>(null, "Aun no se puede dar la segunda dosis");
            else
                return new Tuple<int?, string>(1002, "Segunda dosis aplicada");
        }

        var iDate = DateTime.ParseExact(patient.BirthDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        return (iDate.AddMonths(15) < DateTime.Now) ? new Tuple<int?, string>(null, "Aun no se puede aplicar la primera dosis") : new Tuple<int?, string>(1001, "Primera dosis aplicada");
    }
}
