using System.Globalization;
using VacunnasistBackend.Entities;

namespace VacunassistBackend.Entities.Vaccines.Calendar;

public class M_FiebreAmarilla : Vaccine
{
    public M_FiebreAmarilla()
    : base(1300, "Fiebre Amarilla", VaccineTypeEnum.Calendar)
    {
        ShortName = "fiebreamarilla";
        Doses = new[] {
            new VaccineDose(1301, 0, 18),
            new VaccineDose(1302, 1, 132, null, true) //11 años
        };
    }

    protected override Tuple<int?, string> internalValidation(Patient patient)
    {
        var alreadyApplied = patient.GetAlreadyAppliedVaccines(Id);

        if (alreadyApplied.Any())
        {
            if (alreadyApplied.Any(x => DateTime.Now >= x.AppliedDate.AddMonths(132)))
                return new Tuple<int?, string>(1302, "Segunda dosis aplicada");
            else
                return new Tuple<int?, string>(null, "Aun no se puede dar la segunda dosis");
        }
        var iDate = DateTime.ParseExact(patient.BirthDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        return (DateTime.Now >= iDate.AddMonths(18)) ? new Tuple<int?, string>(1301, "Primera dosis aplicada") : new Tuple<int?, string>(null, "Aun no se puede dar la primera dosis. Según esquema de vacunación aplicar a partir de los 18 meses de edad.");
    }
}
