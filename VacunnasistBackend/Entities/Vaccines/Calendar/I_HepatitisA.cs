using System.Globalization;
using VacunnasistBackend.Entities;

namespace VacunassistBackend.Entities.Vaccines.Calendar;

public class I_HepatitisA : Vaccine
{
    public I_HepatitisA()
    : base(900, "Hepatitis A (HA)", VaccineType.Calendar)
    {
        Doses = new[] {
            new VaccineDose(901, 0, 12),
        };
    }

    protected override int? internalValidation(Patient patient)
    {
        var alreadyApplied = patient.AppliedVaccines.Where(x => x.LocalBatchVaccine.BatchVaccine.DevelopedVaccine.Vaccine.Id == Id).ToArray();
        if (alreadyApplied.Any())
            return null;

        var iDate = DateTime.ParseExact(patient.BirthDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        return (iDate.AddMonths(12) < DateTime.Now) ? null : 901;
    }
}
