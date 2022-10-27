using Microsoft.EntityFrameworkCore;
using VacunassistBackend.Entities;
using VacunassistBackend.Entities.Vaccines;
using VacunassistBackend.Utils;

namespace VacunassistBackend.Data
{
    public class DataContext : DbContext
    {
        private IConfiguration Configuration { get; }

        public DataContext(DbContextOptions<DataContext> options, IConfiguration configuration) : base(options)
        {
            Configuration = configuration;
        }

        public DbSet<User> Users { get; set; }
        public DbSet<DevelopedVaccine> DevelopedVaccines { get; set; }
        public DbSet<AppliedVaccine> AppliedVaccines { get; set; }

        #region Required
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserEntityTypeConfiguration).Assembly);

            if (bool.Parse(Configuration.GetValue<String>("SeedDatabase", "false")))
            {
                var admin = new User
                {
                    Id = 1,
                    UserName = "Admin",
                    Role = UserRoles.Administrator,
                    Address = "Calle Falsa 1234, La Plata",
                    FullName = "Administrador",
                    BirthDate = DateTime.Now.Date,
                    DNI = "11111111",
                    Gender = Gender.Other,
                    Email = "admin@vacunassist.com",
                    Province = Province.BuenosAires,
                    IsActive = true,
                    PasswordHash = PasswordHash.CreateHash("1234")
                };

                var operador1 = new User
                {
                    Id = 2,
                    UserName = "Operador1",
                    Role = UserRoles.Operator,
                    Address = "Calle Falsa 2345, La Plata",
                    FullName = "Luis Gutierrez",
                    BirthDate = DateTime.Now.Date,
                    DNI = "22345678",
                    Gender = Gender.Male,
                    Email = "operador1@vacunassist.com",
                    Province = Province.BuenosAires,
                    IsActive = true,
                    PasswordHash = PasswordHash.CreateHash("1234")
                };

                var operador2 = new User
                {
                    Id = 3,
                    UserName = "Operador2",
                    Role = UserRoles.Operator,
                    Address = "Calle Falsa 9874, Salta",
                    FullName = "Estefania Borzi",
                    BirthDate = DateTime.Now.Date,
                    DNI = "89785451",
                    Gender = Gender.Female,
                    Email = "estefania@vacunassist.com",
                    Province = Province.Salta,
                    IsActive = true,
                    PasswordHash = PasswordHash.CreateHash("1234")
                };

                var analista1 = new User
                {
                    Id = 4,
                    UserName = "Analista1",
                    Role = UserRoles.Analyst,
                    Address = "Calle Falsa 9874, Salta",
                    FullName = "Jose Luis Rodriguez",
                    BirthDate = DateTime.Now.Date,
                    DNI = "89785451",
                    Gender = Gender.Male,
                    Email = "jr@vacunassist.com",
                    Province = Province.BuenosAires,
                    IsActive = true,
                    PasswordHash = PasswordHash.CreateHash("1234")
                };

                var vacunador1 = new User
                {
                    Id = 5,
                    UserName = "Vacunador",
                    Role = UserRoles.Vacunator,
                    Address = "Calle Falsa 4567, La Plata",
                    FullName = "Vacunador",
                    BirthDate = DateTime.Now.Date,
                    Province = Province.BuenosAires,
                    DNI = "11111111",
                    Gender = Gender.Other,
                    Email = "vacunador@email.com",
                    IsActive = true,
                    PasswordHash = PasswordHash.CreateHash("1234"),
                };

                var vaccine1 = new DevelopedVaccine
                {
                    Id = 1,
                    Name = "Pfizer COVID-19",
                    IsActive = true,
                    Vaccine = Vaccines.O_COVID,
                    DaysToDelivery = 30
                };
                var vaccine2 = new DevelopedVaccine
                {
                    Id = 2,
                    Name = "ROCHE Fiebre amarilla",
                    IsActive = true,
                    Vaccine = Vaccines.M_FiebreAmarilla,
                    DaysToDelivery = 60
                };
                var vaccine3 = new DevelopedVaccine
                {
                    Id = 3,
                    Name = "Fluarix Antigripal",
                    IsActive = true,
                    Vaccine = Vaccines.P_Antigripal,
                    DaysToDelivery = 15
                };

                modelBuilder.Entity<DevelopedVaccine>().HasData(vaccine1, vaccine2, vaccine3);
                modelBuilder.Entity<User>().HasData(admin, operador1, operador2, analista1, vacunador1);
            }
        }
        #endregion
    }
}
