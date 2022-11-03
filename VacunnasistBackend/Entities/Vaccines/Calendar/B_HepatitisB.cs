using System.Globalization;
using VacunnasistBackend.Entities;

namespace VacunassistBackend.Entities.Vaccines.Calendar;

public class B_HepatitisB : Vaccine
{
    public B_HepatitisB()
    : base(200, "Hepatitis B (HB)", VaccineType.Calendar)
    {
        Doses = new[] {
            new VaccineDose(201, 0, 0),
            new VaccineDose(202, 1, 132),
            new VaccineDose(203, 2, 132, 60),
            new VaccineDose(204, 3, 132, 180)
        }; // se puede llegar a dar las 4, si cumplio 11 años, arranca con las 3 dosis
    }

    protected override int? internalValidation(Patient patient)
    {
        var alreadyApplied = patient.AppliedVaccines.Where(x => x.LocalBatchVaccine.BatchVaccine.DevelopedVaccine.Vaccine.Id == Id).ToArray();
        var iDate = DateTime.ParseExact(patient.BirthDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);

        switch (alreadyApplied.Length)
        {
            case 0: return (iDate.AddMonths(132) >= DateTime.Now) ? 202 : 201; break;
            case 1: return (iDate.AddMonths(132) >= DateTime.Now && alreadyApplied[0].AppliedDate.AddDays(60) < DateTime.Now) ? null : 203; break;
            case 2: return (iDate.AddMonths(132) >= DateTime.Now && alreadyApplied[0].AppliedDate.AddDays(180) < DateTime.Now) ? null : 204; break;

        }
        return null;
    }
}
