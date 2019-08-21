using Microsoft.EntityFrameworkCore.Migrations;

namespace DbManager.Migrations
{
    public partial class USPDReport_StateId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ReportStateId",
                table: "USPDReports",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_USPDReports_ReportStateId",
                table: "USPDReports",
                column: "ReportStateId");

            migrationBuilder.AddForeignKey(
                name: "FK_USPDReports_ReportStates_ReportStateId",
                table: "USPDReports",
                column: "ReportStateId",
                principalTable: "ReportStates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_USPDReports_ReportStates_ReportStateId",
                table: "USPDReports");

            migrationBuilder.DropIndex(
                name: "IX_USPDReports_ReportStateId",
                table: "USPDReports");

            migrationBuilder.DropColumn(
                name: "ReportStateId",
                table: "USPDReports");
        }
    }
}
