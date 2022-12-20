using VacunnasistBackend.Entities;
using System.Globalization;

namespace VacunassistBackend.Entities.Vaccines.Calendar;

public class G_Meningococo : Vaccine
{
    public G_Meningococo()
    : base(700, "Meningococo", VaccineTypeEnum.Calendar)
    {
        ShortName = "meningococo";
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
            if (alreadyApplied.Any(x => DateTime.Now >= x.AppliedDate.AddDays(60)))
                return new Tuple<int?, string>(702, "Segunda dosis aplicada");
            else
                return new Tuple<int?, string>(null, "Aun no se puede dar la segunda dosis. Deben pasar 2 meses de la primer dosis aplicada.");

        }
        var iDate = DateTime.ParseExact(patient.BirthDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        return (DateTime.Now >= iDate.AddMonths(3)) ? new Tuple<int?, string>(701, "Primera dosis aplicada") : new Tuple<int?, string>(null, "Aun no se puede dar la primera dosis. Seg�n esquema de vacunaci�n es a partir de los 3 meses de vida.");
    }
}
