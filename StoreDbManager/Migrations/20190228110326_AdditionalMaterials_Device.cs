using Microsoft.EntityFrameworkCore.Migrations;

namespace DbManager.Migrations
{
    public partial class AdditionalMaterials_Device : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DeviceId",
                table: "AdditionalMaterials",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AdditionalMaterials_DeviceId",
                table: "AdditionalMaterials",
                column: "DeviceId");

            migrationBuilder.AddForeignKey(
                name: "FK_AdditionalMaterials_Devices_DeviceId",
                table: "AdditionalMaterials",
                column: "DeviceId",
                principalTable: "Devices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdditionalMaterials_Devices_DeviceId",
                table: "AdditionalMaterials");

            migrationBuilder.DropIndex(
                name: "IX_AdditionalMaterials_DeviceId",
                table: "AdditionalMaterials");

            migrationBuilder.DropColumn(
                name: "DeviceId",
                table: "AdditionalMaterials");
        }
    }
}
