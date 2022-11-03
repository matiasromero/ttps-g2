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

    protected override Tuple<int?, string> internalValidation(Patient patient)
    {
        var alreadyApplied = patient.AppliedVaccines.Where(x => x.LocalBatchVaccine.BatchVaccine.DevelopedVaccine.Vaccine.Id == Id).ToArray();
        if (alreadyApplied.Any())
            return null;

        var iDate = DateTime.ParseExact(patient.BirthDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        return (iDate.AddMonths(12) < DateTime.Now) ? new Tuple<int?, string>(null, "Aun no se puede aplicar la primera dosis") : new Tuple<int?, string>(901, "Primera dosis aplicada");
    }
}
