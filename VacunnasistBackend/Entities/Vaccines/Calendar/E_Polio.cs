using System.Globalization;
using VacunnasistBackend.Entities;

namespace VacunassistBackend.Entities.Vaccines.Calendar;

public class E_Polio : Vaccine
{
    public E_Polio()
    : base(500, "Polio (IPV)", VaccineType.Calendar)
    {
        Doses = new[] {
            new VaccineDose(501, 0, 2),
            new VaccineDose(502, 1, 4, 60),
            new VaccineDose(503, 2, 6, 60),
            new VaccineDose(504, 3, 66, null, true) // 5 años y medio refuerzo
        };
    }

    protected override Tuple<int?, string> internalValidation(Patient patient)
    {
        var alreadyApplied = patient.AppliedVaccines.Where(x => x.LocalBatchVaccine.BatchVaccine.DevelopedVaccine.Vaccine.Id == Id).ToArray();
        var iDate = DateTime.ParseExact(patient.BirthDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        switch (alreadyApplied.Length)
        {
            case 0:
                return (iDate.AddMonths(2) > DateTime.Now) ? new Tuple<int?, string>(501, "Primera dosis aplicada") : new Tuple<int?, string>(null, "Aun no se puede dar la primera dosis");
            case 1:
                return (alreadyApplied[0].AppliedDate.AddDays(60) < DateTime.Now) ? new Tuple<int?, string>(null, "Aun no se puede dar la segunda dosis") : new Tuple<int?, string>(502, "Segunda dosis aplicada");
            case 2:
                return (alreadyApplied[1].AppliedDate.AddDays(60) < DateTime.Now) ? new Tuple<int?, string>(null, "Aun no se puede dar la tercera dosis") : new Tuple<int?, string>(503, "Tercera dosis aplicada");
            case 3:
                return (alreadyApplied[2].AppliedDate.AddMonths(66) < DateTime.Now) ? new Tuple<int?, string>(null, "Aun no se puede dar el refuerzo") : new Tuple<int?, string>(504, "Refuerzo aplicado");
        }
        return new Tuple<int?, string>(null, "Error");
    }
}
