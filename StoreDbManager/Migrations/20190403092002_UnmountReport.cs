using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DbManager.Migrations
{
    public partial class UnmountReport : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UnmountReports",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    WorkerId = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnmountReports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UnmountReports_Workers_WorkerId",
                        column: x => x.WorkerId,
                        principalTable: "Workers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UnmountedDevices",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Reason = table.Column<string>(nullable: true),
                    DeviceId = table.Column<int>(nullable: false),
                    UnmountReportId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnmountedDevices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UnmountedDevices_Devices_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "Devices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UnmountedDevices_UnmountReports_UnmountReportId",
                        column: x => x.UnmountReportId,
                        principalTable: "UnmountReports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UnmountedDevices_DeviceId",
                table: "UnmountedDevices",
                column: "DeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_UnmountedDevices_UnmountReportId",
                table: "UnmountedDevices",
                column: "UnmountReportId");

            migrationBuilder.CreateIndex(
                name: "IX_UnmountReports_WorkerId",
                table: "UnmountReports",
                column: "WorkerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UnmountedDevices");

            migrationBuilder.DropTable(
                name: "UnmountReports");
        }
    }
}
