using Microsoft.EntityFrameworkCore.Migrations;

namespace DbManager.Migrations
{
    public partial class SBReport_Local : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Local",
                table: "SBReports",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Local",
                table: "SBReports");
        }
    }
}
