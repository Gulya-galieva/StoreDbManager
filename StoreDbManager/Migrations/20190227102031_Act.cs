using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DbManager.Migrations
{
    public partial class Act : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RegPoints_Consumers_ConsumerId",
                table: "RegPoints");

            migrationBuilder.DropIndex(
                name: "IX_RegPoints_ConsumerId",
                table: "RegPoints");

            migrationBuilder.DropIndex(
                name: "IX_InstallActs_RegPointId",
                table: "InstallActs");

            migrationBuilder.DropColumn(
                name: "CommReg",
                table: "RegPoints");

            migrationBuilder.DropColumn(
                name: "Direction",
                table: "RegPoints");

            migrationBuilder.DropColumn(
                name: "Feeder",
                table: "RegPoints");

            migrationBuilder.DropColumn(
                name: "InstallPlace",
                table: "RegPoints");

            migrationBuilder.DropColumn(
                name: "Line",
                table: "RegPoints");

            migrationBuilder.DropColumn(
                name: "PS",
                table: "RegPoints");

            migrationBuilder.DropColumn(
                name: "Section",
                table: "RegPoints");

            migrationBuilder.AddColumn<int>(
                name: "InstallActId",
                table: "RegPoints",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Feeder",
                table: "InstallActs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InstallPlace",
                table: "InstallActs",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsCommReg",
                table: "InstallActs",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsOutFlow",
                table: "InstallActs",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Line",
                table: "InstallActs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PS",
                table: "InstallActs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Section",
                table: "InstallActs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Comment",
                table: "DeviceStates",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RegPointId",
                table: "Consumers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ActionTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActionTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RegPointActions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Comment = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    ActionTypeId = table.Column<int>(nullable: false),
                    RegPointId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegPointActions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RegPointActions_ActionTypes_ActionTypeId",
                        column: x => x.ActionTypeId,
                        principalTable: "ActionTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RegPointActions_RegPoints_RegPointId",
                        column: x => x.RegPointId,
                        principalTable: "RegPoints",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RegPointActions_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubstationActions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Comment = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    ActionTypeId = table.Column<int>(nullable: false),
                    SubstationId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubstationActions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubstationActions_ActionTypes_ActionTypeId",
                        column: x => x.ActionTypeId,
                        principalTable: "ActionTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubstationActions_Substations_SubstationId",
                        column: x => x.SubstationId,
                        principalTable: "Substations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubstationActions_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InstallActs_RegPointId",
                table: "InstallActs",
                column: "RegPointId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Consumers_RegPointId",
                table: "Consumers",
                column: "RegPointId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RegPointActions_ActionTypeId",
                table: "RegPointActions",
                column: "ActionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_RegPointActions_RegPointId",
                table: "RegPointActions",
                column: "RegPointId");

            migrationBuilder.CreateIndex(
                name: "IX_RegPointActions_UserId",
                table: "RegPointActions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SubstationActions_ActionTypeId",
                table: "SubstationActions",
                column: "ActionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_SubstationActions_SubstationId",
                table: "SubstationActions",
                column: "SubstationId");

            migrationBuilder.CreateIndex(
                name: "IX_SubstationActions_UserId",
                table: "SubstationActions",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Consumers_RegPoints_RegPointId",
                table: "Consumers",
                column: "RegPointId",
                principalTable: "RegPoints",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Consumers_RegPoints_RegPointId",
                table: "Consumers");

            migrationBuilder.DropTable(
                name: "RegPointActions");

            migrationBuilder.DropTable(
                name: "SubstationActions");

            migrationBuilder.DropTable(
                name: "ActionTypes");

            migrationBuilder.DropIndex(
                name: "IX_InstallActs_RegPointId",
                table: "InstallActs");

            migrationBuilder.DropIndex(
                name: "IX_Consumers_RegPointId",
                table: "Consumers");

            migrationBuilder.DropColumn(
                name: "InstallActId",
                table: "RegPoints");

            migrationBuilder.DropColumn(
                name: "Feeder",
                table: "InstallActs");

            migrationBuilder.DropColumn(
                name: "InstallPlace",
                table: "InstallActs");

            migrationBuilder.DropColumn(
                name: "IsCommReg",
                table: "InstallActs");

            migrationBuilder.DropColumn(
                name: "IsOutFlow",
                table: "InstallActs");

            migrationBuilder.DropColumn(
                name: "Line",
                table: "InstallActs");

            migrationBuilder.DropColumn(
                name: "PS",
                table: "InstallActs");

            migrationBuilder.DropColumn(
                name: "Section",
                table: "InstallActs");

            migrationBuilder.DropColumn(
                name: "Comment",
                table: "DeviceStates");

            migrationBuilder.DropColumn(
                name: "RegPointId",
                table: "Consumers");

            migrationBuilder.AddColumn<bool>(
                name: "CommReg",
                table: "RegPoints",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Direction",
                table: "RegPoints",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Feeder",
                table: "RegPoints",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InstallPlace",
                table: "RegPoints",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Line",
                table: "RegPoints",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PS",
                table: "RegPoints",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Section",
                table: "RegPoints",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RegPoints_ConsumerId",
                table: "RegPoints",
                column: "ConsumerId");

            migrationBuilder.CreateIndex(
                name: "IX_InstallActs_RegPointId",
                table: "InstallActs",
                column: "RegPointId");

            migrationBuilder.AddForeignKey(
                name: "FK_RegPoints_Consumers_ConsumerId",
                table: "RegPoints",
                column: "ConsumerId",
                principalTable: "Consumers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
