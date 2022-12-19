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
                    DaysToDelivery = table.Column<int>(type: "int", nullable: false),
                    VaccineText = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DevelopedVaccines", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Province",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Province", x => x.Id);
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
                name: "PurchaseOrders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PurchaseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ETA = table.Column<DateTime>(type: "datetime2", nullable: true),
                    BatchNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    DeliveredTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DevelopedVaccineId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PurchaseOrders_DevelopedVaccines_DevelopedVaccineId",
                        column: x => x.DevelopedVaccineId,
                        principalTable: "DevelopedVaccines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ProvinceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Departments_Province_ProvinceId",
                        column: x => x.ProvinceId,
                        principalTable: "Province",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BatchVaccines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BatchNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    DevelopedVaccineId = table.Column<int>(type: "int", nullable: false),
                    PurchaseOrderId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    RemainingQuantity = table.Column<int>(type: "int", nullable: false),
                    OverdueQuantity = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Synchronized = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BatchVaccines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BatchVaccines_DevelopedVaccines_DevelopedVaccineId",
                        column: x => x.DevelopedVaccineId,
                        principalTable: "DevelopedVaccines",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BatchVaccines_PurchaseOrders_PurchaseOrderId",
                        column: x => x.PurchaseOrderId,
                        principalTable: "PurchaseOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DNI = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DepartmentId = table.Column<int>(type: "int", nullable: false),
                    BirthDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Pregnant = table.Column<bool>(type: "bit", nullable: false),
                    HealthWorker = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Patients_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LocalBatchVaccines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BatchVaccineId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    RemainingQuantity = table.Column<int>(type: "int", nullable: false),
                    OverdueQuantity = table.Column<int>(type: "int", nullable: false),
                    Province = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DistributionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Synchronized = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocalBatchVaccines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LocalBatchVaccines_BatchVaccines_BatchVaccineId",
                        column: x => x.BatchVaccineId,
                        principalTable: "BatchVaccines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppliedVaccines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppliedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PatientId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    LocalBatchVaccineId = table.Column<int>(type: "int", nullable: false),
                    AppliedDose = table.Column<int>(type: "int", nullable: false),
                    Synchronized = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppliedVaccines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppliedVaccines_LocalBatchVaccines_LocalBatchVaccineId",
                        column: x => x.LocalBatchVaccineId,
                        principalTable: "LocalBatchVaccines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppliedVaccines_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
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
                columns: new[] { "Id", "DaysToDelivery", "IsActive", "Name", "VaccineText" },
                values: new object[,]
                {
                    { 1, 30, true, "Pfizer", "{\"Id\":2000,\"Name\":\"COVID-19\",\"VaccineType\":\"ARNM\",\"Type\":1,\"Doses\":[{\"Id\":2001,\"Number\":0,\"IsReinforcement\":false,\"MinMonthsOfAge\":0,\"DaysAfterPreviousDose\":null},{\"Id\":2002,\"Number\":1,\"IsReinforcement\":false,\"MinMonthsOfAge\":0,\"DaysAfterPreviousDose\":120}]}" },
                    { 2, 60, true, "ROCHE", "{\"Id\":1300,\"Name\":\"Fiebre Amarilla\",\"VaccineType\":\"subunidades proteicas\",\"Type\":0,\"Doses\":[{\"Id\":1301,\"Number\":0,\"IsReinforcement\":false,\"MinMonthsOfAge\":18,\"DaysAfterPreviousDose\":null},{\"Id\":1302,\"Number\":1,\"IsReinforcement\":true,\"MinMonthsOfAge\":132,\"DaysAfterPreviousDose\":null}]}" },
                    { 3, 15, true, "Janssen", "{\"Id\":3000,\"Name\":\"Antigripal\",\"VaccineType\":\"Vector viral\",\"Type\":2,\"Doses\":[{\"Id\":3001,\"Number\":0,\"IsReinforcement\":false,\"MinMonthsOfAge\":0,\"DaysAfterPreviousDose\":365}]}" }
                });

            migrationBuilder.InsertData(
                table: "Province",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[,]
                {
                    { 1, "02", "Ciudad Autónoma de Buenos Aires" },
                    { 2, "06", "Buenos Aires" },
                    { 3, "10", "Catamarca" },
                    { 4, "14", "Córdoba" },
                    { 5, "18", "Corrientes" },
                    { 6, "22", "Chaco" },
                    { 7, "26", "Chubut" },
                    { 8, "30", "Entre Ríos" },
                    { 9, "34", "Formosa" },
                    { 10, "38", "Jujuy" },
                    { 11, "42", "La Pampa" },
                    { 12, "46", "La Rioja" },
                    { 13, "50", "Mendoza" },
                    { 14, "54", "Misiones" },
                    { 15, "58", "Neuquén" },
                    { 16, "62", "Río Negro" },
                    { 17, "66", "Salta" },
                    { 18, "70", "San Juan" },
                    { 19, "74", "San Luis" },
                    { 20, "78", "Santa Cruz" },
                    { 21, "82", "Santa Fe" },
                    { 22, "86", "Santiago del Estero" },
                    { 23, "90", "Tucumán" },
                    { 24, "94", "Tierra del Fuego, Antártida e Islas del Atlántico Sur" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Address", "BirthDate", "DNI", "Email", "FullName", "Gender", "HealthWorker", "IsActive", "PasswordHash", "Pregnant", "Province", "Role", "UserName" },
                values: new object[,]
                {
                    { 1, "Calle Falsa 1234, La Plata", new DateTime(2022, 12, 19, 0, 0, 0, 0, DateTimeKind.Local), "11111111", "admin@vacunassist.com", "Administrador", "other", false, true, "1000:7AnBgUrlHfWKOdGx9jBXBs6bUB4coiO1:JJkJrAqRmC6gLVirDszlrinUA0TiImJK", false, "Buenos Aires", "administrator", "Admin" },
                    { 2, "Calle Falsa 2345, La Plata", new DateTime(2022, 12, 19, 0, 0, 0, 0, DateTimeKind.Local), "22345678", "operador1@vacunassist.com", "Luis Gutierrez", "male", false, true, "1000:UHsg6Rbd+QZeVOW7NDOWGA35Yu4EVRDB:WNIbW5JpwvjXgwc/hq39H4crQD6duDDm", false, "Buenos Aires", "operator", "Operador1" },
                    { 3, "Calle Falsa 9874, Salta", new DateTime(2022, 12, 19, 0, 0, 0, 0, DateTimeKind.Local), "89785451", "estefania@vacunassist.com", "Estefania Borzi", "female", false, true, "1000:DGSDaj3Ip4tnNAI7Th4sD5blX0jwURCD:/mME0CgrfhPCOdUml9ikK9qs3LINgBH1", false, "Salta", "operator", "Operador2" },
                    { 4, "Calle Falsa 9874, Salta", new DateTime(2022, 12, 19, 0, 0, 0, 0, DateTimeKind.Local), "89785451", "jr@vacunassist.com", "Jose Luis Rodriguez", "male", false, true, "1000:SDfkS+pYeZu8hoyD2l6fh9e6dtj8+ZBO:4tSmrea4p9VeTHS9kcnw9v6dt+NMXr+X", false, "Buenos Aires", "analyst", "Analista1" },
                    { 5, "Calle Falsa 4567, La Plata", new DateTime(2022, 12, 19, 0, 0, 0, 0, DateTimeKind.Local), "11111111", "vacunador@email.com", "Vacunador", "other", false, true, "1000:Z+JCEgv4au06NiEpWGOXqrBieJ5Krkyz:Af1cP2BzJTFh5dNC2ZOqVrwwXrjpU0nY", false, "Buenos Aires", "vacunator", "Vacunador" }
                });

            migrationBuilder.InsertData(
                table: "PurchaseOrders",
                columns: new[] { "Id", "BatchNumber", "DeliveredTime", "DevelopedVaccineId", "ETA", "PurchaseDate", "Quantity", "Status" },
                values: new object[,]
                {
                    { 1, null, null, 3, null, new DateTime(2022, 12, 19, 0, 3, 42, 778, DateTimeKind.Local).AddTicks(7245), 1400, 0 },
                    { 2, null, null, 3, null, new DateTime(2022, 12, 19, 0, 3, 42, 778, DateTimeKind.Local).AddTicks(7247), 1200, 0 },
                    { 3, "PF1000001", new DateTime(2022, 12, 19, 0, 0, 0, 0, DateTimeKind.Local), 1, new DateTime(2022, 12, 19, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2022, 11, 19, 0, 0, 0, 0, DateTimeKind.Local), 800, 2 },
                    { 4, "PF1000121", new DateTime(2022, 12, 19, 0, 0, 0, 0, DateTimeKind.Local), 1, new DateTime(2022, 12, 19, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2022, 11, 19, 0, 0, 0, 0, DateTimeKind.Local), 400, 2 },
                    { 5, "R1000001", new DateTime(2022, 12, 19, 0, 0, 0, 0, DateTimeKind.Local), 2, new DateTime(2022, 12, 19, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2022, 10, 20, 0, 0, 0, 0, DateTimeKind.Local), 560, 2 },
                    { 6, "FLU12214001", new DateTime(2022, 12, 19, 0, 0, 0, 0, DateTimeKind.Local), 3, new DateTime(2022, 12, 19, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2022, 12, 4, 0, 0, 0, 0, DateTimeKind.Local), 1500, 2 },
                    { 7, "FLU12214003", new DateTime(2022, 12, 19, 0, 0, 0, 0, DateTimeKind.Local), 3, new DateTime(2022, 12, 19, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2022, 12, 4, 0, 0, 0, 0, DateTimeKind.Local), 3600, 2 },
                    { 8, "FLU13214121", new DateTime(2022, 12, 1, 0, 0, 0, 0, DateTimeKind.Local), 3, new DateTime(2022, 12, 1, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2022, 11, 16, 0, 0, 0, 0, DateTimeKind.Local), 3600, 2 }
                });

            migrationBuilder.InsertData(
                table: "BatchVaccines",
                columns: new[] { "Id", "BatchNumber", "DevelopedVaccineId", "DueDate", "OverdueQuantity", "PurchaseOrderId", "Quantity", "RemainingQuantity", "Status", "Synchronized" },
                values: new object[,]
                {
                    { 1, "PF1000001", 1, new DateTime(2023, 1, 21, 0, 0, 0, 0, DateTimeKind.Local), 0, 3, 800, 800, 0, false },
                    { 2, "PF1000121", 1, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Local), 0, 4, 400, 400, 0, false },
                    { 3, "R1000001", 2, new DateTime(2023, 2, 10, 0, 0, 0, 0, DateTimeKind.Local), 0, 5, 560, 560, 0, false },
                    { 4, "FLU12214001", 3, new DateTime(2022, 12, 27, 0, 0, 0, 0, DateTimeKind.Local), 0, 6, 1500, 1500, 0, false },
                    { 5, "FLU12214003", 3, new DateTime(2023, 3, 27, 0, 0, 0, 0, DateTimeKind.Local), 0, 7, 3600, 3000, 0, false },
                    { 6, "FLU13214121", 3, new DateTime(2022, 12, 1, 0, 0, 0, 0, DateTimeKind.Local), 0, 8, 3600, 3600, 0, false }
                });

            migrationBuilder.InsertData(
                table: "LocalBatchVaccines",
                columns: new[] { "Id", "BatchVaccineId", "DistributionDate", "OverdueQuantity", "Province", "Quantity", "RemainingQuantity", "Synchronized" },
                values: new object[] { 1, 5, new DateTime(2022, 12, 19, 0, 3, 42, 778, DateTimeKind.Local).AddTicks(7308), 0, "Buenos Aires", 600, 600, false });

            migrationBuilder.CreateIndex(
                name: "IX_AppliedVaccines_LocalBatchVaccineId",
                table: "AppliedVaccines",
                column: "LocalBatchVaccineId");

            migrationBuilder.CreateIndex(
                name: "IX_AppliedVaccines_PatientId",
                table: "AppliedVaccines",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_AppliedVaccines_UserId",
                table: "AppliedVaccines",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_BatchVaccines_BatchNumber",
                table: "BatchVaccines",
                column: "BatchNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BatchVaccines_DevelopedVaccineId",
                table: "BatchVaccines",
                column: "DevelopedVaccineId");

            migrationBuilder.CreateIndex(
                name: "IX_BatchVaccines_PurchaseOrderId",
                table: "BatchVaccines",
                column: "PurchaseOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Departments_ProvinceId",
                table: "Departments",
                column: "ProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_LocalBatchVaccines_BatchVaccineId",
                table: "LocalBatchVaccines",
                column: "BatchVaccineId");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_DepartmentId",
                table: "Patients",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrders_DevelopedVaccineId",
                table: "PurchaseOrders",
                column: "DevelopedVaccineId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppliedVaccines");

            migrationBuilder.DropTable(
                name: "LocalBatchVaccines");

            migrationBuilder.DropTable(
                name: "Patients");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "BatchVaccines");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "PurchaseOrders");

            migrationBuilder.DropTable(
                name: "Province");

            migrationBuilder.DropTable(
                name: "DevelopedVaccines");
        }
    }
}
