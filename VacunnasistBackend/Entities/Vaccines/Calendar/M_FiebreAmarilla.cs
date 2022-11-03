using System.Globalization;
using VacunnasistBackend.Entities;

namespace VacunassistBackend.Entities.Vaccines.Calendar;

public class M_FiebreAmarilla : Vaccine
{
    public M_FiebreAmarilla()
    : base(1300, "Fiebre Amarilla", VaccineType.Calendar)
    {
        Doses = new[] {
            new VaccineDose(1301, 0, 18),
            new VaccineDose(1302, 1, 132, null, true) //11 años
        };
    }

    protected override int? internalValidation(Patient patient)
    {
        var alreadyApplied = patient.AppliedVaccines.Where(x => x.LocalBatchVaccine.BatchVaccine.DevelopedVaccine.Vaccine.Id == Id).ToArray();

        if (alreadyApplied.Any())
        {
            if (alreadyApplied.Any(x => x.AppliedDate.AddMonths(132) < DateTime.Now))
                return null;
            else
                return 1302;
        }
        var iDate = DateTime.ParseExact(patient.BirthDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        return (iDate.AddMonths(18) < DateTime.Now) ? null : 1301;
    }
}
