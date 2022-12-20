using Microsoft.EntityFrameworkCore;
using Quartz;
using Dapper;
using System.Data;
using Microsoft.Data.SqlClient;
using VacunassistBackend.Services;

namespace VacunassistBackend.Jobs
{
    public class SyncronizeDataJob : IJob
    {
        private readonly DataContext _context;
        private readonly IDepartmentsService _departmentsService;
        private readonly IConfiguration _configuration;

        public SyncronizeDataJob(DataContext context, IConfiguration configuration, IDepartmentsService departmentsService)
        {
            this._context = context;
            this._configuration = configuration;
            this._departmentsService = departmentsService;
        }

        public Task Execute(IJobExecutionContext context)
        {
            var localBatchVaccines = _context.LocalBatchVaccines
                .Include(x => x.BatchVaccine).ThenInclude(x => x.DevelopedVaccine)
                .Where(x => x.Synchronized == false && x.BatchVaccine.Status == Entities.BatchStatus.Overdue)
                .ToArray();
            var appliedVaccines = _context.AppliedVaccines
                .Include(x => x.LocalBatchVaccine).ThenInclude(x => x.BatchVaccine).ThenInclude(x => x.DevelopedVaccine)
                .Include(x => x.Patient).ThenInclude(x => x.Department).ThenInclude(x => x.Province)
                .Where(x => x.Synchronized == false)
                .ToArray();

            using (IDbConnection db = new SqlConnection(_configuration.GetConnectionString("DatawarehouseDefaultConnection")))
            {
                db.Open();

                foreach (var batch in localBatchVaccines)
                {
                    // Sync
                    var idFecha = GetOrCreateFecha(db, batch.BatchVaccine.DueDate.Year, batch.BatchVaccine.DueDate.Month, batch.BatchVaccine.DueDate.Day);

                    var department = _departmentsService.GetRandomFromProvince(batch.Province);
                    var idLugar = GetOrCreateLugar(db, batch.Province, department.Name);
                    var idVacuna = GetOrCreateVacuna(db, batch.BatchVaccine.DevelopedVaccine.VaccineType, batch.BatchVaccine.DevelopedVaccine.Name, batch.BatchVaccine.DevelopedVaccine.Vaccine.ShortName);

                    AddOverdueBatch(db, idFecha, idLugar, idVacuna);

                    batch.Synchronized = true;
                }

                foreach (var appliedVaccine in appliedVaccines)
                {
                    // Sync
                    var idFecha = GetOrCreateFecha(db, appliedVaccine.AppliedDate.Year, appliedVaccine.AppliedDate.Month, appliedVaccine.AppliedDate.Day);
                    var idLugar = GetOrCreateLugar(db, appliedVaccine.Patient.Department.Province.Name, appliedVaccine.Patient.Department.Name);
                    var idVacunado = GetOrCreateVacunado(db, appliedVaccine.Patient.DNI, appliedVaccine.Patient.Gender, appliedVaccine.Patient.GetAge());
                    var idVacuna = GetOrCreateVacuna(db, appliedVaccine.LocalBatchVaccine.BatchVaccine.DevelopedVaccine.VaccineType, appliedVaccine.LocalBatchVaccine.BatchVaccine.DevelopedVaccine.Name, appliedVaccine.LocalBatchVaccine.BatchVaccine.DevelopedVaccine.Vaccine.ShortName);

                    AddAppliedVaccine(db, idFecha, idLugar, idVacunado, idVacuna);

                    appliedVaccine.Synchronized = true;
                }
            }

            _context.SaveChanges();

            //Write your custom code here
            return Task.CompletedTask;
        }

        public void AddAppliedVaccine(IDbConnection db, int idFecha, int idLugar, int idVacunado, int idVacuna)
        {
            db.Execute("insert into HVacunacion (idVacunacion, idVacunado, idFecha, idLugar, idVacuna) values ((SELECT ISNULL(MAX(idVacunacion) + 1, 0) FROM HVacunacion), @idVacunado, @idFecha, @idLugar, @idVacuna)",
            new { idVacunado = idVacunado, idFecha = idFecha, idLugar = idLugar, idVacuna = idVacuna });
        }

