using Microsoft.EntityFrameworkCore.Migrations;

namespace DbManager.Migrations
{
    public partial class USPDReport_Contract : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ContractId",
                table: "USPDReport",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NetRegionId",
                table: "USPDReport",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "WorkerId",
                table: "USPDReport",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_USPDReport_ContractId",
                table: "USPDReport",
                column: "ContractId");

            migrationBuilder.CreateIndex(
                name: "IX_USPDReport_NetRegionId",
                table: "USPDReport",
                column: "NetRegionId");

            migrationBuilder.CreateIndex(
                name: "IX_USPDReport_WorkerId",
                table: "USPDReport",
                column: "WorkerId");

            migrationBuilder.AddForeignKey(
                name: "FK_USPDReport_Contracts_ContractId",
                table: "USPDReport",
                column: "ContractId",
                principalTable: "Contracts",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_USPDReport_NetRegions_NetRegionId",
                table: "USPDReport",
                column: "NetRegionId",
                principalTable: "NetRegions",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_USPDReport_Workers_WorkerId",
                table: "USPDReport",
                column: "WorkerId",
                principalTable: "Workers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_USPDReport_Contracts_ContractId",
                table: "USPDReport");

            migrationBuilder.DropForeignKey(
                name: "FK_USPDReport_NetRegions_NetRegionId",
                table: "USPDReport");

            migrationBuilder.DropForeignKey(
                name: "FK_USPDReport_Workers_WorkerId",
                table: "USPDReport");

            migrationBuilder.DropIndex(
                name: "IX_USPDReport_ContractId",
                table: "USPDReport");

            migrationBuilder.DropIndex(
                name: "IX_USPDReport_NetRegionId",
                table: "USPDReport");

            migrationBuilder.DropIndex(
                name: "IX_USPDReport_WorkerId",
                table: "USPDReport");

            migrationBuilder.DropColumn(
                name: "ContractId",
                table: "USPDReport");

            migrationBuilder.DropColumn(
                name: "NetRegionId",
                table: "USPDReport");

            migrationBuilder.DropColumn(
                name: "WorkerId",
                table: "USPDReport");
        }
    }
}
