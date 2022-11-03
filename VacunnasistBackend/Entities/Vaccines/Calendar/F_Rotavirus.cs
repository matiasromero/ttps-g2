using System.Globalization;
using VacunnasistBackend.Entities;

namespace VacunassistBackend.Entities.Vaccines.Calendar;

public class F_Rotavirus : Vaccine
{
    public F_Rotavirus()
    : base(600, "Rotavirus", VaccineType.Calendar)
    {
        Doses = new[] {
            new VaccineDose(601, 0, 2),
            new VaccineDose(602, 1, 4, 60),
        };
    }

    protected override int? internalValidation(Patient patient)
    {
        var alreadyApplied = patient.AppliedVaccines.Where(x => x.LocalBatchVaccine.BatchVaccine.DevelopedVaccine.Vaccine.Id == Id).ToArray();
        if (alreadyApplied.Any())
        {
            if (alreadyApplied.Any(x => x.AppliedDate.AddDays(60) < DateTime.Now))
                return null;
            else
                return 602;
        }

        var iDate = DateTime.ParseExact(patient.BirthDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        return (iDate.AddMonths(2) < DateTime.Now) ? null : 601;
    }
}
