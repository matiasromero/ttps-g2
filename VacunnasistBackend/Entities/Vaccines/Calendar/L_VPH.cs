using System.Globalization;
using VacunnasistBackend.Entities;

namespace VacunassistBackend.Entities.Vaccines.Calendar;

public class L_VPH : Vaccine
{
    public L_VPH()
    : base(1200, "VPH", VaccineType.Calendar)
    {
        Doses = new[] {
            new VaccineDose(1201, 0, 132), //11 años
            new VaccineDose(1202, 1, 132, 180) //11 años
        };
    }

    protected override Tuple<int?, string> internalValidation(Patient patient)
    {
        var alreadyApplied = patient.AppliedVaccines.Where(x => x.LocalBatchVaccine.BatchVaccine.DevelopedVaccine.Vaccine.Id == Id).ToArray();
        if (alreadyApplied.Any())
        {
            if (alreadyApplied.Any(x => x.AppliedDate.AddDays(180) < DateTime.Now))
                return new Tuple<int?, string>(null, "Aun no se puede dar la segunda dosis");
            else
                return new Tuple<int?, string>(1202, "Segunda dosis aplicada");
        }
        var iDate = DateTime.ParseExact(patient.BirthDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        return (iDate.AddMonths(132) < DateTime.Now) ? new Tuple<int?, string>(null, "Aun no se puede dar la primera dosis") : new Tuple<int?, string>(1201, "Primera dosis aplicada");
    }
}
