using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DbManager.Migrations
{
    public partial class Comment_substation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "CommentSubstations",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "CommentSubstations",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CommentSubstations_UserId",
                table: "CommentSubstations",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_CommentSubstations_Users_UserId",
                table: "CommentSubstations",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommentSubstations_Users_UserId",
                table: "CommentSubstations");

            migrationBuilder.DropIndex(
                name: "IX_CommentSubstations_UserId",
                table: "CommentSubstations");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "CommentSubstations");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "CommentSubstations");
        }
    }
}
