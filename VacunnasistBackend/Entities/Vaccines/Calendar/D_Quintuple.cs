using VacunnasistBackend.Entities;

namespace VacunassistBackend.Entities.Vaccines.Calendar;

public class D_Quintuple : Vaccine
{
    public D_Quintuple()
    : base(400, "Quíntuple pentavalente", VaccineType.Calendar)
    {
        Doses = new[] {
            new VaccineDose(401, 0, 2),
            new VaccineDose(402, 1, 4, 60),
            new VaccineDose(403, 2, 6, 60),
            new VaccineDose(404, 3, 15, null, true) // 15 meses, refuerzo
        };
    }

    protected override int? internalValidation(Patient patient)
    {
        return null;
    }
}
