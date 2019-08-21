using Microsoft.EntityFrameworkCore.Migrations;

namespace DbManager.Migrations
{
    public partial class flags : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAscueChecked",
                table: "RegPointFlags",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsAscueOk",
                table: "RegPointFlags",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsLinkOk",
                table: "RegPointFlags",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAscueChecked",
                table: "RegPointFlags");

            migrationBuilder.DropColumn(
                name: "IsAscueOk",
                table: "RegPointFlags");

            migrationBuilder.DropColumn(
                name: "IsLinkOk",
                table: "RegPointFlags");
        }
    }
}
