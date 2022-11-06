using Xunit;
using VacunnasistBackend.Entities;
using VacunassistBackend.Entities.Vaccines;
using VacunassistBackend.Entities;
using Moq;

namespace Tests;

public class ApplyVPHVaccineTest
{
    private Patient patient1;
    private Patient patient2;

    public ApplyVPHVaccineTest()
    {
        patient1 = new Patient()
        {
            Id = 1,
            Name = "Jose",
            Surname = "Martinez",
            BirthDate = "19/06/2009",
            Province = "Buenos Aires"
        };


        var mock = new Mock<Patient>();
        mock.Setup(m => m.GetAlreadyAppliedVaccines(It.IsAny<int>())).Returns(new AppliedVaccine[] {
                new AppliedVaccine() {
                    Id = 1,
                    AppliedDose = 1201,
                    AppliedDate = new System.DateTime(2021,10,19)
                }
            });
        patient2 = mock.Object;
        patient2.BirthDate = "05/09/2010";
    }

    [Fact]
    public void PatientWithoutAnyDoseOfVPHShouldApplyToFirstDose()
    {
        var vph = Vaccines.L_VPH;

        var result = vph.CanApply(patient1);

        Assert.True(result.Item1.HasValue);
        Assert.Equal(1201, result.Item1);
        Assert.Equal("Primera dosis aplicada", result.Item2);
    }

    [Fact]
    public void PatientWithOneDoseOfVPHShouldApplyToSecondDose()
    {
        var vph = Vaccines.L_VPH;

        var result = vph.CanApply(patient2);

        Assert.True(result.Item1.HasValue);
        Assert.Equal(1202, result.Item1);
        Assert.Equal("Segunda dosis aplicada", result.Item2);
    }
}