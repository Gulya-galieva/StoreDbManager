using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DbManager.Migrations
{
    public partial class removeFlags : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateAdd",
                table: "Substations");

            migrationBuilder.DropColumn(
                name: "Accept",
                table: "RegPointFlags");

            migrationBuilder.DropColumn(
                name: "Accepted",
                table: "RegPointFlags");

            migrationBuilder.DropColumn(
                name: "Import",
                table: "RegPointFlags");

            migrationBuilder.DropColumn(
                name: "Notification",
                table: "RegPointFlags");

            migrationBuilder.DropColumn(
                name: "Printed",
                table: "RegPointFlags");

            migrationBuilder.DropColumn(
                name: "Sent",
                table: "RegPointFlags");

            migrationBuilder.DropColumn(
                name: "SentActAccount",
                table: "RegPointFlags");

            migrationBuilder.RenameColumn(
                name: "SentPaper",
                table: "RegPointFlags",
                newName: "ImportConsummerData");

            migrationBuilder.AddColumn<string>(
                name: "O_Local_Secondary",
                table: "Consumers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "U_Local_Secondary",
                table: "Consumers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "O_Local_Secondary",
                table: "Consumers");

            migrationBuilder.DropColumn(
                name: "U_Local_Secondary",
                table: "Consumers");

            migrationBuilder.RenameColumn(
                name: "ImportConsummerData",
                table: "RegPointFlags",
                newName: "SentPaper");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateAdd",
                table: "Substations",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "Accept",
                table: "RegPointFlags",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Accepted",
                table: "RegPointFlags",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Import",
                table: "RegPointFlags",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Notification",
                table: "RegPointFlags",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Printed",
                table: "RegPointFlags",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Sent",
                table: "RegPointFlags",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "SentActAccount",
                table: "RegPointFlags",
                nullable: false,
                defaultValue: false);
        }
    }
}
