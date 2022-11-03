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

    protected override int? internalValidation(Patient patient)
    {
        var alreadyApplied = patient.AppliedVaccines.Where(x => x.LocalBatchVaccine.BatchVaccine.DevelopedVaccine.Vaccine.Id == Id).ToArray();
        if (alreadyApplied.Any())
        {
            if (alreadyApplied.Any(x => x.AppliedDate.AddMonths(60) < DateTime.Now))
                return null;
            else
                return 1002;
        }

        var iDate = DateTime.ParseExact(patient.BirthDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        return (iDate.AddMonths(15) < DateTime.Now) ? null : 1001;
    }
}
