using Microsoft.EntityFrameworkCore.Migrations;

namespace DbManager.Migrations
{
    public partial class UnmountReport_FKs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CuratorId",
                table: "UnmountReports",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ReportStateId",
                table: "UnmountReports",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_UnmountReports_ReportStateId",
                table: "UnmountReports",
                column: "ReportStateId");

            migrationBuilder.AddForeignKey(
                name: "FK_UnmountReports_ReportStates_ReportStateId",
                table: "UnmountReports",
                column: "ReportStateId",
                principalTable: "ReportStates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UnmountReports_ReportStates_ReportStateId",
                table: "UnmountReports");

            migrationBuilder.DropIndex(
                name: "IX_UnmountReports_ReportStateId",
                table: "UnmountReports");

            migrationBuilder.DropColumn(
                name: "CuratorId",
                table: "UnmountReports");

            migrationBuilder.DropColumn(
                name: "ReportStateId",
                table: "UnmountReports");
        }
    }
}
