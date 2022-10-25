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
                name: "Vaccines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CanBeRequested = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vaccines", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Appointments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<int>(type: "int", nullable: false),
                    PatientId = table.Column<int>(type: "int", nullable: false),
                    VaccineId = table.Column<int>(type: "int", nullable: false),
                    RequestedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Notified = table.Column<bool>(type: "bit", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    VaccinatorId = table.Column<int>(type: "int", nullable: true),
                    AppliedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Appointments_Users_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Appointments_Users_VaccinatorId",
                        column: x => x.VaccinatorId,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Appointments_Vaccines_VaccineId",
                        column: x => x.VaccineId,
                        principalTable: "Vaccines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppliedVaccines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppliedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AppliedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    VaccineId = table.Column<int>(type: "int", nullable: false),
                    AppointmentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppliedVaccines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppliedVaccines_Appointments_AppointmentId",
                        column: x => x.AppointmentId,
                        principalTable: "Appointments",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppliedVaccines_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppliedVaccines_Vaccines_VaccineId",
                        column: x => x.VaccineId,
                        principalTable: "Vaccines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Address", "BirthDate", "DNI", "Email", "FullName", "Gender", "HealthWorker", "IsActive", "PasswordHash", "Pregnant", "Province", "Role", "UserName" },
                values: new object[,]
                {
                    { 1, "Calle Falsa 1234, La Plata", new DateTime(2022, 10, 25, 0, 0, 0, 0, DateTimeKind.Local), "11111111", "admin@vacunassist.com", "Administrador", "other", false, true, "1000:hAVWAgp1iJE6gBfBIw+2kMI/RMt0VIGh:2krnm/JQn6yAF6+6HEAuA/9KT29TU1m6", false, "Buenos Aires", "administrator", "Admin" },
                    { 2, "Calle Falsa 2345, La Plata", new DateTime(2022, 10, 25, 0, 0, 0, 0, DateTimeKind.Local), "22345678", "operador1@vacunassist.com", "Luis Gutierrez", "male", false, true, "1000:zBGi2DWF2lG5ISXBnZac7UKyVzYpwH+R:mgz/+jBWg14ppGqCGVolEBDfgfr2CSKv", false, "Buenos Aires", "operator", "Operador1" },
                    { 3, "Calle Falsa 9874, Salta", new DateTime(2022, 10, 25, 0, 0, 0, 0, DateTimeKind.Local), "89785451", "estefania@vacunassist.com", "Estefania Borzi", "female", false, true, "1000:SH8w4li/BM+QvSw6KG1YMeQNGUXFfj1p:IwjQJSTY23kScgjycKf/K4CdytTRYAO+", false, "Salta", "operator", "Operador2" },
                    { 4, "Calle Falsa 4567, La Plata", new DateTime(2022, 10, 25, 0, 0, 0, 0, DateTimeKind.Local), "11111111", "vacunador@email.com", "Vacunador", "other", false, true, "1000:qW6C1IuT/uKLpebxKWzDxaH8cAexFVTU:+99ND3uteOJG4/0XSMPL9iVSJXwFvESG", false, "Buenos Aires", "vacunator", "Vacunador" }
                });

            migrationBuilder.InsertData(
                table: "Vaccines",
                columns: new[] { "Id", "CanBeRequested", "IsActive", "Name" },
                values: new object[,]
                {
                    { 1, true, true, "COVID-19" },
                    { 2, false, true, "Fiebre amarilla" },
                    { 3, true, true, "Gripe" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppliedVaccines_AppointmentId",
                table: "AppliedVaccines",
                column: "AppointmentId");

            migrationBuilder.CreateIndex(
                name: "IX_AppliedVaccines_UserId",
                table: "AppliedVaccines",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AppliedVaccines_VaccineId",
                table: "AppliedVaccines",
                column: "VaccineId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_PatientId",
                table: "Appointments",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_VaccinatorId",
                table: "Appointments",
                column: "VaccinatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_VaccineId",
                table: "Appointments",
                column: "VaccineId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppliedVaccines");

            migrationBuilder.DropTable(
                name: "Appointments");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Vaccines");
        }
    }
}
