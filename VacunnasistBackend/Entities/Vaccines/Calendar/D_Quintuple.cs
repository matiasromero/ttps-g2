using System.Globalization;
using VacunnasistBackend.Entities;

namespace VacunassistBackend.Entities.Vaccines.Calendar;

public class D_Quintuple : Vaccine
{
    public D_Quintuple()
    : base(400, "Quíntuple pentavalente", VaccineTypeEnum.Calendar)
    {
        VaccineType = VaccineTypes.Subunidades;
        Doses = new[] {
            new VaccineDose(401, 0, 2),
            new VaccineDose(402, 1, 4, 60),
            new VaccineDose(403, 2, 6, 60),
            new VaccineDose(404, 3, 15, null, true) // 15 meses, refuerzo
        };
    }

    protected override Tuple<int?, string> internalValidation(Patient patient)
    {
        var alreadyApplied = patient.GetAlreadyAppliedVaccines(Id);
        var iDate = DateTime.ParseExact(patient.BirthDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);

        switch (alreadyApplied.Length)
        {
            case 0: return (iDate.AddMonths(2) <= DateTime.Now) ? new Tuple<int?, string>(401, "Primera dosis aplicada") : new Tuple<int?, string>(null, "Aun no se puede dar la primera dosis. Según esquema de vacunación es a partir de los 2 meses de vida.");
            case 1: return (DateTime.Now >= alreadyApplied[0].AppliedDate.AddDays(60)) ? new Tuple<int?, string>(402, "Segunda dosis aplicada") : new Tuple<int?, string>(null, "Aun no se puede dar la segunda dosis. Deben pasar 2 meses de la primer dosis aplicada.");
            case 2: return (DateTime.Now >= alreadyApplied[1].AppliedDate.AddDays(60)) ? new Tuple<int?, string>(403, "Tercera dosis aplicada") : new Tuple<int?, string>(null, "Aun no se puede dar la tercera dosis. Deben pasar 2 meses de la tercer dosis aplicada.");
            case 3: return (DateTime.Now >= alreadyApplied[2].AppliedDate.AddMonths(9)) ? new Tuple<int?, string>(404, "Refuerzo dosis aplicada") : new Tuple<int?, string>(null, "Aun no se puede dar el refuerzo. Deben pasar 9 meses de la dosis anterior.");
        }
        return new Tuple<int?, string>(null, "Error");
    }
}
