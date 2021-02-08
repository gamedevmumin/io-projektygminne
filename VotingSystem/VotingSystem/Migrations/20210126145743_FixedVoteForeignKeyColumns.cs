using Microsoft.EntityFrameworkCore.Migrations;

namespace VotingSystem.Migrations
{
    public partial class FixedVoteForeignKeyColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vote_EditionParticipant_EditionParticipantEditionId_EditionParticipantProjectId",
                table: "Vote");

            migrationBuilder.DropIndex(
                name: "IX_Vote_EditionParticipantEditionId_EditionParticipantProjectId",
                table: "Vote");

            migrationBuilder.DropColumn(
                name: "EditionParticipantEditionId",
                table: "Vote");

            migrationBuilder.DropColumn(
                name: "EditionParticipantProjectId",
                table: "Vote");

            migrationBuilder.AddForeignKey(
                name: "FK_Vote_EditionParticipant_EditionId_ProjectId",
                table: "Vote",
                columns: new[] { "EditionId", "ProjectId" },
                principalTable: "EditionParticipant",
                principalColumns: new[] { "EditionId", "ProjectId" },
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vote_EditionParticipant_EditionId_ProjectId",
                table: "Vote");

            migrationBuilder.AddColumn<int>(
                name: "EditionParticipantEditionId",
                table: "Vote",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EditionParticipantProjectId",
                table: "Vote",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vote_EditionParticipantEditionId_EditionParticipantProjectId",
                table: "Vote",
                columns: new[] { "EditionParticipantEditionId", "EditionParticipantProjectId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Vote_EditionParticipant_EditionParticipantEditionId_EditionParticipantProjectId",
                table: "Vote",
                columns: new[] { "EditionParticipantEditionId", "EditionParticipantProjectId" },
                principalTable: "EditionParticipant",
                principalColumns: new[] { "EditionId", "ProjectId" },
                onDelete: ReferentialAction.Restrict);
        }
    }
}
