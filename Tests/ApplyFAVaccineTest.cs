using Xunit;
using VacunnasistBackend.Entities;
using VacunassistBackend.Entities.Vaccines;
using VacunassistBackend.Entities;
using Moq;

namespace Tests;

public class ApplyFAVaccineTest
{
    private Patient patient1;
    private Patient patient2;

    public ApplyFAVaccineTest()
    {
        patient1 = new Patient()
        {
            Id = 1,
            Name = "Jose",
            Surname = "Martinez",
            BirthDate = "19/06/1998",
        };


        var mock = new Mock<Patient>();
        mock.Setup(m => m.GetAlreadyAppliedVaccines(It.IsAny<int>())).Returns(new AppliedVaccine[] {
                new AppliedVaccine() {
                    Id = 1,
                    AppliedDose = 1301,
                    AppliedDate = new System.DateTime(2011,09,05)
                }
            });
        patient2 = mock.Object;
        patient2.BirthDate = "05/11/2009";
    }

    [Fact]
    public void PatientWithoutAnyDoseOfFiebreAmarillaShouldApplyToFirstDose()
    {
        var fa = Vaccines.M_FiebreAmarilla;

        var result = fa.CanApply(patient1);

        Assert.True(result.Item1.HasValue);
        Assert.Equal(1301, result.Item1);
        Assert.Equal("Primera dosis aplicada", result.Item2);
    }

    [Fact]
    public void PatientWithOneDoseOfFiebreAmarillaShouldApplyToSecondDose()
    {
        var fa = Vaccines.M_FiebreAmarilla;

        var result = fa.CanApply(patient2);

        Assert.True(result.Item1.HasValue);
        Assert.Equal(1302, result.Item1);
        Assert.Equal("Segunda dosis aplicada", result.Item2);
    }
}