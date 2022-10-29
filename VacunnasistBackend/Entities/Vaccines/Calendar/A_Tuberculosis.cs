using VacunnasistBackend.Entities;

namespace VacunassistBackend.Entities.Vaccines.Calendar;

public class A_Tuberculosis : Vaccine
{
    public A_Tuberculosis()
    : base(100, "Tubercolosis (BCG)", VaccineType.Calendar)
    {
        Doses = new[] {
            new VaccineDose(101, 0, 0)
        };
    }
    protected override int? internalValidation(Patient patient)
    {
        return null;
    }
}
