using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VacunnasistBackend.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DevelopedVaccines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    VaccineText = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DevelopedVaccines", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Province = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Pregnant = table.Column<bool>(type: "bit", nullable: false),
                    HealthWorker = table.Column<bool>(type: "bit", nullable: false),
                    DNI = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppliedVaccines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppliedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AppliedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Remark = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PersonId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    VaccineId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppliedVaccines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppliedVaccines_DevelopedVaccines_VaccineId",
                        column: x => x.VaccineId,
                        principalTable: "DevelopedVaccines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppliedVaccines_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "DevelopedVaccines",
                columns: new[] { "Id", "IsActive", "Name", "VaccineText" },
                values: new object[,]
                {
                    { 1, true, "COVID-19", "{\"Id\":2000,\"Name\":\"COVID-19\",\"Type\":1,\"Doses\":[{\"Id\":2001,\"Number\":0,\"IsReinforcement\":false,\"MinMonthsOfAge\":0,\"DaysAfterPreviousDose\":null},{\"Id\":2002,\"Number\":1,\"IsReinforcement\":false,\"MinMonthsOfAge\":0,\"DaysAfterPreviousDose\":120}]}" },
                    { 2, true, "Fiebre amarilla", "{\"Id\":1300,\"Name\":\"Fiebre Amarilla\",\"Type\":0,\"Doses\":[{\"Id\":1301,\"Number\":0,\"IsReinforcement\":false,\"MinMonthsOfAge\":18,\"DaysAfterPreviousDose\":null},{\"Id\":1302,\"Number\":1,\"IsReinforcement\":true,\"MinMonthsOfAge\":132,\"DaysAfterPreviousDose\":null}]}" },
                    { 3, true, "Gripe", "{\"Id\":3000,\"Name\":\"COVID-19\",\"Type\":1,\"Doses\":[{\"Id\":3001,\"Number\":0,\"IsReinforcement\":false,\"MinMonthsOfAge\":0,\"DaysAfterPreviousDose\":365}]}" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Address", "BirthDate", "DNI", "Email", "FullName", "Gender", "HealthWorker", "IsActive", "PasswordHash", "Pregnant", "Province", "Role", "UserName" },
                values: new object[,]
                {
                    { 1, "Calle Falsa 1234, La Plata", new DateTime(2022, 10, 26, 0, 0, 0, 0, DateTimeKind.Local), "11111111", "admin@vacunassist.com", "Administrador", "other", false, true, "1000:xZ/bt+X0HJUTGObVpYVaA1kOAci5tQW/:0tSVLVyp/Djf3MDZijHQTtBGtO2dvt7p", false, "Buenos Aires", "administrator", "Admin" },
                    { 2, "Calle Falsa 2345, La Plata", new DateTime(2022, 10, 26, 0, 0, 0, 0, DateTimeKind.Local), "22345678", "operador1@vacunassist.com", "Luis Gutierrez", "male", false, true, "1000:1s4mYpbWzbg02ltukr8KH7DQmLRxwWML:qvK7Cb7asvkJ4kHCZ0cJ2JOrB2F9idpd", false, "Buenos Aires", "operator", "Operador1" },
                    { 3, "Calle Falsa 9874, Salta", new DateTime(2022, 10, 26, 0, 0, 0, 0, DateTimeKind.Local), "89785451", "estefania@vacunassist.com", "Estefania Borzi", "female", false, true, "1000:H1p57Rtk3iRL5/bN8UFGQxVZjcMoXU81:QxP+9an4unyST5t0X6uqrIfIMiY1Q1DQ", false, "Salta", "operator", "Operador2" },
                    { 4, "Calle Falsa 9874, Salta", new DateTime(2022, 10, 26, 0, 0, 0, 0, DateTimeKind.Local), "89785451", "jr@vacunassist.com", "Jose Luis Rodriguez", "male", false, true, "1000:pMfNhDIgWFW9WdEEhdFWzgxp6Lsh9UUk:b4IdmRlLnTIl8oYeJ9rjC1ogw60AB+eE", false, "Buenos Aires", "analyst", "Analista1" },
                    { 5, "Calle Falsa 4567, La Plata", new DateTime(2022, 10, 26, 0, 0, 0, 0, DateTimeKind.Local), "11111111", "vacunador@email.com", "Vacunador", "other", false, true, "1000:tGQ3RwgJN7RH+8mJZR2Tol2DllYV2wWB:tsfiWmFbS7ETKLwoLU9BxiAhn9raXayH", false, "Buenos Aires", "vacunator", "Vacunador" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppliedVaccines_UserId",
                table: "AppliedVaccines",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AppliedVaccines_VaccineId",
                table: "AppliedVaccines",
                column: "VaccineId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppliedVaccines");

            migrationBuilder.DropTable(
                name: "DevelopedVaccines");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
