using Xunit;
using VacunnasistBackend.Entities;
using VacunassistBackend.Entities.Vaccines;
using VacunassistBackend.Entities;
using Moq;

namespace Tests;

public class ApplyNeumoVaccineTest
{
    private Patient patient1;
    private Patient patient2;

    public ApplyNeumoVaccineTest()
    {
        patient1 = new Patient()
        {
            Id = 1,
            Name = "Jose",
            Surname = "Martinez",
            BirthDate = "05/08/2022",
            Province = "Buenos Aires"
        };


        var mock = new Mock<Patient>();
        mock.Setup(m => m.GetAlreadyAppliedVaccines(It.IsAny<int>())).Returns(new AppliedVaccine[] {
                new AppliedVaccine() {
                    Id = 1,
                    AppliedDose = 301,
                    AppliedDate = new System.DateTime(2022,09,05,9,15,0)
                }
            });
        patient2 = mock.Object;
        patient2.BirthDate = "05/07/2022";
    }

    [Fact]
    public void PatientWithoutAnyDoseOfNeumoShouldApplyToFirstDose()
    {
        var neumo = Vaccines.C_Neumococo;

        var result = neumo.CanApply(patient1);

        Assert.True(result.Item1.HasValue);
        Assert.Equal(301, result.Item1);
        Assert.Equal("Primera dosis aplicada", result.Item2);
    }

    [Fact]
    public void PatientWithOneDoseOfNeumoShouldApplyToSecondDose()
    {
        var neumo = Vaccines.C_Neumococo;

        var result = neumo.CanApply(patient2);

        Assert.True(result.Item1.HasValue);
        Assert.Equal(302, result.Item1);
        Assert.Equal("Segunda dosis aplicada", result.Item2);
    }
}