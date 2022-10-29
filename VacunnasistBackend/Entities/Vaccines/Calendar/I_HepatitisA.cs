using VacunnasistBackend.Entities;

namespace VacunassistBackend.Entities.Vaccines.Calendar;

public class I_HepatitisA : Vaccine
{
    public I_HepatitisA()
    : base(900, "Hepatitis A (HA)", VaccineType.Calendar)
    {
        Doses = new[] {
            new VaccineDose(901, 0, 12),
        };
    }

    protected override int? internalValidation(Patient patient)
    {
        return null;
    }
}
