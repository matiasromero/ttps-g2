using VacunnasistBackend.Entities;

namespace VacunassistBackend.Entities.Vaccines.Calendar;

public class C_Neumococo : Vaccine
{
    public C_Neumococo()
    : base(300, "Neumococo conjugado", VaccineType.Calendar)
    {
        Doses = new[] {
            new VaccineDose(301, 0, 2),
            new VaccineDose(302, 2, null, 60),
            new VaccineDose(303, 3, 8, null, true) // 8 meses, refuerzo
        };
    }

    protected override int? internalValidation(Patient patient)
    {
        return null;
    }
}
