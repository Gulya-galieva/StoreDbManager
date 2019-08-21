using Microsoft.EntityFrameworkCore.Migrations;

namespace DbManager.Migrations
{
    public partial class AdditionalMaterialFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdditionalMaterials_Devices_DeviceId",
                table: "AdditionalMaterials");

            migrationBuilder.DropForeignKey(
                name: "FK_AdditionalMaterials_ReportTypes_ReportTypeId",
                table: "AdditionalMaterials");

            migrationBuilder.DropIndex(
                name: "IX_AdditionalMaterials_ReportTypeId",
                table: "AdditionalMaterials");

            migrationBuilder.DropColumn(
                name: "ReportId",
                table: "AdditionalMaterials");

            migrationBuilder.DropColumn(
                name: "ReportTypeId",
                table: "AdditionalMaterials");

            migrationBuilder.AlterColumn<int>(
                name: "DeviceId",
                table: "AdditionalMaterials",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AdditionalMaterials_Devices_DeviceId",
                table: "AdditionalMaterials",
                column: "DeviceId",
                principalTable: "Devices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdditionalMaterials_Devices_DeviceId",
                table: "AdditionalMaterials");

            migrationBuilder.AlterColumn<int>(
                name: "DeviceId",
                table: "AdditionalMaterials",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "ReportId",
                table: "AdditionalMaterials",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ReportTypeId",
                table: "AdditionalMaterials",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_AdditionalMaterials_ReportTypeId",
                table: "AdditionalMaterials",
                column: "ReportTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_AdditionalMaterials_Devices_DeviceId",
                table: "AdditionalMaterials",
                column: "DeviceId",
                principalTable: "Devices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AdditionalMaterials_ReportTypes_ReportTypeId",
                table: "AdditionalMaterials",
                column: "ReportTypeId",
                principalTable: "ReportTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
