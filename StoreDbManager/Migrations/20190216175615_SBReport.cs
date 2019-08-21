using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DbManager.Migrations
{
    public partial class SBReport : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SBReports",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Substation = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    MeterBoard = table.Column<string>(nullable: true),
                    ContractId = table.Column<int>(nullable: false),
                    NetRegionId = table.Column<int>(nullable: false),
                    WorkerId = table.Column<int>(nullable: false),
                    Date1 = table.Column<DateTime>(nullable: false),
                    WorkPermit1 = table.Column<string>(nullable: true),
                    Brigade1 = table.Column<string>(nullable: true),
                    Phase1 = table.Column<string>(nullable: true),
                    Date2 = table.Column<DateTime>(nullable: false),
                    WorkPermit2 = table.Column<string>(nullable: true),
                    Brigade2 = table.Column<string>(nullable: true),
                    Phase2 = table.Column<string>(nullable: true),
                    ReportStateId = table.Column<int>(nullable: false),
                    CuratorId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SBReports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SBReports_Contracts_ContractId",
                        column: x => x.ContractId,
                        principalTable: "Contracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SBReports_NetRegions_NetRegionId",
                        column: x => x.NetRegionId,
                        principalTable: "NetRegions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_SBReports_ReportStates_ReportStateId",
                        column: x => x.ReportStateId,
                        principalTable: "ReportStates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SBReports_Workers_WorkerId",
                        column: x => x.WorkerId,
                        principalTable: "Workers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Switches",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SwitchNumber = table.Column<string>(nullable: true),
                    DeviceSerial = table.Column<string>(nullable: true),
                    DeviceSeal = table.Column<string>(nullable: true),
                    TestBoxSeal = table.Column<string>(nullable: true),
                    TTAk = table.Column<int>(nullable: false),
                    TTANumber = table.Column<string>(nullable: true),
                    TTASeal = table.Column<string>(nullable: true),
                    TTBk = table.Column<int>(nullable: false),
                    TTBNumber = table.Column<string>(nullable: true),
                    TTBSeal = table.Column<string>(nullable: true),
                    TTCk = table.Column<int>(nullable: false),
                    TTCNumber = table.Column<string>(nullable: true),
                    TTCSeal = table.Column<string>(nullable: true),
                    Sum = table.Column<double>(nullable: false),
                    T1 = table.Column<double>(nullable: false),
                    T2 = table.Column<double>(nullable: false),
                    SBReportId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Switches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Switches_SBReports_SBReportId",
                        column: x => x.SBReportId,
                        principalTable: "SBReports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SBReports_ContractId",
                table: "SBReports",
                column: "ContractId");

            migrationBuilder.CreateIndex(
                name: "IX_SBReports_NetRegionId",
                table: "SBReports",
                column: "NetRegionId");

            migrationBuilder.CreateIndex(
                name: "IX_SBReports_ReportStateId",
                table: "SBReports",
                column: "ReportStateId");

            migrationBuilder.CreateIndex(
                name: "IX_SBReports_WorkerId",
                table: "SBReports",
                column: "WorkerId");

            migrationBuilder.CreateIndex(
                name: "IX_Switches_SBReportId",
                table: "Switches",
                column: "SBReportId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Switches");

            migrationBuilder.DropTable(
                name: "SBReports");
        }
    }
}
