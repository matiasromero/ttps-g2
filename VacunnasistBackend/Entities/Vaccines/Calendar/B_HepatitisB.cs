using System.Globalization;
using VacunnasistBackend.Entities;

namespace VacunassistBackend.Entities.Vaccines.Calendar;

public class B_HepatitisB : Vaccine
{
    public B_HepatitisB()
    : base(200, "Hepatitis B (HB)", VaccineType.Calendar)
    {
        Doses = new[] {
            new VaccineDose(201, 0, 0),
            new VaccineDose(202, 1, 132),
            new VaccineDose(203, 2, 132, 60),
            new VaccineDose(204, 3, 132, 180)
        }; // se puede llegar a dar las 4, si cumplio 11 años, arranca con las 3 dosis
    }

    protected override Tuple<int?, string> internalValidation(Patient patient)
    {
        var alreadyApplied = patient.GetAlreadyAppliedVaccines(Id);
        var iDate = DateTime.ParseExact(patient.BirthDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);

        switch (alreadyApplied.Length)
        {
            case 0: return (iDate.AddMonths(132) >= DateTime.Now) ? new Tuple<int?, string>(202, "Primera dosis aplicada") : new Tuple<int?, string>(201, "Primera dosis aplicada");
            case 1: return (iDate.AddMonths(132) >= DateTime.Now && alreadyApplied[0].AppliedDate.AddDays(60) < DateTime.Now) ? new Tuple<int?, string>(null, "Aun no se puede dar la segunda dosis") : new Tuple<int?, string>(203, "Segunda dosis aplicada");
            case 2: return (iDate.AddMonths(132) >= DateTime.Now && alreadyApplied[0].AppliedDate.AddDays(180) < DateTime.Now) ? new Tuple<int?, string>(null, "Aun no se puede dar la tercera dosis") : new Tuple<int?, string>(204, "Tercera dosis aplicada");
        }

        return new Tuple<int?, string>(null, "Error");
    }
}
