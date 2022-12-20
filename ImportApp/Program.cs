using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Globalization;
using Dapper;

namespace ImportApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            //LOAD    
            //Created a temporary dataset to hold the records    

            using (IDbConnection db = new SqlConnection("server=172.18.0.2;database=vacunassist_datawarehouse;User Id=sa;Password=Inge2022!;"))
            // using (IDbConnection db = new SqlConnection("Server=tcp:vacunassist.database.windows.net,1433;Initial Catalog=Vacunassist-datawarehouse;Persist Security Info=False;User ID=vacunassist;Password=Admin2022*;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"))
            {
                db.Open();
                using (var reader = new StreamReader(@"//home/matias/development/temp/1m.csv"))
                {
                    if (!reader.EndOfStream)
                        reader.ReadLine();
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        var values = line.Split(',');

                        var vacuna = values[2].Split('_')[0];
                        var laboratorio = values[2].Split('_')[1];
                        var tipo = (vacuna == "antigripal" ? "Vector viral" : (vacuna == "anticovid" ? "ARNM" : "subunidades proteicas"));
                        var provincia = values[4];
                        var departamento = values[5];
                        var fechaAplicacion = DateTime.ParseExact(values[6], "yyyy-MM-dd", CultureInfo.InvariantCulture);
                        var dni = values[7];
                        var fechaNacimiento = DateTime.ParseExact(values[8], "yyyy-MM-dd", CultureInfo.InvariantCulture);
                        int age = (int)(DateTime.Today - fechaNacimiento).TotalDays;
                        age = age / 365;
                        var sexo = values[9];

                        var idFecha = GetOrCreateFecha(db, fechaAplicacion.Year, fechaAplicacion.Month, fechaAplicacion.Day);
                        var idLugar = GetOrCreateLugar(db, provincia, departamento);
                        var idVacuna = GetOrCreateVacuna(db, tipo, laboratorio, vacuna);
                        var idVacunado = GetOrCreateVacunado(db, dni, sexo, age);

                        AddAppliedVaccine(db, idFecha, idLugar, idVacunado, idVacuna);
                    }
                }
            }
            stopwatch.Stop();
            Console.WriteLine($"Time ellapsed {stopwatch.ElapsedMilliseconds / 1000}");
            Console.ReadLine();
        }

        public static void AddAppliedVaccine(IDbConnection db, int idFecha, int idLugar, int idVacunado, int idVacuna)
        {
            db.Execute("insert into HVacunacion (idVacunacion, idVacunado, idFecha, idLugar, idVacuna) values ((SELECT ISNULL(MAX(idVacunacion) + 1, 0) FROM HVacunacion), @idVacunado, @idFecha, @idLugar, @idVacuna)",
            new { idVacunado = idVacunado, idFecha = idFecha, idLugar = idLugar, idVacuna = idVacuna });
        }

        public static int GetOrCreateVacunado(IDbConnection db, string dni, string gender, int age)
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

        public static int GetOrCreateVacuna(IDbConnection db, string type, string laboratory, string vaccine)
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

        public static int GetOrCreateFecha(IDbConnection db, int year, int mes, int dia)
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

        public static int GetOrCreateLugar(IDbConnection db, string province, string department)
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
