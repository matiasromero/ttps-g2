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
            case 0:     
                if (iDate.AddMonths(2) > DateTime.Now)
                    return 501;
                else 
                    return null;
                break;
            case 1:
                if (alreadyApplied[0].AppliedDate.AddDays(60) < DateTime.Now)
                    return null;
                else
                    return 502;
                break;
            case 2:
                if (alreadyApplied[1].AppliedDate.AddDays(60) < DateTime.Now)
                    return null;
                else
                    return 503;
                break;
            case 3:
                if (alreadyApplied[2].AppliedDate.AddMonths(66) < DateTime.Now)
                    return null;
                else
                    return 504;
                break;

        }
        return null;
    }
}
