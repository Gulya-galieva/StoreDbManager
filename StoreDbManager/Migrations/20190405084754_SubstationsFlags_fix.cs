using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DbManager.Migrations
{
    public partial class SubstationsFlags_fix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SubstationFlags");

            migrationBuilder.AddColumn<bool>(
                name: "IsBalanceDone",
                table: "Substations",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsInstallationDone",
                table: "Substations",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsPropSchemeDone",
                table: "Substations",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsBalanceDone",
                table: "Substations");

            migrationBuilder.DropColumn(
                name: "IsInstallationDone",
                table: "Substations");

            migrationBuilder.DropColumn(
                name: "IsPropSchemeDone",
                table: "Substations");

            migrationBuilder.CreateTable(
                name: "SubstationFlags",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IsBalanceDone = table.Column<bool>(nullable: false),
                    IsInstallationDone = table.Column<bool>(nullable: false),
                    IsPropSchemeDone = table.Column<bool>(nullable: false),
                    SubstationId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubstationFlags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubstationFlags_Substations_SubstationId",
                        column: x => x.SubstationId,
                        principalTable: "Substations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SubstationFlags_SubstationId",
                table: "SubstationFlags",
                column: "SubstationId");
        }
    }
}
