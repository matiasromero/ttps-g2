using Xunit;
using VacunnasistBackend.Entities;
using VacunassistBackend.Entities.Vaccines;
using VacunassistBackend.Entities;
using Moq;

namespace Tests;

public class ApplyVaricelaVaccineTest
{
    private Patient patient1;
    private Patient patient2;

    public ApplyVaricelaVaccineTest()
    {
        patient1 = new Patient()
        {
            Id = 1,
            Name = "Joe",
            Surname = "Doe",
            BirthDate = "01/05/1980",
        };


        var mock = new Mock<Patient>();
        mock.Setup(m => m.GetAlreadyAppliedVaccines(It.IsAny<int>())).Returns(new AppliedVaccine[] {
                new AppliedVaccine() {
                    Id = 1,
                    AppliedDose = 1001,
                    AppliedDate = new System.DateTime(2017,01,31)
                }
            });
        patient2 = mock.Object;
        patient2.BirthDate = "01/12/2015";
    }

    [Fact]
    public void PatientWithoutAnyDoseOfVaricelaShouldApplyToFirstDose()
    {
        var varicela = Vaccines.J_Varicela;

        var result = varicela.CanApply(patient1);

        Assert.True(result.Item1.HasValue);
        Assert.Equal(1001, result.Item1);
        Assert.Equal("Primera dosis aplicada", result.Item2);
    }

    [Fact]
    public void PatientWithOneDoseOfVaricelaShouldApplyToSecondDose()
    {
        var varicela = Vaccines.J_Varicela;

        var result = varicela.CanApply(patient2);

        Assert.True(result.Item1.HasValue);
        Assert.Equal(1002, result.Item1);
        Assert.Equal("Segunda dosis aplicada", result.Item2);
    }
}