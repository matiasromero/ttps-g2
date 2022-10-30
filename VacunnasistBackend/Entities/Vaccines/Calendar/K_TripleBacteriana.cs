using VacunnasistBackend.Entities;

namespace VacunassistBackend.Entities.Vaccines.Calendar;

public class K_TripleBacteriana : Vaccine
{
    public K_TripleBacteriana()
    : base(1100, "Triple Bacteriana (DTP)", VaccineType.Calendar)
    {
        Doses = new[] {
            new VaccineDose(1101, 0, 60),
        };
    }

    protected override int? internalValidation(Patient patient)
    {
        var alreadyApplied = patient.AppliedVaccines.Where(x => x.LocalBatchVaccine.BatchVaccine.DevelopedVaccine.Vaccine.Id == Id).ToArray();
        if (alreadyApplied.Any())
            return null;

        var iDate = Convert.ToDateTime(patient.BirthDate);
        return (iDate.AddMonths(60) < DateTime.Now) ? null : 1101;
    }
}
