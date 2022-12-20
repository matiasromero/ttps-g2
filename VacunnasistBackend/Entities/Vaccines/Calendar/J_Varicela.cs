using System.Globalization;
using VacunnasistBackend.Entities;

namespace VacunassistBackend.Entities.Vaccines.Calendar;

public class J_Varicela : Vaccine
{
    public J_Varicela()
    : base(1000, "Varicela", VaccineTypeEnum.Calendar)
    {
        ShortName = "varicela";
        Doses = new[] {
            new VaccineDose(1001, 0, 15),
            new VaccineDose(1002, 1, 60),
        };
    }

    protected override Tuple<int?, string> internalValidation(Patient patient)
    {
        var alreadyApplied = patient.GetAlreadyAppliedVaccines(Id);
        if (alreadyApplied.Any())
        {
            if (alreadyApplied.Any(x => DateTime.Now >= x.AppliedDate.AddMonths(45)))
                return new Tuple<int?, string>(1002, "Segunda dosis aplicada");
            else
                return new Tuple<int?, string>(null, "Aun no se puede dar la segunda dosis. Seg�n esquema de vacunaci�n deben apsar 45 meses despues de la primera dosis.");
        }

        var iDate = DateTime.ParseExact(patient.BirthDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        return (DateTime.Now >= iDate.AddMonths(15)) ? new Tuple<int?, string>(1001, "Primera dosis aplicada") : new Tuple<int?, string>(null, "Aun no se puede aplicar la primera dosis");
    }
}
