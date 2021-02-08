using Microsoft.EntityFrameworkCore.Migrations;

namespace VotingSystem.Migrations
{
    public partial class AddedMultipleColumnsToProject : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Edition_District_DistrictId",
                table: "Edition");

            migrationBuilder.DropForeignKey(
                name: "FK_EditionParticipant_Edition_EditionId",
                table: "EditionParticipant");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Edition",
                table: "Edition");

            migrationBuilder.DropColumn(
                name: "PricePln",
                table: "Projects");

            migrationBuilder.RenameTable(
                name: "Edition",
                newName: "Editions");

            migrationBuilder.RenameIndex(
                name: "IX_Edition_DistrictId",
                table: "Editions",
                newName: "IX_Editions_DistrictId");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DistrictId",
                table: "Projects",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<long>(
                name: "EstimatedRealizationTime",
                table: "Projects",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Projects",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Editions",
                table: "Editions",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_DistrictId",
                table: "Projects",
                column: "DistrictId");

            migrationBuilder.AddForeignKey(
                name: "FK_EditionParticipant_Editions_EditionId",
                table: "EditionParticipant",
                column: "EditionId",
                principalTable: "Editions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Editions_District_DistrictId",
                table: "Editions",
                column: "DistrictId",
                principalTable: "District",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_District_DistrictId",
                table: "Projects",
                column: "DistrictId",
                principalTable: "District",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EditionParticipant_Editions_EditionId",
                table: "EditionParticipant");

            migrationBuilder.DropForeignKey(
                name: "FK_Editions_District_DistrictId",
                table: "Editions");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_District_DistrictId",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_DistrictId",
                table: "Projects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Editions",
                table: "Editions");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "DistrictId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "EstimatedRealizationTime",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Projects");

            migrationBuilder.RenameTable(
                name: "Editions",
                newName: "Edition");

            migrationBuilder.RenameIndex(
                name: "IX_Editions_DistrictId",
                table: "Edition",
                newName: "IX_Edition_DistrictId");

            migrationBuilder.AddColumn<decimal>(
                name: "PricePln",
                table: "Projects",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Edition",
                table: "Edition",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Edition_District_DistrictId",
                table: "Edition",
                column: "DistrictId",
                principalTable: "District",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EditionParticipant_Edition_EditionId",
                table: "EditionParticipant",
                column: "EditionId",
                principalTable: "Edition",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
