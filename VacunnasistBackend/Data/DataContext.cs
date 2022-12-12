using Microsoft.EntityFrameworkCore;
using VacunassistBackend.Entities;
using VacunassistBackend.Entities.Vaccines;
using VacunassistBackend.Utils;
using VacunnasistBackend.Entities;

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
        public DbSet<Province> Provinces { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Laboratory> Laboratories { get; set; }

        #region Required
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserEntityTypeConfiguration).Assembly);

            if (bool.Parse(Configuration.GetValue<String>("SeedDatabase", "false")))
            {
                #region Laboratories
                var lab1 = new Laboratory()
                {
                    Id = 1,
                    Name = "Pfizer"
                };
                var lab2 = new Laboratory()
                {
                    Id = 2,
                    Name = "ROCHE"
                };
                var lab3 = new Laboratory()
                {
                    Id = 3,
                    Name = "BioNTech"
                };
                var lab4 = new Laboratory()
                {
                    Id = 4,
                    Name = "Janssen"
                };
                #endregion

                #region Users
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
                    Province = ProvinceEnum.BuenosAires,
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
                    Province = ProvinceEnum.BuenosAires,
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
                    Province = ProvinceEnum.Salta,
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
                    Province = ProvinceEnum.BuenosAires,
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
                    Province = ProvinceEnum.BuenosAires,
                    DNI = "11111111",
                    Gender = Gender.Other,
                    Email = "vacunador@email.com",
                    IsActive = true,
                    PasswordHash = PasswordHash.CreateHash("1234"),
                };
                #endregion

                #region Vaccines
                var vaccine1 = new DevelopedVaccine
                {
                    Id = 1,
                    Name = "Pfizer COVID-19",
                    IsActive = true,
                    Vaccine = Vaccines.O_COVID,
                    DaysToDelivery = 30,
                    Type = DevelopedVaccineType.ARNM,
                    LaboratoryId = lab1.Id
                };

                var vaccine2 = new DevelopedVaccine
                {
                    Id = 2,
                    Name = "ROCHE Fiebre amarilla",
                    IsActive = true,
                    Vaccine = Vaccines.M_FiebreAmarilla,
                    DaysToDelivery = 60,
                    Type = DevelopedVaccineType.Vector_viral,
                    LaboratoryId = lab2.Id
                };

                var vaccine3 = new DevelopedVaccine
                {
                    Id = 3,
                    Name = "Janssen Antigripal",
                    IsActive = true,
                    Vaccine = Vaccines.P_Antigripal,
                    DaysToDelivery = 15,
                    Type = DevelopedVaccineType.Vector_viral,
                    LaboratoryId = lab4.Id
                };
                #endregion

                #region Purchase Orders
                var po1 = new PurchaseOrder(1400, vaccine3.Id)
                {
                    Id = 1,
                };
                var po2 = new PurchaseOrder(1200, vaccine3.Id)
                {
                    Id = 2,
                };

                var po3 = new PurchaseOrder(800, vaccine1.Id)
                {
                    Id = 3,
                    PurchaseDate = DateTime.Now.Date.AddDays(-1 * vaccine1.DaysToDelivery),
                    DeliveredTime = DateTime.Now.Date,
                    ETA = DateTime.Now.Date,
                    Status = PurchaseStatus.Delivered,
                    BatchNumber = "PF1000001"
                };
                var po4 = new PurchaseOrder(400, vaccine1.Id)
                {
                    Id = 4,
                    PurchaseDate = DateTime.Now.Date.AddDays(-1 * vaccine1.DaysToDelivery),
                    DeliveredTime = DateTime.Now.Date,
                    ETA = DateTime.Now.Date,
                    Status = PurchaseStatus.Delivered,
                    BatchNumber = "PF1000121"
                };
                var po5 = new PurchaseOrder(560, vaccine2.Id)
                {
                    Id = 5,
                    PurchaseDate = DateTime.Now.Date.AddDays(-1 * vaccine2.DaysToDelivery),
                    DeliveredTime = DateTime.Now.Date,
                    ETA = DateTime.Now.Date,
                    Status = PurchaseStatus.Delivered,
                    BatchNumber = "R1000001"
                };
                var po6 = new PurchaseOrder(1500, vaccine3.Id)
                {
                    Id = 6,
                    PurchaseDate = DateTime.Now.Date.AddDays(-1 * vaccine3.DaysToDelivery),
                    DeliveredTime = DateTime.Now.Date,
                    ETA = DateTime.Now.Date,
                    Status = PurchaseStatus.Delivered,
                    BatchNumber = "FLU12214001"
                };
                var po7 = new PurchaseOrder(3600, vaccine3.Id)
                {
                    Id = 7,
                    PurchaseDate = DateTime.Now.Date.AddDays(-1 * vaccine3.DaysToDelivery),
                    DeliveredTime = DateTime.Now.Date,
                    ETA = DateTime.Now.Date,
                    Status = PurchaseStatus.Delivered,
                    BatchNumber = "FLU12214003"
                };
                var po8 = new PurchaseOrder(3600, vaccine3.Id)
                {
                    Id = 8,
                    PurchaseDate = DateTime.Now.Date.AddDays((-1 * vaccine3.DaysToDelivery) - 18),
                    DeliveredTime = DateTime.Now.Date.AddDays(-18),
                    ETA = DateTime.Now.Date.AddDays(-18),
                    Status = PurchaseStatus.Delivered,
                    BatchNumber = "FLU13214121"
                };
                #endregion

                #region Batches
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
                    RemainingQuantity = 3000
                };
                var batch6 = new BatchVaccine("FLU13214121", 3600)
                {
                    Id = 6,
                    DevelopedVaccineId = vaccine3.Id,
                    DueDate = DateTime.Now.AddDays(-18).Date,
                    PurchaseOrderId = po8.Id
                };


                var localBatch1 = new LocalBatchVaccine(600, ProvinceEnum.BuenosAires, batch5.Id)
                {
                    Id = 1
                };
                #endregion

                #region Provinces
                var prov1 = new Province()
                {
                    Id = 1,
                    Code = "02",
                    Name = "Ciudad Autónoma de Buenos Aires"
                };
                var prov2 = new Province()
                {
                    Id = 2,
                    Code = "06",
                    Name = "Buenos Aires"
                };
                var prov3 = new Province()
                {
                    Id = 3,
                    Code = "10",
                    Name = "Catamarca"
                };
                var prov4 = new Province()
                {
                    Id = 4,
                    Code = "14",
                    Name = "Córdoba"
                };
                var prov5 = new Province()
                {
                    Id = 5,
                    Code = "18",
                    Name = "Corrientes"
                };
                var prov6 = new Province()
                {
                    Id = 6,
                    Code = "22",
                    Name = "Chaco"
                };
                var prov7 = new Province()
                {
                    Id = 7,
                    Code = "26",
                    Name = "Chubut"
                };
                var prov8 = new Province()
                {
                    Id = 8,
                    Code = "30",
                    Name = "Entre Ríos"
                };
                var prov9 = new Province()
                {
                    Id = 9,
                    Code = "34",
                    Name = "Formosa"
                };
                var prov10 = new Province()
                {
                    Id = 10,
                    Code = "38",
                    Name = "Jujuy"
                };
                var prov11 = new Province()
                {
                    Id = 11,
                    Code = "42",
                    Name = "La Pampa"
                };
                var prov12 = new Province()
                {
                    Id = 12,
                    Code = "46",
                    Name = "La Rioja"
                };
                var prov13 = new Province()
                {
                    Id = 13,
                    Code = "50",
                    Name = "Mendoza"
                };
                var prov14 = new Province()
                {
                    Id = 14,
                    Code = "54",
                    Name = "Misiones"
                };
                var prov15 = new Province()
                {
                    Id = 15,
                    Code = "58",
                    Name = "Neuquén"
                };
                var prov16 = new Province()
                {
                    Id = 16,
                    Code = "62",
                    Name = "Río Negro"
                };
                var prov17 = new Province()
                {
                    Id = 17,
                    Code = "66",
                    Name = "Salta"
                };
                var prov18 = new Province()
                {
                    Id = 18,
                    Code = "70",
                    Name = "San Juan"
                };
                var prov19 = new Province()
                {
                    Id = 19,
                    Code = "74",
                    Name = "San Luis"
                };
                var prov20 = new Province()
                {
                    Id = 20,
                    Code = "78",
                    Name = "Santa Cruz"
                };
                var prov21 = new Province()
                {
                    Id = 21,
                    Code = "82",
                    Name = "Santa Fe"
                };
                var prov22 = new Province()
                {
                    Id = 22,
                    Code = "86",
                    Name = "Santiago del Estero"
                };
                var prov23 = new Province()
                {
                    Id = 23,
                    Code = "90",
                    Name = "Tucumán"
                };
                var prov24 = new Province()
                {
                    Id = 24,
                    Code = "94",
                    Name = "Tierra del Fuego, Antártida e Islas del Atlántico Sur"
                };
                #endregion

                modelBuilder.Entity<Laboratory>().HasData(lab1, lab2, lab3, lab4);
                modelBuilder.Entity<Province>().HasData(prov1, prov2, prov3, prov4, prov5, prov6, prov7, prov8, prov9, prov10, prov11, prov12, prov13, prov14, prov15, prov16, prov17, prov18, prov19, prov20, prov21, prov22, prov23, prov24);
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
