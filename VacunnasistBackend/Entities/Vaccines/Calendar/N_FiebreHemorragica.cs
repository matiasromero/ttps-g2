using VacunnasistBackend.Entities;

namespace VacunassistBackend.Entities.Vaccines.Calendar;

public class N_FiebreHemorragica : Vaccine
{
    public N_FiebreHemorragica()
    : base(1400, "Fiebre Hemorragica (FHA)", VaccineType.Calendar)
    {
        Doses = new[] {
            new VaccineDose(1401, 0, 132)
        };
    }

    protected override int? internalValidation(Patient patient)
    {
        return null;
    }
}
