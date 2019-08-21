using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DbManager.Migrations
{
    public partial class USPDReport : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Switches_SBReports_SBReportId",
                table: "Switches");

            migrationBuilder.AlterColumn<int>(
                name: "SBReportId",
                table: "Switches",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "USPDReportId",
                table: "Switches",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "USPDReport",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Substation = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    Local = table.Column<string>(nullable: true),
                    UspdSerial = table.Column<string>(nullable: true),
                    PlcSerial = table.Column<string>(nullable: true),
                    AVR = table.Column<bool>(nullable: false),
                    Kvvg = table.Column<double>(nullable: false),
                    Gofr = table.Column<double>(nullable: false),
                    Ftp = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USPDReport", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Switches_USPDReportId",
                table: "Switches",
                column: "USPDReportId");

            migrationBuilder.AddForeignKey(
                name: "FK_Switches_SBReports_SBReportId",
                table: "Switches",
                column: "SBReportId",
                principalTable: "SBReports",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Switches_USPDReport_USPDReportId",
                table: "Switches",
                column: "USPDReportId",
                principalTable: "USPDReport",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Switches_SBReports_SBReportId",
                table: "Switches");

            migrationBuilder.DropForeignKey(
                name: "FK_Switches_USPDReport_USPDReportId",
                table: "Switches");

            migrationBuilder.DropTable(
                name: "USPDReport");

            migrationBuilder.DropIndex(
                name: "IX_Switches_USPDReportId",
                table: "Switches");

            migrationBuilder.DropColumn(
                name: "USPDReportId",
                table: "Switches");

            migrationBuilder.AlterColumn<int>(
                name: "SBReportId",
                table: "Switches",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Switches_SBReports_SBReportId",
                table: "Switches",
                column: "SBReportId",
                principalTable: "SBReports",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
