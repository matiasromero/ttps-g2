using System.Globalization;
using VacunnasistBackend.Entities;

namespace VacunassistBackend.Entities.Vaccines.Calendar;

public class D_Quintuple : Vaccine
{
    public D_Quintuple()
    : base(400, "Quíntuple pentavalente", VaccineType.Calendar)
    {
        Doses = new[] {
            new VaccineDose(401, 0, 2),
            new VaccineDose(402, 1, 4, 60),
            new VaccineDose(403, 2, 6, 60),
            new VaccineDose(404, 3, 15, null, true) // 15 meses, refuerzo
        };
    }

    protected override Tuple<int?, string> internalValidation(Patient patient)
    {
        var alreadyApplied = patient.AppliedVaccines.Where(x => x.LocalBatchVaccine.BatchVaccine.DevelopedVaccine.Vaccine.Id == Id).ToArray();
        var iDate = DateTime.ParseExact(patient.BirthDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);

        switch (alreadyApplied.Length)
        {
            case 0: return (iDate.AddMonths(2) >= DateTime.Now) ? new Tuple<int?, string>(401, "Primera dosis aplicada") : new Tuple<int?, string>(null, "Aun no se puede dar la primera dosis");
            case 1: return (alreadyApplied[0].AppliedDate.AddDays(60) < DateTime.Now) ? null : 402; break;
            case 2: return (alreadyApplied[1].AppliedDate.AddDays(60) < DateTime.Now) ? null : 403; break;
            case 3: return (alreadyApplied[2].AppliedDate.AddMonths(9) < DateTime.Now) ? null : 404; break;
        }
        return null;
    }
}
