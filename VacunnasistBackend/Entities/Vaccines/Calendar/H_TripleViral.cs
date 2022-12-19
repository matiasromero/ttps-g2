using System.Globalization;
using System.Linq;
using VacunnasistBackend.Entities;

namespace VacunassistBackend.Entities.Vaccines.Calendar;

public class H_TripleViral : Vaccine
{
    public H_TripleViral()
    : base(800, "Triple (SRP)", VaccineTypeEnum.Calendar)
    {
        VaccineType = VaccineTypes.Vector;
        Doses = new[] {
            new VaccineDose(801, 0, 12),
            new VaccineDose(802, 1, 60, 1826),
        };
    }

    protected override Tuple<int?, string> internalValidation(Patient patient)
    {
        var alreadyApplied = patient.GetAlreadyAppliedVaccines(Id);
        if (alreadyApplied.Any())
        {
            if (alreadyApplied.Any(x => DateTime.Now >= x.AppliedDate.AddDays(1826)))
                return new Tuple<int?, string>(802, "Segunda dosis aplicada");
            else
                return new Tuple<int?, string>(null, "Aun no se puede dar la segunda dosis");
        }

        var iDate = DateTime.ParseExact(patient.BirthDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        return (DateTime.Now >= iDate.AddMonths(12)) ? new Tuple<int?, string>(801, "Primera dosis aplicada") : new Tuple<int?, string>(null, "Aun no se puede dar la primera dosis");
    }
}
