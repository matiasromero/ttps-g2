using System.Globalization;
using VacunnasistBackend.Entities;

namespace VacunassistBackend.Entities.Vaccines.Calendar;

public class L_VPH : Vaccine
{
    public L_VPH()
    : base(1200, "VPH", VaccineTypeEnum.Calendar)
    {
        ShortName = "vph";
        Doses = new[] {
            new VaccineDose(1201, 0, 132), //11 años
            new VaccineDose(1202, 1, 132, 180) //11 años
        };
    }

    protected override Tuple<int?, string> internalValidation(Patient patient)
    {
        var alreadyApplied = patient.GetAlreadyAppliedVaccines(Id);
        if (alreadyApplied.Any())
        {
            if (alreadyApplied.Any(x => DateTime.Now >= x.AppliedDate.AddDays(180)))
                return new Tuple<int?, string>(1202, "Segunda dosis aplicada");
            else
                return new Tuple<int?, string>(null, "Aun no se puede dar la segunda dosis. Deben pasar 6 meses entre la dosis anterior.");
        }
        var iDate = DateTime.ParseExact(patient.BirthDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        return (DateTime.Now >= iDate.AddMonths(132)) ? new Tuple<int?, string>(1201, "Primera dosis aplicada") : new Tuple<int?, string>(null, "Aun no se puede dar la primera dosis. Según esquema de vacunación es a partir de los 11 años.");
    }
}