        public void AddOverdueBatch(IDbConnection db, int idFecha, int idLugar, int idVacuna)
        {
            db.Execute("insert into HVencimiento (idVencimiento, idFecha, idLugar, idVacuna) values ((SELECT ISNULL(MAX(idVencimiento) + 1, 0) FROM HVencimiento), @idFecha, @idLugar, @idVacuna)",
            new { idFecha = idFecha, idLugar = idLugar, idVacuna = idVacuna });
        }

        public int GetOrCreateVacunado(IDbConnection db, string dni, string gender, int age)
        {
            var idVacunado = db.Query<int?>("SELECT idVacunado FROM DVacunado WHERE dni = @dni",
                    new { dni = dni })
                    .SingleOrDefault();
            if (idVacunado.HasValue == false)
            {
                db.Execute("insert into DVacunado (idVacunado, edadanio, edaddecada, edadveintena, dni, sexo) values ((SELECT ISNULL(MAX(idVacunado) + 1, 0) FROM DVacunado), @edadanio, @edaddecada, @edadveintena, @dni, @sexo)",
                new { edadanio = age, edaddecada = (age / 10) + 1, edadveintena = (age / 20) + 1, dni = dni, sexo = gender });
                idVacunado = db.Query<int?>("SELECT idVacunado FROM DVacunado WHERE dni = @dni",
                    new { dni = dni })
                    .SingleOrDefault();
            }
            return idVacunado.Value;
        }

        public int GetOrCreateVacuna(IDbConnection db, string type, string laboratory, string vaccine)
        {
            var idVacuna = db.Query<int?>("SELECT idVacuna FROM DVacuna WHERE name = @name AND laboratory = @laboratory AND type = @type",
                    new { name = vaccine, type = type, laboratory = laboratory })
                    .SingleOrDefault();
            if (idVacuna.HasValue == false)
            {
                db.Execute("insert into DVacuna (idVacuna, name, laboratory, type) values ((SELECT ISNULL(MAX(idVacuna) + 1, 0) FROM DVacuna), @name, @laboratory, @type)",
                new { name = vaccine, laboratory = laboratory, type = type });
                idVacuna = db.Query<int?>("SELECT idVacuna FROM DVacuna WHERE name = @name AND laboratory = @laboratory AND type = @type",
                    new { name = vaccine, type = type, laboratory = laboratory })
                    .SingleOrDefault();
            }
            return idVacuna.Value;
        }

        public int GetOrCreateFecha(IDbConnection db, int year, int mes, int dia)
        {
            var idFecha = db.Query<int?>("SELECT idFecha FROM DFecha WHERE year = @year AND mes = @mes AND dia = @dia",
                    new { year = (int?)year, mes = (int?)mes, dia = (int?)dia })
                    .SingleOrDefault();
            if (idFecha.HasValue == false)
            {
                db.Execute("insert into DFecha (idFecha, year, mes, dia) values ((SELECT ISNULL(MAX(idFecha) + 1, 0) FROM DFecha), @year, @mes, @dia)",
                new { year = year, mes = mes, dia = dia });
                idFecha = db.Query<int?>("SELECT idFecha FROM DFecha WHERE year = @year AND mes = @mes AND dia = @dia",
            new { year = (int?)year, mes = (int?)mes, dia = (int?)dia })
            .SingleOrDefault();
            }
            return idFecha.Value;
        }

        public int GetOrCreateLugar(IDbConnection db, string province, string department)
        {
            var idLugar = db.Query<int?>("SELECT idLugar FROM DLugar WHERE province = @province AND department = @department",
                    new { province = province, department = department })
                    .SingleOrDefault();
            if (idLugar.HasValue == false)
            {
                db.Execute("insert into DLugar (idLugar, province, department) values ((SELECT ISNULL(MAX(idLugar) + 1, 0) FROM DLugar), @province, @department)",
                new { province = province, department = department });
                idLugar = db.Query<int?>("SELECT idLugar FROM DLugar WHERE province = @province AND department = @department",
                        new { province = province, department = department })
                        .SingleOrDefault();
            }
            return idLugar.Value;
        }
    }
}