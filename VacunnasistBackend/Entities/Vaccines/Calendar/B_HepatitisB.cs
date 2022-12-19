using System.Globalization;
using VacunnasistBackend.Entities;

namespace VacunassistBackend.Entities.Vaccines.Calendar;

public class B_HepatitisB : Vaccine
{
    public B_HepatitisB()
    : base(200, "Hepatitis B (HB)", VaccineTypeEnum.Calendar)
    {
        VaccineType = VaccineTypes.Subunidades;
        Doses = new[] {
            new VaccineDose(201, 0, 0),
            new VaccineDose(202, 1, 132),
            new VaccineDose(203, 2, 132, 30),
            new VaccineDose(204, 3, 132, 180)
        }; // se puede llegar a dar las 4, si cumplio 11 años, arranca con las 3 dosis
    }

    protected override Tuple<int?, string> internalValidation(Patient patient)
    {
        var alreadyApplied = patient.GetAlreadyAppliedVaccines(Id);
        var iDate = DateTime.ParseExact(patient.BirthDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);

        switch (alreadyApplied.Length)
        {
            case 0: return (DateTime.Now >= iDate.AddMonths(132)) ? new Tuple<int?, string>(202, "Primera dosis aplicada") : new Tuple<int?, string>(201, "Primera dosis aplicada");
            case 1: return (DateTime.Now >= iDate.AddMonths(132) && (DateTime.Now > alreadyApplied[0].AppliedDate.AddDays(30))) ? new Tuple<int?, string>(203, "Segunda dosis aplicada") : new Tuple<int?, string>(null, "Aun no se puede dar la segunda dosis. Debe pasar 1 mes de la primera aplicación.");
            case 2: return (DateTime.Now >= iDate.AddMonths(132) && (DateTime.Now > alreadyApplied[0].AppliedDate.AddDays(180))) ? new Tuple<int?, string>(204, "Tercera dosis aplicada") : new Tuple<int?, string>(null, "Aun no se puede dar la tercera dosis. Deben pasar 6 meses de la primera aplicación. ");
        }

        return new Tuple<int?, string>(null, "Error");
    }
}
