using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VotingSystem.Migrations
{
    public partial class MergingEditionTables_MergingParticipantTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConcludedEditionParticipant_ConcludedEditions_ConcludedEditionId",
                table: "ConcludedEditionParticipant");

            migrationBuilder.DropForeignKey(
                name: "FK_ConcludedEditionParticipant_Projects_ProjectId",
                table: "ConcludedEditionParticipant");

            migrationBuilder.DropForeignKey(
                name: "FK_Vote_ActiveEditionParticipant_ActiveEditionParticipantActiveEditionId_ActiveEditionParticipantProjectId",
                table: "Vote");

            migrationBuilder.DropForeignKey(
                name: "FK_Vote_ConcludedEditionParticipant_ConcludedEditionParticipantConcludedEditionId_ConcludedEditionParticipantProjectId",
                table: "Vote");

            migrationBuilder.DropTable(
                name: "ActiveEditionParticipant");

            migrationBuilder.DropTable(
                name: "ActiveEditions");

            migrationBuilder.DropIndex(
                name: "IX_Vote_ActiveEditionId_ConcludedEditionId_ProjectId_VoterPesel",
                table: "Vote");

            migrationBuilder.DropIndex(
                name: "IX_Vote_ActiveEditionParticipantActiveEditionId_ActiveEditionParticipantProjectId",
                table: "Vote");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ConcludedEditions",
                table: "ConcludedEditions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ConcludedEditionParticipant",
                table: "ConcludedEditionParticipant");

            migrationBuilder.DropColumn(
                name: "ActiveEditionId",
                table: "Vote");

            migrationBuilder.DropColumn(
                name: "ActiveEditionParticipantActiveEditionId",
                table: "Vote");

            migrationBuilder.DropColumn(
                name: "ActiveEditionParticipantProjectId",
                table: "Vote");

            migrationBuilder.RenameTable(
                name: "ConcludedEditions",
                newName: "Edition");

            migrationBuilder.RenameTable(
                name: "ConcludedEditionParticipant",
                newName: "EditionParticipant");

            migrationBuilder.RenameColumn(
                name: "ConcludedEditionParticipantProjectId",
                table: "Vote",
                newName: "EditionParticipantProjectId");

            migrationBuilder.RenameColumn(
                name: "ConcludedEditionParticipantConcludedEditionId",
                table: "Vote",
                newName: "EditionParticipantEditionId");

            migrationBuilder.RenameColumn(
                name: "ConcludedEditionId",
                table: "Vote",
                newName: "EditionId");

            migrationBuilder.RenameIndex(
                name: "IX_Vote_ConcludedEditionParticipantConcludedEditionId_ConcludedEditionParticipantProjectId",
                table: "Vote",
                newName: "IX_Vote_EditionParticipantEditionId_EditionParticipantProjectId");

            migrationBuilder.RenameColumn(
                name: "ConcludedEditionId",
                table: "EditionParticipant",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_ConcludedEditionParticipant_ProjectId",
                table: "EditionParticipant",
                newName: "IX_EditionParticipant_ProjectId");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Edition",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "EditionId",
                table: "EditionParticipant",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "EditionParticipant",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Edition",
                table: "Edition",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EditionParticipant",
                table: "EditionParticipant",
                columns: new[] { "EditionId", "ProjectId" });

            migrationBuilder.CreateIndex(
                name: "IX_Vote_EditionId_ProjectId_VoterPesel",
                table: "Vote",
                columns: new[] { "EditionId", "ProjectId", "VoterPesel" },
                unique: true,
                filter: "[VoterPesel] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_EditionParticipant_Edition_EditionId",
                table: "EditionParticipant",
                column: "EditionId",
                principalTable: "Edition",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EditionParticipant_Projects_ProjectId",
                table: "EditionParticipant",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vote_EditionParticipant_EditionParticipantEditionId_EditionParticipantProjectId",
                table: "Vote",
                columns: new[] { "EditionParticipantEditionId", "EditionParticipantProjectId" },
                principalTable: "EditionParticipant",
                principalColumns: new[] { "EditionId", "ProjectId" },
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EditionParticipant_Edition_EditionId",
                table: "EditionParticipant");

            migrationBuilder.DropForeignKey(
                name: "FK_EditionParticipant_Projects_ProjectId",
                table: "EditionParticipant");

            migrationBuilder.DropForeignKey(
                name: "FK_Vote_EditionParticipant_EditionParticipantEditionId_EditionParticipantProjectId",
                table: "Vote");

            migrationBuilder.DropIndex(
                name: "IX_Vote_EditionId_ProjectId_VoterPesel",
                table: "Vote");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EditionParticipant",
                table: "EditionParticipant");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Edition",
                table: "Edition");

            migrationBuilder.DropColumn(
                name: "EditionId",
                table: "EditionParticipant");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "EditionParticipant");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Edition");

            migrationBuilder.RenameTable(
                name: "EditionParticipant",
                newName: "ConcludedEditionParticipant");

            migrationBuilder.RenameTable(
                name: "Edition",
                newName: "ConcludedEditions");

            migrationBuilder.RenameColumn(
                name: "EditionParticipantProjectId",
                table: "Vote",
                newName: "ConcludedEditionParticipantProjectId");

            migrationBuilder.RenameColumn(
                name: "EditionParticipantEditionId",
                table: "Vote",
                newName: "ConcludedEditionParticipantConcludedEditionId");

            migrationBuilder.RenameColumn(
                name: "EditionId",
                table: "Vote",
                newName: "ConcludedEditionId");

            migrationBuilder.RenameIndex(
                name: "IX_Vote_EditionParticipantEditionId_EditionParticipantProjectId",
                table: "Vote",
                newName: "IX_Vote_ConcludedEditionParticipantConcludedEditionId_ConcludedEditionParticipantProjectId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "ConcludedEditionParticipant",
                newName: "ConcludedEditionId");

            migrationBuilder.RenameIndex(
                name: "IX_EditionParticipant_ProjectId",
                table: "ConcludedEditionParticipant",
                newName: "IX_ConcludedEditionParticipant_ProjectId");

            migrationBuilder.AddColumn<int>(
                name: "ActiveEditionId",
                table: "Vote",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

            migrationBuilder.AddPrimaryKey(
                name: "PK_ConcludedEditionParticipant",
                table: "ConcludedEditionParticipant",
                columns: new[] { "ConcludedEditionId", "ProjectId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ConcludedEditions",
                table: "ConcludedEditions",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ActiveEditions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActiveEditions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ActiveEditionParticipant",
                columns: table => new
                {
                    ActiveEditionId = table.Column<int>(type: "int", nullable: false),
                    ProjectId = table.Column<int>(type: "int", nullable: false)
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
                name: "IX_Vote_ActiveEditionId_ConcludedEditionId_ProjectId_VoterPesel",
                table: "Vote",
                columns: new[] { "ActiveEditionId", "ConcludedEditionId", "ProjectId", "VoterPesel" },
                unique: true,
                filter: "[VoterPesel] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Vote_ActiveEditionParticipantActiveEditionId_ActiveEditionParticipantProjectId",
                table: "Vote",
                columns: new[] { "ActiveEditionParticipantActiveEditionId", "ActiveEditionParticipantProjectId" });

            migrationBuilder.CreateIndex(
                name: "IX_ActiveEditionParticipant_ProjectId",
                table: "ActiveEditionParticipant",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_ConcludedEditionParticipant_ConcludedEditions_ConcludedEditionId",
                table: "ConcludedEditionParticipant",
                column: "ConcludedEditionId",
                principalTable: "ConcludedEditions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ConcludedEditionParticipant_Projects_ProjectId",
                table: "ConcludedEditionParticipant",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vote_ActiveEditionParticipant_ActiveEditionParticipantActiveEditionId_ActiveEditionParticipantProjectId",
                table: "Vote",
                columns: new[] { "ActiveEditionParticipantActiveEditionId", "ActiveEditionParticipantProjectId" },
                principalTable: "ActiveEditionParticipant",
                principalColumns: new[] { "ActiveEditionId", "ProjectId" },
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Vote_ConcludedEditionParticipant_ConcludedEditionParticipantConcludedEditionId_ConcludedEditionParticipantProjectId",
                table: "Vote",
                columns: new[] { "ConcludedEditionParticipantConcludedEditionId", "ConcludedEditionParticipantProjectId" },
                principalTable: "ConcludedEditionParticipant",
                principalColumns: new[] { "ConcludedEditionId", "ProjectId" },
                onDelete: ReferentialAction.Restrict);
        }
    }
}
