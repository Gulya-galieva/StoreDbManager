using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DbManager.Migrations
{
    public partial class InstallPlace : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InstallPlace",
                table: "InstallActs");

            migrationBuilder.AddColumn<int>(
                name: "InstallPlaceNumber",
                table: "InstallActs",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "InstallPlaceTypeId",
                table: "InstallActs",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "InstallPlaceTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstallPlaceTypes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InstallActs_InstallPlaceTypeId",
                table: "InstallActs",
                column: "InstallPlaceTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_InstallActs_InstallPlaceTypes_InstallPlaceTypeId",
                table: "InstallActs",
                column: "InstallPlaceTypeId",
                principalTable: "InstallPlaceTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InstallActs_InstallPlaceTypes_InstallPlaceTypeId",
                table: "InstallActs");

            migrationBuilder.DropTable(
                name: "InstallPlaceTypes");

            migrationBuilder.DropIndex(
                name: "IX_InstallActs_InstallPlaceTypeId",
                table: "InstallActs");

            migrationBuilder.DropColumn(
                name: "InstallPlaceNumber",
                table: "InstallActs");

            migrationBuilder.DropColumn(
                name: "InstallPlaceTypeId",
                table: "InstallActs");

            migrationBuilder.AddColumn<string>(
                name: "InstallPlace",
                table: "InstallActs",
                nullable: true);
        }
    }
}
