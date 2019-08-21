using Microsoft.EntityFrameworkCore.Migrations;

namespace DbManager.Migrations
{
    public partial class USPDReport_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Switches_USPDReport_USPDReportId",
                table: "Switches");

            migrationBuilder.DropForeignKey(
                name: "FK_USPDReport_Contracts_ContractId",
                table: "USPDReport");

            migrationBuilder.DropForeignKey(
                name: "FK_USPDReport_NetRegions_NetRegionId",
                table: "USPDReport");

            migrationBuilder.DropForeignKey(
                name: "FK_USPDReport_Workers_WorkerId",
                table: "USPDReport");

            migrationBuilder.DropPrimaryKey(
                name: "PK_USPDReport",
                table: "USPDReport");

            migrationBuilder.RenameTable(
                name: "USPDReport",
                newName: "USPDReports");

            migrationBuilder.RenameIndex(
                name: "IX_USPDReport_WorkerId",
                table: "USPDReports",
                newName: "IX_USPDReports_WorkerId");

            migrationBuilder.RenameIndex(
                name: "IX_USPDReport_NetRegionId",
                table: "USPDReports",
                newName: "IX_USPDReports_NetRegionId");

            migrationBuilder.RenameIndex(
                name: "IX_USPDReport_ContractId",
                table: "USPDReports",
                newName: "IX_USPDReports_ContractId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_USPDReports",
                table: "USPDReports",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Switches_USPDReports_USPDReportId",
                table: "Switches",
                column: "USPDReportId",
                principalTable: "USPDReports",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_USPDReports_Contracts_ContractId",
                table: "USPDReports",
                column: "ContractId",
                principalTable: "Contracts",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_USPDReports_NetRegions_NetRegionId",
                table: "USPDReports",
                column: "NetRegionId",
                principalTable: "NetRegions",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_USPDReports_Workers_WorkerId",
                table: "USPDReports",
                column: "WorkerId",
                principalTable: "Workers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Switches_USPDReports_USPDReportId",
                table: "Switches");

            migrationBuilder.DropForeignKey(
                name: "FK_USPDReports_Contracts_ContractId",
                table: "USPDReports");

            migrationBuilder.DropForeignKey(
                name: "FK_USPDReports_NetRegions_NetRegionId",
                table: "USPDReports");

            migrationBuilder.DropForeignKey(
                name: "FK_USPDReports_Workers_WorkerId",
                table: "USPDReports");

            migrationBuilder.DropPrimaryKey(
                name: "PK_USPDReports",
                table: "USPDReports");

            migrationBuilder.RenameTable(
                name: "USPDReports",
                newName: "USPDReport");

            migrationBuilder.RenameIndex(
                name: "IX_USPDReports_WorkerId",
                table: "USPDReport",
                newName: "IX_USPDReport_WorkerId");

            migrationBuilder.RenameIndex(
                name: "IX_USPDReports_NetRegionId",
                table: "USPDReport",
                newName: "IX_USPDReport_NetRegionId");

            migrationBuilder.RenameIndex(
                name: "IX_USPDReports_ContractId",
                table: "USPDReport",
                newName: "IX_USPDReport_ContractId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_USPDReport",
                table: "USPDReport",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Switches_USPDReport_USPDReportId",
                table: "Switches",
                column: "USPDReportId",
                principalTable: "USPDReport",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_USPDReport_Contracts_ContractId",
                table: "USPDReport",
                column: "ContractId",
                principalTable: "Contracts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_USPDReport_NetRegions_NetRegionId",
                table: "USPDReport",
                column: "NetRegionId",
                principalTable: "NetRegions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_USPDReport_Workers_WorkerId",
                table: "USPDReport",
                column: "WorkerId",
                principalTable: "Workers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
