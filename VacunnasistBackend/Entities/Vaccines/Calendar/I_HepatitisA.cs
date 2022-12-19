using System.Globalization;
using VacunnasistBackend.Entities;

namespace VacunassistBackend.Entities.Vaccines.Calendar;

public class I_HepatitisA : Vaccine
{
    public I_HepatitisA()
    : base(900, "Hepatitis A (HA)", VaccineTypeEnum.Calendar)
    {
        VaccineType = VaccineTypes.Arnm;
        Doses = new[] {
            new VaccineDose(901, 0, 12),
        };
    }

    protected override Tuple<int?, string> internalValidation(Patient patient)
    {
        var alreadyApplied = patient.GetAlreadyAppliedVaccines(Id);
        if (alreadyApplied.Any())
            return new Tuple<int?, string>(null, "Dosis �nica");

        var iDate = DateTime.ParseExact(patient.BirthDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        return (DateTime.Now >= iDate.AddMonths(12)) ? new Tuple<int?, string>(901, "Primera dosis aplicada") : new Tuple<int?, string>(null, "Aun no se puede dar la primera dosis. Seg�n esquema de vacunaci�n debe vacunarse a los 12 meses de vida");
    }
}
