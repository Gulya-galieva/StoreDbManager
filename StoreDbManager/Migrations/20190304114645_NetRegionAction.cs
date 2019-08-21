using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DbManager.Migrations
{
    public partial class NetRegionAction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NetRegionActions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Comment = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    ActionTypeId = table.Column<int>(nullable: false),
                    NetRegionId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NetRegionActions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NetRegionActions_ActionTypes_ActionTypeId",
                        column: x => x.ActionTypeId,
                        principalTable: "ActionTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NetRegionActions_NetRegions_NetRegionId",
                        column: x => x.NetRegionId,
                        principalTable: "NetRegions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NetRegionActions_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NetRegionActions_ActionTypeId",
                table: "NetRegionActions",
                column: "ActionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_NetRegionActions_NetRegionId",
                table: "NetRegionActions",
                column: "NetRegionId");

            migrationBuilder.CreateIndex(
                name: "IX_NetRegionActions_UserId",
                table: "NetRegionActions",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NetRegionActions");
        }
    }
}
