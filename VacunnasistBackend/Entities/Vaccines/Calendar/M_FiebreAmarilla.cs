using System.Globalization;
using VacunnasistBackend.Entities;

namespace VacunassistBackend.Entities.Vaccines.Calendar;

public class M_FiebreAmarilla : Vaccine
{
    public M_FiebreAmarilla()
    : base(1300, "Fiebre Amarilla", VaccineType.Calendar)
    {
        Doses = new[] {
            new VaccineDose(1301, 0, 18),
            new VaccineDose(1302, 1, 132, null, true) //11 años
        };
    }

    protected override Tuple<int?, string> internalValidation(Patient patient)
    {
        var alreadyApplied = patient.AppliedVaccines.Where(x => x.LocalBatchVaccine.BatchVaccine.DevelopedVaccine.Vaccine.Id == Id).ToArray();

        if (alreadyApplied.Any())
        {
            if (alreadyApplied.Any(x => x.AppliedDate.AddMonths(132) < DateTime.Now))
                return new Tuple<int?, string>(null, "Aun no se puede dar la segunda dosis");
            else
                return new Tuple<int?, string>(1302, "Segunda dosis aplicada");
        }
        var iDate = DateTime.ParseExact(patient.BirthDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        return (iDate.AddMonths(18) < DateTime.Now) ? new Tuple<int?, string>(null, "Aun no se puede dar la primera dosis") : new Tuple<int?, string>(1301, "Primera dosis aplicada");
    }
}
