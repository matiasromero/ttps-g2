using Xunit;
using VacunnasistBackend.Entities;
using VacunassistBackend.Entities.Vaccines;
using VacunassistBackend.Entities;
using Moq;

namespace Tests;

public class ApplyHAVaccineTest
{
    private Patient patient1;
    private Patient patient2;

    public ApplyHAVaccineTest()
    {
        patient1 = new Patient()
        {
            Id = 1,
            Name = "Jose",
            Surname = "Martinez",
            BirthDate = "19/06/1998",
            Province = "Buenos Aires"
        };


        var mock = new Mock<Patient>();
        mock.Setup(m => m.GetAlreadyAppliedVaccines(It.IsAny<int>())).Returns(new AppliedVaccine[] {
                new AppliedVaccine() {
                    Id = 1,
                    AppliedDose = 901,
                    AppliedDate = new System.DateTime(2021,11,05)
                }
            });
        patient2 = mock.Object;
        patient2.BirthDate = "05/11/2020";
    }

    [Fact]
    public void PatientWithoutAnyDoseOfHAShouldApplyToFirstDose()
    {
        var ha = Vaccines.I_HepatitisA;

        var result = ha.CanApply(patient1);

        Assert.True(result.Item1.HasValue);
        Assert.Equal(901, result.Item1);
        Assert.Equal("Primera dosis aplicada", result.Item2);
    }

    [Fact]
    public void PatientWithOneDoseOfHAShouldNotApplyToSecondDose()
    {
        var ha = Vaccines.I_HepatitisA;

        var result = ha.CanApply(patient2);

        Assert.True(!result.Item1.HasValue);
        Assert.Equal("Dosis única", result.Item2);
    }
}