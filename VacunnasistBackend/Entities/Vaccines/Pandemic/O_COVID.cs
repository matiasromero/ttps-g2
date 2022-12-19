using VacunnasistBackend.Entities;

namespace VacunassistBackend.Entities.Vaccines.Pandemic;

public class O_COVID : Vaccine
{
    public O_COVID()
    : base(2000, "COVID-19", VaccineTypeEnum.Pandemic)
    {
        VaccineType = VaccineTypes.Arnm;
        Doses = new[] {
            new VaccineDose(2001, 0, 0),
            new VaccineDose(2002, 1, 0, 120) // 120 dias segunda dosis
        };
    }

    protected override Tuple<int?, string> internalValidation(Patient patient)
    {
        var alreadyApplied = patient.GetAlreadyAppliedVaccines(Id);
        if (alreadyApplied.Any())
        {
            if (alreadyApplied.Any(x => DateTime.Now >= x.AppliedDate.AddDays(120))) // tiene una dosis y la distancia es menor a 120 dias.
                return new Tuple<int?, string>(2002, "Segunda dosis aplicada"); // tiene una dosis, y la distancia es mayor a 120 dias.
            else
                return new Tuple<int?, string>(null, "Aun no se puede dar la segunda dosis");
        }
        return new Tuple<int?, string>(2001, "Primera dosis aplicada"); // no tiene ninguna dosis y es la primera.
    }
}
