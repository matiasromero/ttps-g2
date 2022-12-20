using Xunit;
using VacunnasistBackend.Entities;
using VacunassistBackend.Entities.Vaccines;
using VacunassistBackend.Entities;
using Moq;

namespace Tests;

public class ApplyBCGVaccineTest
{
    private Patient patient1;
    private Patient patient2;

    public ApplyBCGVaccineTest()
    {
        patient1 = new Patient()
        {
            Id = 1,
            Name = "Joe",
            Surname = "Doe",
            BirthDate = "05/11/2022"
        };


        var mock = new Mock<Patient>();
        mock.Setup(m => m.GetAlreadyAppliedVaccines(It.IsAny<int>())).Returns(new AppliedVaccine[] {
                new AppliedVaccine() {
                    Id = 1,
                    AppliedDose = 101
                }
            });
        patient2 = mock.Object;
        patient2.BirthDate = "05/10/2000";
    }

    [Fact]
    public void PatientWithoutAnyDoseOfBCGShouldApplyToFirstDose()
    {
        var bcg = Vaccines.A_Tuberculosis;

        var result = bcg.CanApply(patient1);

        Assert.True(result.Item1.HasValue);
        Assert.Equal(101, result.Item1);
        Assert.Equal("Primera dosis aplicada", result.Item2);
    }

    [Fact]
    public void PatientWithOneDoseOfBCGShouldNotApplyToSecondDose()
    {
        var bcg = Vaccines.A_Tuberculosis;

        var result = bcg.CanApply(patient2);

        Assert.True(!result.Item1.HasValue);
        Assert.Equal("Ya se aplic� una dosis", result.Item2);
    }
}