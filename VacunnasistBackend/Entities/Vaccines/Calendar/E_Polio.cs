using System.Globalization;
using VacunnasistBackend.Entities;

namespace VacunassistBackend.Entities.Vaccines.Calendar;

public class E_Polio : Vaccine
{
    public E_Polio()
    : base(500, "Polio (IPV)", VaccineTypeEnum.Calendar)
    {
        ShortName = "ipv";
        Doses = new[] {
            new VaccineDose(501, 0, 2),
            new VaccineDose(502, 1, 4, 60),
            new VaccineDose(503, 2, 6, 60),
            new VaccineDose(504, 3, 66, null, true) // 5 años y medio refuerzo
        };
    }

    protected override Tuple<int?, string> internalValidation(Patient patient)
    {
        var alreadyApplied = patient.GetAlreadyAppliedVaccines(Id);
        var iDate = DateTime.ParseExact(patient.BirthDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        switch (alreadyApplied.Length)
        {
            case 0:
                return (DateTime.Now >= iDate.AddMonths(2)) ? new Tuple<int?, string>(501, "Primera dosis aplicada") : new Tuple<int?, string>(null, "Aun no se puede dar la primera dosis. Según esquema de vacunación es a partir de los 2 meses de vida.");
            case 1:
                return (DateTime.Now >= alreadyApplied[0].AppliedDate.AddDays(60)) ? new Tuple<int?, string>(502, "Segunda dosis aplicada") : new Tuple<int?, string>(null, "Aun no se puede dar la segunda dosis. Deben pasar 2 meses de la primer dosis aplicada.");
            case 2:
                return (DateTime.Now >= alreadyApplied[1].AppliedDate.AddDays(60)) ? new Tuple<int?, string>(503, "Tercera dosis aplicada") : new Tuple<int?, string>(null, "Aun no se puede dar la tercera dosis. Deben pasar 2 meses de la primer dosis aplicada.");
            case 3:
                return (DateTime.Now >= alreadyApplied[2].AppliedDate.AddMonths(66)) ? new Tuple<int?, string>(504, "Refuerzo aplicado") : new Tuple<int?, string>(null, "Aun no se puede dar el refuerzo. Refuerzo a partir de los 5 años.");
        }
        return new Tuple<int?, string>(null, "Error");
    }
}
