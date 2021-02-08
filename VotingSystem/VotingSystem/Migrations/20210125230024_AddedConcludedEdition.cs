using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VotingSystem.Migrations
{
    public partial class AddedConcludedEdition : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ConcludedEditionParticipantConcludedEditionId",
                table: "Vote",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ConcludedEditionParticipantProjectId",
                table: "Vote",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ConcludedEditions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConcludedEditions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ConcludedEditionParticipant",
                columns: table => new
                {
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    ConcludedEditionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConcludedEditionParticipant", x => new { x.ConcludedEditionId, x.ProjectId });
                    table.ForeignKey(
                        name: "FK_ConcludedEditionParticipant_ConcludedEditions_ConcludedEditionId",
                        column: x => x.ConcludedEditionId,
                        principalTable: "ConcludedEditions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ConcludedEditionParticipant_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Vote_ConcludedEditionParticipantConcludedEditionId_ConcludedEditionParticipantProjectId",
                table: "Vote",
                columns: new[] { "ConcludedEditionParticipantConcludedEditionId", "ConcludedEditionParticipantProjectId" });

            migrationBuilder.CreateIndex(
                name: "IX_ConcludedEditionParticipant_ProjectId",
                table: "ConcludedEditionParticipant",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vote_ConcludedEditionParticipant_ConcludedEditionParticipantConcludedEditionId_ConcludedEditionParticipantProjectId",
                table: "Vote",
                columns: new[] { "ConcludedEditionParticipantConcludedEditionId", "ConcludedEditionParticipantProjectId" },
                principalTable: "ConcludedEditionParticipant",
                principalColumns: new[] { "ConcludedEditionId", "ProjectId" },
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vote_ConcludedEditionParticipant_ConcludedEditionParticipantConcludedEditionId_ConcludedEditionParticipantProjectId",
                table: "Vote");

            migrationBuilder.DropTable(
                name: "ConcludedEditionParticipant");

            migrationBuilder.DropTable(
                name: "ConcludedEditions");

            migrationBuilder.DropIndex(
                name: "IX_Vote_ConcludedEditionParticipantConcludedEditionId_ConcludedEditionParticipantProjectId",
                table: "Vote");

            migrationBuilder.DropColumn(
                name: "ConcludedEditionParticipantConcludedEditionId",
                table: "Vote");

            migrationBuilder.DropColumn(
                name: "ConcludedEditionParticipantProjectId",
                table: "Vote");
        }
    }
}
