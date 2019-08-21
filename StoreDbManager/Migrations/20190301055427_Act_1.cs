using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DbManager.Migrations
{
    public partial class Act_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InstallActs_InstallActTypes_InstallActTypeId",
                table: "InstallActs");

            migrationBuilder.DropTable(
                name: "InstallActTypes");

            migrationBuilder.DropIndex(
                name: "IX_InstallActs_InstallActTypeId",
                table: "InstallActs");

            migrationBuilder.DropColumn(
                name: "InstallActTypeId",
                table: "InstallActs");

            migrationBuilder.AddColumn<int>(
                name: "InstallActType",
                table: "InstallActs",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InstallActType",
                table: "InstallActs");

            migrationBuilder.AddColumn<int>(
                name: "InstallActTypeId",
                table: "InstallActs",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "InstallActTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstallActTypes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InstallActs_InstallActTypeId",
                table: "InstallActs",
                column: "InstallActTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_InstallActs_InstallActTypes_InstallActTypeId",
                table: "InstallActs",
                column: "InstallActTypeId",
                principalTable: "InstallActTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
