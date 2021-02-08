using Microsoft.EntityFrameworkCore.Migrations;

namespace VotingSystem.Migrations
{
    public partial class AddedRelationBetweenProjectAndDraft : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EditionDraftProject",
                columns: table => new
                {
                    EditionDraftsId = table.Column<int>(type: "int", nullable: false),
                    ProjectsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EditionDraftProject", x => new { x.EditionDraftsId, x.ProjectsId });
                    table.ForeignKey(
                        name: "FK_EditionDraftProject_EditionDrafts_EditionDraftsId",
                        column: x => x.EditionDraftsId,
                        principalTable: "EditionDrafts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EditionDraftProject_Projects_ProjectsId",
                        column: x => x.ProjectsId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EditionDraftProject_ProjectsId",
                table: "EditionDraftProject",
                column: "ProjectsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EditionDraftProject");
        }
    }
}
