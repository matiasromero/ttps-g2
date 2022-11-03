using System.Globalization;
using VacunnasistBackend.Entities;

namespace VacunassistBackend.Entities.Vaccines.Calendar;

public class C_Neumococo : Vaccine
{
    public C_Neumococo()
    : base(300, "Neumococo conjugado", VaccineType.Calendar)
    {
        Doses = new[] {
            new VaccineDose(301, 0, 2),
            new VaccineDose(302, 2, null, 60),
            new VaccineDose(303, 3, 8, null, true) // 8 meses, refuerzo
        };
    }

    protected override Tuple<int?, string> internalValidation(Patient patient)
    {
        var alreadyApplied = patient.AppliedVaccines.Where(x => x.LocalBatchVaccine.BatchVaccine.DevelopedVaccine.Vaccine.Id == Id).ToArray();
        var iDate = DateTime.ParseExact(patient.BirthDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);

        switch (alreadyApplied.Length)
        {
            case 0: return (iDate.AddMonths(2) >= DateTime.Now) ? 301 : null; break;
            case 1: return (alreadyApplied[0].AppliedDate.AddDays(60) < DateTime.Now) ? null : 302; break;
            case 2: return (alreadyApplied[1].AppliedDate.AddMonths(8) < DateTime.Now) ? null : 303; break;
        }
        return null;
    }
}
