using Microsoft.EntityFrameworkCore.Migrations;

namespace DbManager.Migrations
{
    public partial class ReportComment_User : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "ReportComments",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ReportComments_UserId",
                table: "ReportComments",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReportComments_Users_UserId",
                table: "ReportComments",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReportComments_Users_UserId",
                table: "ReportComments");

            migrationBuilder.DropIndex(
                name: "IX_ReportComments_UserId",
                table: "ReportComments");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ReportComments");
        }
    }
}
