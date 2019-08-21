using Microsoft.EntityFrameworkCore.Migrations;

namespace DbManager.Migrations
{
    public partial class DeviceMaterials_fix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeviceMaterials_Materials_MaterialId",
                table: "DeviceMaterials");

            migrationBuilder.RenameColumn(
                name: "MaterialId",
                table: "DeviceMaterials",
                newName: "MaterialTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_DeviceMaterials_MaterialId",
                table: "DeviceMaterials",
                newName: "IX_DeviceMaterials_MaterialTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_DeviceMaterials_MaterialTypes_MaterialTypeId",
                table: "DeviceMaterials",
                column: "MaterialTypeId",
                principalTable: "MaterialTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeviceMaterials_MaterialTypes_MaterialTypeId",
                table: "DeviceMaterials");

            migrationBuilder.RenameColumn(
                name: "MaterialTypeId",
                table: "DeviceMaterials",
                newName: "MaterialId");

            migrationBuilder.RenameIndex(
                name: "IX_DeviceMaterials_MaterialTypeId",
                table: "DeviceMaterials",
                newName: "IX_DeviceMaterials_MaterialId");

            migrationBuilder.AddForeignKey(
                name: "FK_DeviceMaterials_Materials_MaterialId",
                table: "DeviceMaterials",
                column: "MaterialId",
                principalTable: "Materials",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
