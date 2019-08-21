using Microsoft.EntityFrameworkCore.Migrations;

namespace DbManager.Migrations
{
    public partial class Letter_TrackNumber : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumOfReestr",
                table: "Letters");

            migrationBuilder.AddColumn<string>(
                name: "TrackNumber",
                table: "Letters",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TrackNumber",
                table: "Letters");

            migrationBuilder.AddColumn<int>(
                name: "NumOfReestr",
                table: "Letters",
                nullable: true);
        }
    }
}
