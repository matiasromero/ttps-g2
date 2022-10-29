using VacunnasistBackend.Entities;

namespace VacunassistBackend.Entities.Vaccines.Calendar;

public class G_Meningococo : Vaccine
{
    public G_Meningococo()
    : base(700, "Meningococo", VaccineType.Calendar)
    {
        Doses = new[] {
            new VaccineDose(701, 0, 3),
            new VaccineDose(702, 1, 5, 60),
        };
    }

    protected override int? internalValidation(Patient patient)
    {
        return null;
    }
}
