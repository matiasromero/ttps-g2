using System.Globalization;
using VacunnasistBackend.Entities;

namespace VacunassistBackend.Entities.Vaccines.Calendar;

public class C_Neumococo : Vaccine
{
    public C_Neumococo()
    : base(300, "Neumococo conjugado", VaccineType.Calendar)
    {
        Doses = new[] {
            new VaccineDose(301, 0, 2),
            new VaccineDose(302, 2, null, 60),
            new VaccineDose(303, 3, 8, null, true) // 8 meses, refuerzo
        };
    }

    protected override Tuple<int?, string> internalValidation(Patient patient)
    {
        var alreadyApplied = patient.GetAlreadyAppliedVaccines(Id);
        var iDate = DateTime.ParseExact(patient.BirthDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);

        switch (alreadyApplied.Length)
        {
            case 0: return (DateTime.Now >= iDate.AddMonths(2)) ? new Tuple<int?, string>(301, "Primera dosis aplicada") : new Tuple<int?, string>(null, "Aun no se puede dar la primera dosis. Según esquema de vacunación es a partir de los 2 meses de vida.");
            case 1: return (DateTime.Now >= alreadyApplied[0].AppliedDate.AddDays(60)) ? new Tuple<int?, string>(302, "Segunda dosis aplicada") : new Tuple<int?, string>(null, "Aun no se puede dar la segunda dosis. Deben pasar 2 meses de la primer dosis aplicada.");
            case 2: return (DateTime.Now >= alreadyApplied[1].AppliedDate.AddMonths(8)) ? new Tuple<int?, string>(203, "Refuerzo aplicado") : new Tuple<int?, string>(null, "Aun no se puede dar el refuerzo. Deben pasar 8 meses para su aplicación.");
        }
        return new Tuple<int?, string>(null, "Error");
    }
}
