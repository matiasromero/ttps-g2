using Xunit;
using VacunnasistBackend.Entities;
using VacunassistBackend.Entities.Vaccines;
using VacunassistBackend.Entities;
using Moq;

namespace Tests;

public class ApplyTripleVaccineTest
{
    private Patient patient1;
    private Patient patient2;

    public ApplyTripleVaccineTest()
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
                    AppliedDose = 701,
                    AppliedDate = new System.DateTime(1999,11,05)
                }
            });
        patient2 = mock.Object;
        patient2.BirthDate = "05/11/1998";
    }

    [Fact]
    public void PatientWithoutAnyDoseOfTripleShouldApplyToFirstDose()
    {
        var triple = Vaccines.H_TripleViral;

        var result = triple.CanApply(patient1);

        Assert.True(result.Item1.HasValue);
        Assert.Equal(801, result.Item1);
        Assert.Equal("Primera dosis aplicada", result.Item2);
    }

    [Fact]
    public void PatientWithOneDoseOfTripleShouldApplyToSecondDose()
    {
        var triple = Vaccines.H_TripleViral;

        var result = triple.CanApply(patient2);

        Assert.True(result.Item1.HasValue);
        Assert.Equal(802, result.Item1);
        Assert.Equal("Segunda dosis aplicada", result.Item2);
    }
}