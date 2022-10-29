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
        public DbSet<BatchVaccine> BatchVaccines { get; set; }
        public DbSet<LocalBatchVaccine> LocalBatchVaccines { get; set; }
        public DbSet<AppliedVaccine> AppliedVaccines { get; set; }
        public DbSet<PurchaseOrder> PurchaseOrders { get; set; }

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

                var po1 = new PurchaseOrder("FLU140012580", 1400, vaccine3.Id)
                {
                    Id = 1
                };
                var po2 = new PurchaseOrder("FLU140012581", 1200, vaccine3.Id)
                {
                    Id = 2
                };

                var po3 = new PurchaseOrder("PF1000001", 800, vaccine1.Id)
                {
                    Id = 3,
                    PurchaseDate = DateTime.Now.Date.AddDays(-1 * vaccine1.DaysToDelivery),
                    DeliveredTime = DateTime.Now.Date,
                    ETA = DateTime.Now.Date,
                    Status = PurchaseStatus.Delivered
                };
                var po4 = new PurchaseOrder("PF1000121", 400, vaccine1.Id)
                {
                    Id = 4,
                    PurchaseDate = DateTime.Now.Date.AddDays(-1 * vaccine1.DaysToDelivery),
                    DeliveredTime = DateTime.Now.Date,
                    ETA = DateTime.Now.Date,
                    Status = PurchaseStatus.Delivered
                };
                var po5 = new PurchaseOrder("R1000001", 560, vaccine2.Id)
                {
                    Id = 5,
                    PurchaseDate = DateTime.Now.Date.AddDays(-1 * vaccine2.DaysToDelivery),
                    DeliveredTime = DateTime.Now.Date,
                    ETA = DateTime.Now.Date,
                    Status = PurchaseStatus.Delivered
                };
                var po6 = new PurchaseOrder("FLU12214001", 1500, vaccine3.Id)
                {
                    Id = 6,
                    PurchaseDate = DateTime.Now.Date.AddDays(-1 * vaccine3.DaysToDelivery),
                    DeliveredTime = DateTime.Now.Date,
                    ETA = DateTime.Now.Date,
                    Status = PurchaseStatus.Delivered
                };
                var po7 = new PurchaseOrder("FLU12214003", 3600, vaccine3.Id)
                {
                    Id = 7,
                    PurchaseDate = DateTime.Now.Date.AddDays(-1 * vaccine3.DaysToDelivery),
                    DeliveredTime = DateTime.Now.Date,
                    ETA = DateTime.Now.Date,
                    Status = PurchaseStatus.Delivered
                };
                var po8 = new PurchaseOrder("FLU13214121", 3600, vaccine3.Id)
                {
                    Id = 8,
                    PurchaseDate = DateTime.Now.Date.AddDays((-1 * vaccine3.DaysToDelivery) - 18),
                    DeliveredTime = DateTime.Now.Date.AddDays(-18),
                    ETA = DateTime.Now.Date.AddDays(-18),
                    Status = PurchaseStatus.Delivered
                };

                var batch1 = new BatchVaccine("PF1000001", 800)
                {
                    Id = 1,
                    DevelopedVaccineId = vaccine1.Id,
                    DueDate = DateTime.Now.AddDays(33).Date,
                    PurchaseOrderId = po3.Id
                };
                var batch2 = new BatchVaccine("PF1000121", 400)
                {
                    Id = 2,
                    DevelopedVaccineId = vaccine1.Id,
                    DueDate = DateTime.Now.AddDays(13).Date,
                    PurchaseOrderId = po4.Id
                };
                var batch3 = new BatchVaccine("R1000001", 560)
                {
                    Id = 3,
                    DevelopedVaccineId = vaccine2.Id,
                    DueDate = DateTime.Now.AddDays(53).Date,
                    PurchaseOrderId = po5.Id
                };
                var batch4 = new BatchVaccine("FLU12214001", 1500)
                {
                    Id = 4,
                    DevelopedVaccineId = vaccine3.Id,
                    DueDate = DateTime.Now.AddDays(8).Date,
                    PurchaseOrderId = po6.Id
                };
                var batch5 = new BatchVaccine("FLU12214003", 3600)
                {
                    Id = 5,
                    DevelopedVaccineId = vaccine3.Id,
                    DueDate = DateTime.Now.AddDays(98).Date,
                    PurchaseOrderId = po7.Id,
                    RemainingQuantity = 3200
                };
                var batch6 = new BatchVaccine("FLU13214121", 3600)
                {
                    Id = 6,
                    DevelopedVaccineId = vaccine3.Id,
                    DueDate = DateTime.Now.AddDays(-18).Date,
                    PurchaseOrderId = po8.Id
                };

                var localBatch1 = new LocalBatchVaccine(600, Province.BuenosAires, batch5.Id)
                {
                    Id = 1
                };

                modelBuilder.Entity<DevelopedVaccine>().HasData(vaccine1, vaccine2, vaccine3);
                modelBuilder.Entity<PurchaseOrder>().HasData(po1, po2, po3, po4, po5, po6, po7, po8);
                modelBuilder.Entity<LocalBatchVaccine>().HasData(localBatch1);
                modelBuilder.Entity<BatchVaccine>().HasData(batch1, batch2, batch3, batch4, batch5, batch6);
                modelBuilder.Entity<User>().HasData(admin, operador1, operador2, analista1, vacunador1);
            }
        }
        #endregion
    }
}
