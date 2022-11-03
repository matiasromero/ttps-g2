using System.Globalization;
using VacunnasistBackend.Entities;

namespace VacunassistBackend.Entities.Vaccines.Calendar;

public class N_FiebreHemorragica : Vaccine
{
    public N_FiebreHemorragica()
    : base(1400, "Fiebre Hemorragica (FHA)", VaccineType.Calendar)
    {
        Doses = new[] {
            new VaccineDose(1401, 0, 180)
        };
    }

    protected override int? internalValidation(Patient patient)
    {
        var alreadyApplied = patient.AppliedVaccines.Where(x => x.LocalBatchVaccine.BatchVaccine.DevelopedVaccine.Vaccine.Id == Id).ToArray();
        if (alreadyApplied.Any())
            return null;

        var iDate = DateTime.ParseExact(patient.BirthDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        return (iDate.AddMonths(180) < DateTime.Now) ? null : 1401;
    }
}
