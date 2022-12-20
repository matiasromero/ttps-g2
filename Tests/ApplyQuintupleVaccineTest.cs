using Xunit;
using VacunnasistBackend.Entities;
using VacunassistBackend.Entities.Vaccines;
using VacunassistBackend.Entities;
using Moq;

namespace Tests;

public class ApplyQuintupleVaccineTest
{
    private Patient patient1;
    private Patient patient2;

    public ApplyQuintupleVaccineTest()
    {
        patient1 = new Patient()
        {
            Id = 1,
            Name = "Joe",
            Surname = "Doe",
            BirthDate = "13/08/2022",
        };


        var mock = new Mock<Patient>();
        mock.Setup(m => m.GetAlreadyAppliedVaccines(It.IsAny<int>())).Returns(new AppliedVaccine[] {
                new AppliedVaccine() {
                    Id = 1,
                    AppliedDose = 401,
                    AppliedDate = new System.DateTime(2022,10,13)
                }
            });
        patient2 = mock.Object;
        patient2.BirthDate = "13/08/2022";
    }

    [Fact]
    public void PatientWithoutAnyDoseOfQuintupleShouldApplyToFirstDose()
    {
        var quintuple = Vaccines.D_Quintuple;

        var result = quintuple.CanApply(patient1);

        Assert.True(result.Item1.HasValue);
        Assert.Equal(401, result.Item1);
        Assert.Equal("Primera dosis aplicada", result.Item2);
    }

    [Fact]
    public void PatientWithOneDoseOfQuintupleShouldNotApplyToSecondDose()
    {
        var quintuple = Vaccines.D_Quintuple;
        var result = quintuple.CanApply(patient2);

        Assert.True(!result.Item1.HasValue);
        Assert.Equal("Aun no se puede dar la segunda dosis. Deben pasar 2 meses de la primer dosis aplicada.", result.Item2);
    }
}