using Xunit;
using VacunnasistBackend.Entities;
using VacunassistBackend.Entities.Vaccines;
using VacunassistBackend.Entities;
using Moq;

namespace Tests;

public class ApplyRotaVaccineTest
{
    private Patient patient1;
    private Patient patient2;

    public ApplyRotaVaccineTest()
    {
        patient1 = new Patient()
        {
            Id = 1,
            Name = "Jose",
            Surname = "Martinez",
            BirthDate = "05/08/2022",
        };


        var mock = new Mock<Patient>();
        mock.Setup(m => m.GetAlreadyAppliedVaccines(It.IsAny<int>())).Returns(new AppliedVaccine[] {
                new AppliedVaccine() {
                    Id = 1,
                    AppliedDose = 601,
                    AppliedDate = new System.DateTime(2022,09,04)
                }
            });
        patient2 = mock.Object;
        patient2.BirthDate = "04/07/2022";
    }

    [Fact]
    public void PatientWithoutAnyDoseOfRotavirusShouldApplyToFirstDose()
    {
        var rota = Vaccines.F_Rotavirus;

        var result = rota.CanApply(patient1);

        Assert.True(result.Item1.HasValue);
        Assert.Equal(601, result.Item1);
        Assert.Equal("Primera dosis aplicada", result.Item2);
    }

    [Fact]
    public void PatientWithOneDoseOfRotavirusShouldApplyToSecondDose()
    {
        var rota = Vaccines.F_Rotavirus;

        var result = rota.CanApply(patient2);

        Assert.True(result.Item1.HasValue);
        Assert.Equal(602, result.Item1);
        Assert.Equal("Segunda dosis aplicada", result.Item2);
    }
}