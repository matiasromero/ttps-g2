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
                    DeliveredTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    BatchNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
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
                    Status = table.Column<int>(type: "int", nullable: false)
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

            migrationBuilder.InsertData(
                table: "DevelopedVaccines",
                columns: new[] { "Id", "DaysToDelivery", "IsActive", "Name", "VaccineText" },
                values: new object[,]
                {
                    { 1, 30, true, "Pfizer COVID-19", "{\"Id\":2000,\"Name\":\"COVID-19\",\"Type\":1,\"Doses\":[{\"Id\":2001,\"Number\":0,\"IsReinforcement\":false,\"MinMonthsOfAge\":0,\"DaysAfterPreviousDose\":null},{\"Id\":2002,\"Number\":1,\"IsReinforcement\":false,\"MinMonthsOfAge\":0,\"DaysAfterPreviousDose\":120}]}" },
                    { 2, 60, true, "ROCHE Fiebre amarilla", "{\"Id\":1300,\"Name\":\"Fiebre Amarilla\",\"Type\":0,\"Doses\":[{\"Id\":1301,\"Number\":0,\"IsReinforcement\":false,\"MinMonthsOfAge\":18,\"DaysAfterPreviousDose\":null},{\"Id\":1302,\"Number\":1,\"IsReinforcement\":true,\"MinMonthsOfAge\":132,\"DaysAfterPreviousDose\":null}]}" },
                    { 3, 15, true, "Fluarix Antigripal", "{\"Id\":3000,\"Name\":\"Antigripal\",\"Type\":2,\"Doses\":[{\"Id\":3001,\"Number\":0,\"IsReinforcement\":false,\"MinMonthsOfAge\":0,\"DaysAfterPreviousDose\":365}]}" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Address", "BirthDate", "DNI", "Email", "FullName", "Gender", "HealthWorker", "IsActive", "PasswordHash", "Pregnant", "Province", "Role", "UserName" },
                values: new object[,]
                {
                    { 1, "Calle Falsa 1234, La Plata", new DateTime(2022, 10, 28, 0, 0, 0, 0, DateTimeKind.Local), "11111111", "admin@vacunassist.com", "Administrador", "other", false, true, "1000:RjOrsizrGp/PAHdDpXeJQfDabcfNjN3g:HewdJLH1UVzSDCXUiDlmG1FjRpkOWjPZ", false, "Buenos Aires", "administrator", "Admin" },
                    { 2, "Calle Falsa 2345, La Plata", new DateTime(2022, 10, 28, 0, 0, 0, 0, DateTimeKind.Local), "22345678", "operador1@vacunassist.com", "Luis Gutierrez", "male", false, true, "1000:yifzj29Uy65beDL96KR+OcJXFVLsskOQ:/kYxHj1Uk2qh63ijrkaa7kKw5DtwKhmO", false, "Buenos Aires", "operator", "Operador1" },
                    { 3, "Calle Falsa 9874, Salta", new DateTime(2022, 10, 28, 0, 0, 0, 0, DateTimeKind.Local), "89785451", "estefania@vacunassist.com", "Estefania Borzi", "female", false, true, "1000:3wsf2XcUpP1r3vCNQyXteSiikLiUizCF:t8zrIhYVywL8HLcpT5sxy4szcTIJkMzd", false, "Salta", "operator", "Operador2" },
                    { 4, "Calle Falsa 9874, Salta", new DateTime(2022, 10, 28, 0, 0, 0, 0, DateTimeKind.Local), "89785451", "jr@vacunassist.com", "Jose Luis Rodriguez", "male", false, true, "1000:DPde7cTqjAuxuWx5jMVhpKnLMO0S1B3Y:arVuIw7rxthR5Jnw4+CDAKlBC+oQx3nj", false, "Buenos Aires", "analyst", "Analista1" },
                    { 5, "Calle Falsa 4567, La Plata", new DateTime(2022, 10, 28, 0, 0, 0, 0, DateTimeKind.Local), "11111111", "vacunador@email.com", "Vacunador", "other", false, true, "1000:+ZeaMU7MUNpiQDEHbWOh84spQZwKKUsl:jZxVLTpm3MnSM/8Inng/ro64eJrhXan0", false, "Buenos Aires", "vacunator", "Vacunador" }
                });

            migrationBuilder.InsertData(
                table: "PurchaseOrders",
                columns: new[] { "Id", "BatchNumber", "DeliveredTime", "DevelopedVaccineId", "ETA", "PurchaseDate", "Quantity", "Status" },
                values: new object[,]
                {
                    { 1, "FLU140012580", null, 3, null, new DateTime(2022, 10, 28, 11, 34, 15, 581, DateTimeKind.Local).AddTicks(5373), 1400, 0 },
                    { 2, "FLU140012581", null, 3, null, new DateTime(2022, 10, 28, 11, 34, 15, 581, DateTimeKind.Local).AddTicks(5380), 1200, 0 },
                    { 3, "PF1000001", new DateTime(2022, 10, 28, 0, 0, 0, 0, DateTimeKind.Local), 1, new DateTime(2022, 10, 28, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2022, 9, 28, 0, 0, 0, 0, DateTimeKind.Local), 800, 2 },
                    { 4, "PF1000121", new DateTime(2022, 10, 28, 0, 0, 0, 0, DateTimeKind.Local), 1, new DateTime(2022, 10, 28, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2022, 9, 28, 0, 0, 0, 0, DateTimeKind.Local), 400, 2 },
                    { 5, "R1000001", new DateTime(2022, 10, 28, 0, 0, 0, 0, DateTimeKind.Local), 2, new DateTime(2022, 10, 28, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2022, 8, 29, 0, 0, 0, 0, DateTimeKind.Local), 560, 2 },
                    { 6, "FLU12214001", new DateTime(2022, 10, 28, 0, 0, 0, 0, DateTimeKind.Local), 3, new DateTime(2022, 10, 28, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2022, 10, 13, 0, 0, 0, 0, DateTimeKind.Local), 1500, 2 },
                    { 7, "FLU12214003", new DateTime(2022, 10, 28, 0, 0, 0, 0, DateTimeKind.Local), 3, new DateTime(2022, 10, 28, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2022, 10, 13, 0, 0, 0, 0, DateTimeKind.Local), 3600, 2 },
                    { 8, "FLU13214121", new DateTime(2022, 10, 10, 0, 0, 0, 0, DateTimeKind.Local), 3, new DateTime(2022, 10, 10, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2022, 9, 25, 0, 0, 0, 0, DateTimeKind.Local), 3600, 2 }
                });

            migrationBuilder.InsertData(
                table: "BatchVaccines",
                columns: new[] { "Id", "BatchNumber", "DevelopedVaccineId", "DueDate", "OverdueQuantity", "PurchaseOrderId", "Quantity", "RemainingQuantity", "Status" },
                values: new object[,]
                {
                    { 1, "PF1000001", 1, new DateTime(2022, 11, 30, 0, 0, 0, 0, DateTimeKind.Local), 0, 3, 800, 800, 0 },
                    { 2, "PF1000121", 1, new DateTime(2022, 11, 10, 0, 0, 0, 0, DateTimeKind.Local), 0, 4, 400, 400, 0 },
                    { 3, "R1000001", 2, new DateTime(2022, 12, 20, 0, 0, 0, 0, DateTimeKind.Local), 0, 5, 560, 560, 0 },
                    { 4, "FLU12214001", 3, new DateTime(2022, 11, 5, 0, 0, 0, 0, DateTimeKind.Local), 0, 6, 1500, 1500, 0 },
                    { 5, "FLU12214003", 3, new DateTime(2023, 2, 3, 0, 0, 0, 0, DateTimeKind.Local), 0, 7, 3600, 3600, 0 },
                    { 6, "FLU13214121", 3, new DateTime(2022, 10, 10, 0, 0, 0, 0, DateTimeKind.Local), 3600, 8, 3600, 0, 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppliedVaccines_UserId",
                table: "AppliedVaccines",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AppliedVaccines_VaccineId",
                table: "AppliedVaccines",
                column: "VaccineId");

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
                name: "IX_PurchaseOrders_BatchNumber",
                table: "PurchaseOrders",
                column: "BatchNumber",
                unique: true);

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
                name: "BatchVaccines");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "PurchaseOrders");

            migrationBuilder.DropTable(
                name: "DevelopedVaccines");
        }
    }
}
