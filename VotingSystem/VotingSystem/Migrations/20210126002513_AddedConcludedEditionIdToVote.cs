using Microsoft.EntityFrameworkCore.Migrations;

namespace VotingSystem.Migrations
{
    public partial class AddedConcludedEditionIdToVote : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vote_ActiveEditionParticipant_ActiveEditionId_ProjectId",
                table: "Vote");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Vote",
                table: "Vote");

            migrationBuilder.AlterColumn<string>(
                name: "VoterPesel",
                table: "Vote",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Vote",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "ActiveEditionParticipantActiveEditionId",
                table: "Vote",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ActiveEditionParticipantProjectId",
                table: "Vote",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ConcludedEditionId",
                table: "Vote",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Vote",
                table: "Vote",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Vote_ActiveEditionId_ConcludedEditionId_ProjectId_VoterPesel",
                table: "Vote",
                columns: new[] { "ActiveEditionId", "ConcludedEditionId", "ProjectId", "VoterPesel" },
                unique: true,
                filter: "[VoterPesel] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Vote_ActiveEditionParticipantActiveEditionId_ActiveEditionParticipantProjectId",
                table: "Vote",
                columns: new[] { "ActiveEditionParticipantActiveEditionId", "ActiveEditionParticipantProjectId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Vote_ActiveEditionParticipant_ActiveEditionParticipantActiveEditionId_ActiveEditionParticipantProjectId",
                table: "Vote",
                columns: new[] { "ActiveEditionParticipantActiveEditionId", "ActiveEditionParticipantProjectId" },
                principalTable: "ActiveEditionParticipant",
                principalColumns: new[] { "ActiveEditionId", "ProjectId" },
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vote_ActiveEditionParticipant_ActiveEditionParticipantActiveEditionId_ActiveEditionParticipantProjectId",
                table: "Vote");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Vote",
                table: "Vote");

            migrationBuilder.DropIndex(
                name: "IX_Vote_ActiveEditionId_ConcludedEditionId_ProjectId_VoterPesel",
                table: "Vote");

            migrationBuilder.DropIndex(
                name: "IX_Vote_ActiveEditionParticipantActiveEditionId_ActiveEditionParticipantProjectId",
                table: "Vote");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Vote");

            migrationBuilder.DropColumn(
                name: "ActiveEditionParticipantActiveEditionId",
                table: "Vote");

            migrationBuilder.DropColumn(
                name: "ActiveEditionParticipantProjectId",
                table: "Vote");

            migrationBuilder.DropColumn(
                name: "ConcludedEditionId",
                table: "Vote");

            migrationBuilder.AlterColumn<string>(
                name: "VoterPesel",
                table: "Vote",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Vote",
                table: "Vote",
                columns: new[] { "ActiveEditionId", "ProjectId", "VoterPesel" });

            migrationBuilder.AddForeignKey(
                name: "FK_Vote_ActiveEditionParticipant_ActiveEditionId_ProjectId",
                table: "Vote",
                columns: new[] { "ActiveEditionId", "ProjectId" },
                principalTable: "ActiveEditionParticipant",
                principalColumns: new[] { "ActiveEditionId", "ProjectId" },
                onDelete: ReferentialAction.Cascade);
        }
    }
}
