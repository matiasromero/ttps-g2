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
        if (alreadyApplied.Any())
        {
            if (alreadyApplied.Any(x => x.AppliedDate.AddMonths(132) < DateTime.Now)) // tiene una dosis y la distancia es menor a 132 meses.
                return null;
            else if(alreadyApplied.Any(x => x.AppliedDate.AddMonths(132).AddDays(60) < DateTime.Now))
                return 2002; // tiene una dosis, y la distancia es mayor a 120 dias.
        }
        return 201;
    }
}
