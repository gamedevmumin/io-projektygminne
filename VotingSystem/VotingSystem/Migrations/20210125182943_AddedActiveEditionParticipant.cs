using Microsoft.EntityFrameworkCore.Migrations;

namespace VotingSystem.Migrations
{
    public partial class AddedActiveEditionParticipant : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ActiveEditionParticipant",
                columns: table => new
                {
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    ActiveEditionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActiveEditionParticipant", x => new { x.ActiveEditionId, x.ProjectId });
                    table.ForeignKey(
                        name: "FK_ActiveEditionParticipant_ActiveEditions_ActiveEditionId",
                        column: x => x.ActiveEditionId,
                        principalTable: "ActiveEditions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ActiveEditionParticipant_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActiveEditionParticipant_ProjectId",
                table: "ActiveEditionParticipant",
                column: "ProjectId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActiveEditionParticipant");
        }
    }
}
