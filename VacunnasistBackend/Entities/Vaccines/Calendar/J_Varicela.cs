using VacunnasistBackend.Entities;

namespace VacunassistBackend.Entities.Vaccines.Calendar;

public class J_Varicela : Vaccine
{
    public J_Varicela()
    : base(1000, "Varicela", VaccineType.Calendar)
    {
        Doses = new[] {
            new VaccineDose(1001, 0, 15),
            new VaccineDose(1002, 1, 60),
        };
    }

    protected override int? internalValidation(Patient patient)
    {
        return null;
    }
}
