using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DbManager.Migrations
{
    public partial class paymentReport : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PaymentReports",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DateCreate = table.Column<DateTime>(nullable: false),
                    DatePeriodStart = table.Column<DateTime>(nullable: false),
                    IsClosed = table.Column<bool>(nullable: false),
                    Comment = table.Column<string>(nullable: true),
                    WorkerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentReports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaymentReports_Workers_WorkerId",
                        column: x => x.WorkerId,
                        principalTable: "Workers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PaymentReportDevices",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DeviceId = table.Column<int>(nullable: false),
                    PaymentReportId = table.Column<int>(nullable: false),
                    WorkType = table.Column<int>(nullable: false),
                    CostRUB = table.Column<double>(nullable: false)
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

            migrationBuilder.CreateIndex(
                name: "IX_PaymentReports_WorkerId",
                table: "PaymentReports",
                column: "WorkerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PaymentReportDevices");

            migrationBuilder.DropTable(
                name: "PaymentReports");
        }
    }
}
