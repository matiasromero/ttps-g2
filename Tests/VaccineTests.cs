using Xunit;
using VacunnasistBackend.Entities;
using VacunassistBackend.Entities.Vaccines;
using VacunassistBackend.Entities;
using Moq;

namespace Tests;

public class VaccineTests
{
    private Patient patient1;
    private Patient patient2;

    public VaccineTests()
    {
        patient1 = new Patient()
        {
            Id = 1,
            Name = "Joe",
            Surname = "Doe",
            BirthDate = "01/05/1980",
            Province = "Buenos Aires"
        };


        var mock = new Mock<Patient>();
        mock.Setup(m => m.GetAlreadyAppliedVaccines(It.IsAny<int>())).Returns(new AppliedVaccine[] {
                new AppliedVaccine() {
                    Id = 1,
                    AppliedDose = 201
                }
            });
        patient2 = mock.Object;
        patient2.BirthDate = "01/05/1980";
    }

    [Fact]
    public void PatientWithoutAnyDoseOfHepatitisShouldApplyToFirstDose()
    {
        var hepatitis = Vaccines.B_HepatitisB;

        var result = hepatitis.CanApply(patient1);

        Assert.True(result.Item1.HasValue);
        Assert.Equal(201, result.Item1);
        Assert.Equal("Primera dosis aplicada", result.Item2);
    }

    [Fact]
    public void PatientWithOneDoseOfHepatitisShouldApplyToSecondDose()
    {
        var hepatitis = Vaccines.B_HepatitisB;

        var result = hepatitis.CanApply(patient2);

        Assert.True(result.Item1.HasValue);
        Assert.Equal(203, result.Item1);
        Assert.Equal("Segunda dosis aplicada", result.Item2);
    }
}