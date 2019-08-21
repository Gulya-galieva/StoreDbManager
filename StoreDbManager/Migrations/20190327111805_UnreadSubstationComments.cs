using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DbManager.Migrations
{
    public partial class UnreadSubstationComments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UnreadSubstationComments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(nullable: false),
                    CommentSubstationId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnreadSubstationComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UnreadSubstationComments_CommentSubstations_CommentSubstationId",
                        column: x => x.CommentSubstationId,
                        principalTable: "CommentSubstations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_UnreadSubstationComments_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UnreadSubstationComments_CommentSubstationId",
                table: "UnreadSubstationComments",
                column: "CommentSubstationId");

            migrationBuilder.CreateIndex(
                name: "IX_UnreadSubstationComments_UserId",
                table: "UnreadSubstationComments",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UnreadSubstationComments");
        }
    }
}
