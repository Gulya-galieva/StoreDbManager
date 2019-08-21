using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DbManager.Migrations
{
    public partial class _2sectionRemove : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Brigade1",
                table: "SBReports");

            migrationBuilder.DropColumn(
                name: "Brigade2",
                table: "SBReports");

            migrationBuilder.DropColumn(
                name: "Date1",
                table: "SBReports");

            migrationBuilder.DropColumn(
                name: "Phase1",
                table: "SBReports");

            migrationBuilder.RenameColumn(
                name: "WorkPermit2",
                table: "SBReports",
                newName: "WorkPermit");

            migrationBuilder.RenameColumn(
                name: "WorkPermit1",
                table: "SBReports",
                newName: "Phase");

            migrationBuilder.RenameColumn(
                name: "Phase2",
                table: "SBReports",
                newName: "Brigade");

            migrationBuilder.RenameColumn(
                name: "Date2",
                table: "SBReports",
                newName: "Date");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "WorkPermit",
                table: "SBReports",
                newName: "WorkPermit2");

            migrationBuilder.RenameColumn(
                name: "Phase",
                table: "SBReports",
                newName: "WorkPermit1");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "SBReports",
                newName: "Date2");

            migrationBuilder.RenameColumn(
                name: "Brigade",
                table: "SBReports",
                newName: "Phase2");

            migrationBuilder.AddColumn<string>(
                name: "Brigade1",
                table: "SBReports",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Brigade2",
                table: "SBReports",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Date1",
                table: "SBReports",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Phase1",
                table: "SBReports",
                nullable: true);
        }
    }
}
