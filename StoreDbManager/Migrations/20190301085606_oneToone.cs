using Microsoft.EntityFrameworkCore.Migrations;

namespace DbManager.Migrations
{
    public partial class oneToone : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConsumerId",
                table: "RegPoints");

            migrationBuilder.DropColumn(
                name: "InstallActId",
                table: "RegPoints");

            migrationBuilder.DropColumn(
                name: "RegPointFlagsId",
                table: "RegPoints");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ConsumerId",
                table: "RegPoints",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "InstallActId",
                table: "RegPoints",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RegPointFlagsId",
                table: "RegPoints",
                nullable: false,
                defaultValue: 0);
        }
    }
}
