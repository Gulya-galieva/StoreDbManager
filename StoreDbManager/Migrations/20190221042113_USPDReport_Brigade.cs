using Microsoft.EntityFrameworkCore.Migrations;

namespace DbManager.Migrations
{
    public partial class USPDReport_Brigade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Brigade",
                table: "USPDReport",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Brigade",
                table: "USPDReport");
        }
    }
}
