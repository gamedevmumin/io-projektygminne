using Microsoft.EntityFrameworkCore.Migrations;

namespace VotingSystem.Migrations
{
    public partial class AddedDistrictTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EditionDrafts_District_DistrictId",
                table: "EditionDrafts");

            migrationBuilder.DropForeignKey(
                name: "FK_Editions_District_DistrictId",
                table: "Editions");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_District_DistrictId",
                table: "Projects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_District",
                table: "District");

            migrationBuilder.RenameTable(
                name: "District",
                newName: "Districts");

            migrationBuilder.AlterColumn<int>(
                name: "DistrictId",
                table: "Editions",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Districts",
                table: "Districts",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EditionDrafts_Districts_DistrictId",
                table: "EditionDrafts",
                column: "DistrictId",
                principalTable: "Districts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Editions_Districts_DistrictId",
                table: "Editions",
                column: "DistrictId",
                principalTable: "Districts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Districts_DistrictId",
                table: "Projects",
                column: "DistrictId",
                principalTable: "Districts",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EditionDrafts_Districts_DistrictId",
                table: "EditionDrafts");

            migrationBuilder.DropForeignKey(
                name: "FK_Editions_Districts_DistrictId",
                table: "Editions");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Districts_DistrictId",
                table: "Projects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Districts",
                table: "Districts");

            migrationBuilder.RenameTable(
                name: "Districts",
                newName: "District");

            migrationBuilder.AlterColumn<int>(
                name: "DistrictId",
                table: "Editions",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_District",
                table: "District",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EditionDrafts_District_DistrictId",
                table: "EditionDrafts",
                column: "DistrictId",
                principalTable: "District",
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
    }
}
