using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DbManager.Migrations
{
    public partial class PaymentReports : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PaymentReportDevices");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "RegPoints",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "PaymentReportRegPoints",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RegPointId = table.Column<int>(nullable: false),
                    PaymentReportId = table.Column<int>(nullable: false),
                    WorkType = table.Column<int>(nullable: false),
                    CostRUB = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentReportRegPoints", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaymentReportRegPoints_PaymentReports_PaymentReportId",
                        column: x => x.PaymentReportId,
                        principalTable: "PaymentReports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PaymentReportRegPoints_RegPoints_RegPointId",
                        column: x => x.RegPointId,
                        principalTable: "RegPoints",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PaymentReportRegPoints_PaymentReportId",
                table: "PaymentReportRegPoints",
                column: "PaymentReportId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentReportRegPoints_RegPointId",
                table: "PaymentReportRegPoints",
                column: "RegPointId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PaymentReportRegPoints");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "RegPoints");

            migrationBuilder.CreateTable(
                name: "PaymentReportDevices",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CostRUB = table.Column<double>(nullable: false),
                    DeviceId = table.Column<int>(nullable: false),
                    PaymentReportId = table.Column<int>(nullable: false),
                    WorkType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentReportDevices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaymentReportDevices_Devices_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "Devices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PaymentReportDevices_PaymentReports_PaymentReportId",
                        column: x => x.PaymentReportId,
                        principalTable: "PaymentReports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PaymentReportDevices_DeviceId",
                table: "PaymentReportDevices",
                column: "DeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentReportDevices_PaymentReportId",
                table: "PaymentReportDevices",
                column: "PaymentReportId");
        }
    }
}
