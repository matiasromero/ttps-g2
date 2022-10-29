using VacunnasistBackend.Entities;

namespace VacunassistBackend.Entities.Vaccines.Calendar;

public class F_Rotavirus : Vaccine
{
    public F_Rotavirus()
    : base(600, "Rotavirus", VaccineType.Calendar)
    {
        Doses = new[] {
            new VaccineDose(601, 0, 2),
            new VaccineDose(602, 1, 4, 60),
        };
    }

    protected override int? internalValidation(Patient patient)
    {
        return null;
    }
}
