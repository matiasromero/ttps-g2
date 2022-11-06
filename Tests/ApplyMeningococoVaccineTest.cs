using Xunit;
using VacunnasistBackend.Entities;
using VacunassistBackend.Entities.Vaccines;
using VacunassistBackend.Entities;
using Moq;

namespace Tests;

public class ApplyMeningococoVaccineTest
{
    private Patient patient1;
    private Patient patient2;

    public ApplyMeningococoVaccineTest()
    {
        patient1 = new Patient()
        {
            Id = 1,
            Name = "Jose",
            Surname = "Martinez",
            BirthDate = "01/08/2022",
            Province = "Buenos Aires"
        };


        var mock = new Mock<Patient>();
        mock.Setup(m => m.GetAlreadyAppliedVaccines(It.IsAny<int>())).Returns(new AppliedVaccine[] {
                new AppliedVaccine() {
                    Id = 1,
                    AppliedDose = 701,
                    AppliedDate = new System.DateTime(2022,09,04)
                }
            });
        patient2 = mock.Object;
        patient2.BirthDate = "04/06/2022";
    }

    [Fact]
    public void PatientWithoutAnyDoseOfRotavirusShouldApplyToFirstDose()
    {
        var menin = Vaccines.G_Meningococo;

        var result = menin.CanApply(patient1);

        Assert.True(result.Item1.HasValue);
        Assert.Equal(701, result.Item1);
        Assert.Equal("Primera dosis aplicada", result.Item2);
    }

    [Fact]
    public void PatientWithOneDoseOfRotavirusShouldApplyToSecondDose()
    {
        var menin = Vaccines.G_Meningococo;

        var result = menin.CanApply(patient2);

        Assert.True(result.Item1.HasValue);
        Assert.Equal(702, result.Item1);
        Assert.Equal("Segunda dosis aplicada", result.Item2);
    }
}