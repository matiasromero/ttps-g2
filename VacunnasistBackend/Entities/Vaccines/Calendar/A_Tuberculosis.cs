using VacunnasistBackend.Entities;

namespace VacunassistBackend.Entities.Vaccines.Calendar;

public class A_Tuberculosis : Vaccine
{
    public A_Tuberculosis()
    : base(100, "Tubercolosis (BCG)", VaccineTypeEnum.Calendar)
    {
        VaccineType = VaccineTypes.Arnm;
        Doses = new[] {
            new VaccineDose(101, 0, 0)
        };
    }
    protected override Tuple<int?, string> internalValidation(Patient patient)
    {
        var alreadyApplied = patient.GetAlreadyAppliedVaccines(Id);
        if (alreadyApplied.Any())
        {
            return new Tuple<int?, string>(null, "Ya se aplicó una dosis");
        }

        return new Tuple<int?, string>(101, "Primera dosis aplicada"); //Nunca se la dio y es la primer vez.
    }
}
