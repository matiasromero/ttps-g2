using VacunnasistBackend.Entities;

namespace VacunassistBackend.Entities.Vaccines.Calendar;

public class E_Polio : Vaccine
{
    public E_Polio()
    : base(500, "Polio (IPV)", VaccineType.Calendar)
    {
        Doses = new[] {
            new VaccineDose(501, 0, 2),
            new VaccineDose(502, 1, 4, 60),
            new VaccineDose(503, 2, 6, 60),
            new VaccineDose(504, 3, 66, null, true) // 5 años y medio refuerzo
        };
    }

    protected override int? internalValidation(Patient patient)
    {
        var alreadyApplied = patient.AppliedVaccines.Where(x => x.LocalBatchVaccine.BatchVaccine.DevelopedVaccine.Vaccine.Id == Id).ToArray();
        var iDate = Convert.ToDateTime(patient.BirthDate);
        switch (alreadyApplied.Length)
        {
            case 0: return (iDate.AddMonths(2) > DateTime.Now) ? 501 : null;
                break;
            case 1: return (alreadyApplied[0].AppliedDate.AddDays(60) < DateTime.Now) ? null : 502;
                break;
            case 2: return (alreadyApplied[1].AppliedDate.AddDays(60) < DateTime.Now) ? null : 503;
                break;
            case 3: return (alreadyApplied[2].AppliedDate.AddMonths(66) < DateTime.Now) ? null : 504;
                break;
        }
        return null;
    }
}
