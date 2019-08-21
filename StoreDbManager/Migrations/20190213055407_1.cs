using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DbManager.Migrations
{
    public partial class _1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ReportStateId",
                table: "MounterReportUgesALs",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ReportStates",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportStates", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MounterReportUgesALs_ReportStateId",
                table: "MounterReportUgesALs",
                column: "ReportStateId");

            migrationBuilder.AddForeignKey(
                name: "FK_MounterReportUgesALs_ReportStates_ReportStateId",
                table: "MounterReportUgesALs",
                column: "ReportStateId",
                principalTable: "ReportStates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MounterReportUgesALs_ReportStates_ReportStateId",
                table: "MounterReportUgesALs");

            migrationBuilder.DropTable(
                name: "ReportStates");

            migrationBuilder.DropIndex(
                name: "IX_MounterReportUgesALs_ReportStateId",
                table: "MounterReportUgesALs");

            migrationBuilder.DropColumn(
                name: "ReportStateId",
                table: "MounterReportUgesALs");
        }
    }
}
