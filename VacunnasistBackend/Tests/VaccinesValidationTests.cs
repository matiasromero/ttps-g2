
using NUnit.Framework;
using VacunnasistBackend.Entities;

namespace VacunassistBackend.Tests
{
    [TestFixture]
    public class VaccinesValidationTests
    {
        private Patient patient1;

        [SetUp]
        public void SetUp()
        {
            patient1 = new Patient()
            {
                DNI = "30000000",
                BirthDate = "01/05/1980",
                Name = "Joe",
                Surname = "Doe",
                Gender = "male",
                Id = 1,
                Province = "Buenos Aires"
            };
        }

        [Test]
        public void CheckValidation()
        {
            //...
            Assert.Fail("I failed"); // Introduce assertion failure
        }
    }
}