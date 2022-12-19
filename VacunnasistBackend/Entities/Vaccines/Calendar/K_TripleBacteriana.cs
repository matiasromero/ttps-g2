using System.Globalization;
using VacunnasistBackend.Entities;

namespace VacunassistBackend.Entities.Vaccines.Calendar;

public class K_TripleBacteriana : Vaccine
{
    public K_TripleBacteriana()
    : base(1100, "Triple Bacteriana (DTP)", VaccineTypeEnum.Calendar)
    {
        VaccineType = VaccineTypes.Vector;
        Doses = new[] {
            new VaccineDose(1101, 0, 132),
        };
    }

    protected override Tuple<int?, string> internalValidation(Patient patient)
    {
        var alreadyApplied = patient.GetAlreadyAppliedVaccines(Id);
        if (alreadyApplied.Any() && (!patient.Pregnant || !patient.HealthWorker))
            return new Tuple<int?, string>(null, "Ya posee el esquema completo");

        var iDate = DateTime.ParseExact(patient.BirthDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        return (DateTime.Now >= iDate.AddMonths(132) || patient.Pregnant || patient.HealthWorker) ? new Tuple<int?, string>(1101, "Primera dosis aplicada") : new Tuple<int?, string>(null, "Aun no se puede aplicar la primera dosis. Seg�n esquema de vacunaci�n se aplica a partir d elos 5 a�os.");
    }
}
