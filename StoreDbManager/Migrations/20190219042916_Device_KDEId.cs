using Microsoft.EntityFrameworkCore.Migrations;

namespace DbManager.Migrations
{
    public partial class Device_KDEId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KDEs_PowerLineSupports_PowerLineSupportId",
                table: "KDEs");

            migrationBuilder.AlterColumn<int>(
                name: "PowerLineSupportId",
                table: "KDEs",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "KDEId",
                table: "Devices",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_KDEs_PowerLineSupports_PowerLineSupportId",
                table: "KDEs",
                column: "PowerLineSupportId",
                principalTable: "PowerLineSupports",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KDEs_PowerLineSupports_PowerLineSupportId",
                table: "KDEs");

            migrationBuilder.DropColumn(
                name: "KDEId",
                table: "Devices");

            migrationBuilder.AlterColumn<int>(
                name: "PowerLineSupportId",
                table: "KDEs",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_KDEs_PowerLineSupports_PowerLineSupportId",
                table: "KDEs",
                column: "PowerLineSupportId",
                principalTable: "PowerLineSupports",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
