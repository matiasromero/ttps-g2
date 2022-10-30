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
        var alreadyApplied = patient.AppliedVaccines.Where(x => x.LocalBatchVaccine.BatchVaccine.DevelopedVaccine.Vaccine.Id == Id).ToArray();
        if (alreadyApplied.Any())
        {
            return null;    
        }
        
        return 101; //Nunca se la dió y es la primer vez.
    }
}
