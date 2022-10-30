using System.Linq;
using VacunnasistBackend.Entities;

namespace VacunassistBackend.Entities.Vaccines.Calendar;

public class H_TripleViral : Vaccine
{
    public H_TripleViral()
    : base(800, "Triple (SRP)", VaccineType.Calendar)
    {
        Doses = new[] {
            new VaccineDose(801, 0, 12),
            new VaccineDose(802, 1, 60, 1826),
        };
    }

    protected override int? internalValidation(Patient patient)
    {
        var alreadyApplied = patient.AppliedVaccines.Where(x => x.LocalBatchVaccine.BatchVaccine.DevelopedVaccine.Vaccine.Id == Id).ToArray();
        if (alreadyApplied.Any())
        {
            if (alreadyApplied.Any(x => x.AppliedDate.AddDays(1826) < DateTime.Now))
                return null;
            else
                return 802;
        }

        var iDate = Convert.ToDateTime(patient.BirthDate);
        return (iDate.AddMonths(12) < DateTime.Now) ? null : 801;
    }
}
