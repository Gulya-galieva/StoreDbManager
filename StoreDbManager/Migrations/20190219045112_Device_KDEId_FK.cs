using Microsoft.EntityFrameworkCore.Migrations;

namespace DbManager.Migrations
{
    public partial class Device_KDEId_FK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Devices_KDEId",
                table: "Devices",
                column: "KDEId");

            migrationBuilder.AddForeignKey(
                name: "FK_Devices_KDEs_KDEId",
                table: "Devices",
                column: "KDEId",
                principalTable: "KDEs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Devices_KDEs_KDEId",
                table: "Devices");

            migrationBuilder.DropIndex(
                name: "IX_Devices_KDEId",
                table: "Devices");
        }
    }
}
