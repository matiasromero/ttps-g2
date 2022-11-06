using Xunit;
using VacunnasistBackend.Entities;
using VacunassistBackend.Entities.Vaccines;
using VacunassistBackend.Entities;
using Moq;

namespace Tests;

public class ApplyIPVVaccineTest
{
    private Patient patient1;
    private Patient patient2;
    private Patient patient3;

    public ApplyIPVVaccineTest()
    {
        patient1 = new Patient()
        {
            Id = 1,
            Name = "Joe",
            Surname = "Doe",
            BirthDate = "05/08/2022",
            Province = "Buenos Aires"
        };


        var mock = new Mock<Patient>();
        mock.Setup(m => m.GetAlreadyAppliedVaccines(It.IsAny<int>())).Returns(new AppliedVaccine[] {
                new AppliedVaccine() {
                    Id = 1,
                    AppliedDose = 501,
                    AppliedDate = new System.DateTime(2000,05,21)         
                }
            });
        patient2 = mock.Object;
        patient2.BirthDate = "19/03/2000";

        mock = new Mock<Patient>();
        mock.Setup(m => m.GetAlreadyAppliedVaccines(It.IsAny<int>())).Returns(new AppliedVaccine[] {
                new AppliedVaccine() {
                    Id = 1,
                    AppliedDose = 501,
                    AppliedDate = new System.DateTime(2016,12,05)
                },
                new AppliedVaccine() {
                    Id = 1,
                    AppliedDose = 502,
                    AppliedDate = new System.DateTime(2017,02,05)
                },
                new AppliedVaccine() {
                    Id = 1,
                    AppliedDose = 503,
                    AppliedDate = new System.DateTime(2017,04,05)
                }
            });
        patient3 = mock.Object;
        patient3.BirthDate = "04/10/2016";
    }

    [Fact]
    public void PatientWithoutAnyDoseOfIPVShouldApplyToFirstDose()
    {
        var polio = Vaccines.E_Polio;

        var result = polio.CanApply(patient1);

        Assert.True(result.Item1.HasValue);
        Assert.Equal(501, result.Item1);
        Assert.Equal("Primera dosis aplicada", result.Item2);
    }

    [Fact]
    public void PatientWithOneDoseOfIPVShouldApplyToSecondDose()
    {
        var polio = Vaccines.E_Polio;
        var result = polio.CanApply(patient2);

        Assert.True(result.Item1.HasValue);
        Assert.Equal(502, result.Item1);
        Assert.Equal("Segunda dosis aplicada", result.Item2);
    }

    [Fact]
    public void PatientShouldApplyReforce()
    {
        var polio = Vaccines.E_Polio;
        var result = polio.CanApply(patient3);

        Assert.True(result.Item1.HasValue);
        Assert.Equal(504, result.Item1);
        Assert.Equal("Refuerzo aplicado", result.Item2);
    }
}