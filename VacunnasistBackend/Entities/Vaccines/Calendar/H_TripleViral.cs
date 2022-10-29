using VacunnasistBackend.Entities;

namespace VacunassistBackend.Entities.Vaccines.Calendar;

public class H_TripleViral : Vaccine
{
    public H_TripleViral()
    : base(800, "Triple (SRP)", VaccineType.Calendar)
    {
        Doses = new[] {
            new VaccineDose(801, 0, 12),
            new VaccineDose(802, 1, 60, 1440),
        };
    }

    protected override int? internalValidation(Patient patient)
    {
        return null;
    }
}
