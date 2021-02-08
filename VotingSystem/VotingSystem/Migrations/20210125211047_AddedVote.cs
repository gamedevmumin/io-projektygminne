using Microsoft.EntityFrameworkCore.Migrations;

namespace VotingSystem.Migrations
{
    public partial class AddedVote : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Vote",
                columns: table => new
                {
                    VoterPesel = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    ActiveEditionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vote", x => new { x.ActiveEditionId, x.ProjectId, x.VoterPesel });
                    table.ForeignKey(
                        name: "FK_Vote_ActiveEditionParticipant_ActiveEditionId_ProjectId",
                        columns: x => new { x.ActiveEditionId, x.ProjectId },
                        principalTable: "ActiveEditionParticipant",
                        principalColumns: new[] { "ActiveEditionId", "ProjectId" },
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Vote");
        }
    }
}
