using Xunit;
using VacunnasistBackend.Entities;
using VacunassistBackend.Entities.Vaccines;
using VacunassistBackend.Entities;
using Moq;

namespace Tests;

public class ApplyDTPVaccineTest
{
    private Patient patient1;
    private Patient patient2;
    private Patient patient3;

    public ApplyDTPVaccineTest()
    {
        patient1 = new Patient()
        {
            Id = 1,
            Name = "Joe",
            Surname = "Doe",
            BirthDate = "01/05/2009",
            Province = "Buenos Aires"
        };


        var mock = new Mock<Patient>();
        mock.Setup(m => m.GetAlreadyAppliedVaccines(It.IsAny<int>())).Returns(new AppliedVaccine[] {
                new AppliedVaccine() {
                    Id = 1,
                    AppliedDose = 1001,
                    AppliedDate = new System.DateTime(2022,01,31)
                }
            });
        patient2 = mock.Object;
        patient2.BirthDate = "01/12/2009";

        patient3 = new Patient()
        {
            Id = 1,
            Name = "Joe",
            Surname = "Doe",
            BirthDate = "01/05/1980",
            Province = "Buenos Aires",
            HealthWorker = true
        };
    }

    [Fact]
    public void PatientWithoutAnyDoseOfVaricelaShouldApplyToFirstDose()
    {
        var dtp = Vaccines.K_TripleBacteriana;

        var result = dtp.CanApply(patient1);

        Assert.True(result.Item1.HasValue);
        Assert.Equal(1101, result.Item1);
        Assert.Equal("Primera dosis aplicada", result.Item2);
    }

    [Fact]
    public void PatientWithOneDoseOfDTPShouldNotApplyToSecondDose()
    {
        var dtp = Vaccines.K_TripleBacteriana;
        var result = dtp.CanApply(patient2);

        Assert.True(!result.Item1.HasValue);
    }

    [Fact]
    public void PatientHealthWorkerWithAnyDoseOfDTPShouldApplyToDose()
    {
        var dtp = Vaccines.K_TripleBacteriana;

        var result = dtp.CanApply(patient1);

        Assert.True(result.Item1.HasValue);
        Assert.Equal(1101, result.Item1);
        Assert.Equal("Primera dosis aplicada", result.Item2);
    }
}