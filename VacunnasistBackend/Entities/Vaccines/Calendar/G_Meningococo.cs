using VacunnasistBackend.Entities;
using System.Globalization;

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
        var alreadyApplied = patient.GetAlreadyAppliedVaccines(Id);
        if (alreadyApplied.Any())
        {
            if (alreadyApplied.Any(x => x.AppliedDate.AddDays(60) < DateTime.Now))
                return new Tuple<int?, string>(null, "Aun no se puede dar la segunda dosis");
            else
                return new Tuple<int?, string>(702, "Segunda dosis aplicada");

        }
        var iDate = DateTime.ParseExact(patient.BirthDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        return (iDate.AddMonths(3) < DateTime.Now) ? new Tuple<int?, string>(null, "Aun no se puede dar la primera dosis") : new Tuple<int?, string>(701, "Primera dosis aplicada");
    }
}
