using VacunnasistBackend.Entities;

namespace VacunassistBackend.Entities.Vaccines.Calendar;

public class G_Meningococo : Vaccine
{
    public G_Meningococo()
    : base(700, "Meningococo", VaccineType.Calendar)
    {
        Doses = new[] {
            new VaccineDose(701, 0, 3),
            new VaccineDose(702, 1, 5, 60),
        };
    }

    protected override Tuple<int?, string> internalValidation(Patient patient)
    {
        var alreadyApplied = patient.AppliedVaccines.Where(x => x.LocalBatchVaccine.BatchVaccine.DevelopedVaccine.Vaccine.Id == Id).ToArray();
        if (alreadyApplied.Any())
        {
            if (alreadyApplied.Any(x => x.AppliedDate.AddDays(60) < DateTime.Now))
                return null;
            else
                return 702;

        }
        return 701;
    }
}
