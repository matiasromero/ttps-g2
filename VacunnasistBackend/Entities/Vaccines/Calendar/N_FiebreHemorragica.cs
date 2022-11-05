using System.Globalization;
using VacunnasistBackend.Entities;

namespace VacunassistBackend.Entities.Vaccines.Calendar;

public class N_FiebreHemorragica : Vaccine
{
    public N_FiebreHemorragica()
    : base(1400, "Fiebre Hemorragica (FHA)", VaccineType.Calendar)
    {
        Doses = new[] {
            new VaccineDose(1401, 0, 180)
        };
    }

    protected override Tuple<int?, string> internalValidation(Patient patient)
    {
        var alreadyApplied = patient.GetAlreadyAppliedVaccines(Id);
        if (alreadyApplied.Any())
            return new Tuple<int?, string>(null, "Ya posee el esquema completo");

        var iDate = DateTime.ParseExact(patient.BirthDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        return (iDate.AddMonths(180) < DateTime.Now) ? new Tuple<int?, string>(null, "Aun no se puede dar la primera dosis") : new Tuple<int?, string>(1401, "Primera dosis aplicada");
    }
}
