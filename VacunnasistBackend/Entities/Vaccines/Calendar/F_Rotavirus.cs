using System.Globalization;
using VacunnasistBackend.Entities;

namespace VacunassistBackend.Entities.Vaccines.Calendar;

public class F_Rotavirus : Vaccine
{
    public F_Rotavirus()
    : base(600, "Rotavirus", VaccineType.Calendar)
    {
        Doses = new[] {
            new VaccineDose(601, 0, 2),
            new VaccineDose(602, 1, 4, 60),
        };
    }

    protected override Tuple<int?, string> internalValidation(Patient patient)
    {
        var alreadyApplied = patient.GetAlreadyAppliedVaccines(Id);
        if (alreadyApplied.Any())
        {
            if (alreadyApplied.Any(x => x.AppliedDate.AddDays(60) < DateTime.Now))
                return new Tuple<int?, string>(null, "Aun no se puede dar la segunda dosis");
            else
                return new Tuple<int?, string>(602, "Segunda dosis aplicada");
        }

        var iDate = DateTime.ParseExact(patient.BirthDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        return (iDate.AddMonths(2) < DateTime.Now) ? new Tuple<int?, string>(null, "Aun no se puede dar la primera dosis") : new Tuple<int?, string>(601, "Primera dosis aplicada");
    }
}
