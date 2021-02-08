using Microsoft.EntityFrameworkCore.Migrations;

namespace VotingSystem.Migrations
{
    public partial class AddedEditionIdToAcceptedProjectInsteadOfAcceptedFlag : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Accepted",
                table: "Projects");

            migrationBuilder.AddColumn<int>(
                name: "ConcludedEditionId",
                table: "Projects",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Projects_ConcludedEditionId",
                table: "Projects",
                column: "ConcludedEditionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Editions_ConcludedEditionId",
                table: "Projects",
                column: "ConcludedEditionId",
                principalTable: "Editions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Editions_ConcludedEditionId",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_ConcludedEditionId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "ConcludedEditionId",
                table: "Projects");

            migrationBuilder.AddColumn<bool>(
                name: "Accepted",
                table: "Projects",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
