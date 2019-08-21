using Microsoft.EntityFrameworkCore.Migrations;

namespace DbManager.Migrations
{
    public partial class UnmountReport_Contrat_Id : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ContractId",
                table: "UnmountReports",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_UnmountReports_ContractId",
                table: "UnmountReports",
                column: "ContractId");

            migrationBuilder.AddForeignKey(
                name: "FK_UnmountReports_Contracts_ContractId",
                table: "UnmountReports",
                column: "ContractId",
                principalTable: "Contracts",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UnmountReports_Contracts_ContractId",
                table: "UnmountReports");

            migrationBuilder.DropIndex(
                name: "IX_UnmountReports_ContractId",
                table: "UnmountReports");

            migrationBuilder.DropColumn(
                name: "ContractId",
                table: "UnmountReports");
        }
    }
}
