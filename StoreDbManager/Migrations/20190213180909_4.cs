using Microsoft.EntityFrameworkCore.Migrations;

namespace DbManager.Migrations
{
    public partial class _4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ReportRemarks_UserId",
                table: "ReportRemarks",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReportRemarks_Users_UserId",
                table: "ReportRemarks",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReportRemarks_Users_UserId",
                table: "ReportRemarks");

            migrationBuilder.DropIndex(
                name: "IX_ReportRemarks_UserId",
                table: "ReportRemarks");
        }
    }
}
