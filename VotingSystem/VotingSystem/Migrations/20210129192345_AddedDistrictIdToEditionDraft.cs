using Microsoft.EntityFrameworkCore.Migrations;

namespace VotingSystem.Migrations
{
    public partial class AddedDistrictIdToEditionDraft : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EditionDrafts_District_DistrictId",
                table: "EditionDrafts");

            migrationBuilder.AlterColumn<int>(
                name: "DistrictId",
                table: "EditionDrafts",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_EditionDrafts_District_DistrictId",
                table: "EditionDrafts",
                column: "DistrictId",
                principalTable: "District",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EditionDrafts_District_DistrictId",
                table: "EditionDrafts");

            migrationBuilder.AlterColumn<int>(
                name: "DistrictId",
                table: "EditionDrafts",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_EditionDrafts_District_DistrictId",
                table: "EditionDrafts",
                column: "DistrictId",
                principalTable: "District",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
