using Microsoft.EntityFrameworkCore.Migrations;

namespace VotingSystem.Migrations
{
    public partial class AddedDistrictToConcludedAndActiveEdition : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DistrictId",
                table: "EditionDrafts",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DistrictId",
                table: "Edition",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "District",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_District", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "District",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Repty" },
                    { 2, "Lasowice" },
                    { 3, "Bobrowniki Śląskie" },
                    { 4, "Opatowice" },
                    { 5, "Rybna" },
                    { 6, "Pniowiec" },
                    { 7, "Sowice" },
                    { 8, "Puferki" },
                    { 9, "Repecko" },
                    { 10, "Siwcowe" },
                    { 11, "Tłuczykąt" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_EditionDrafts_DistrictId",
                table: "EditionDrafts",
                column: "DistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_Edition_DistrictId",
                table: "Edition",
                column: "DistrictId");

            migrationBuilder.AddForeignKey(
                name: "FK_Edition_District_DistrictId",
                table: "Edition",
                column: "DistrictId",
                principalTable: "District",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EditionDrafts_District_DistrictId",
                table: "EditionDrafts",
                column: "DistrictId",
                principalTable: "District",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Edition_District_DistrictId",
                table: "Edition");

            migrationBuilder.DropForeignKey(
                name: "FK_EditionDrafts_District_DistrictId",
                table: "EditionDrafts");

            migrationBuilder.DropTable(
                name: "District");

            migrationBuilder.DropIndex(
                name: "IX_EditionDrafts_DistrictId",
                table: "EditionDrafts");

            migrationBuilder.DropIndex(
                name: "IX_Edition_DistrictId",
                table: "Edition");

            migrationBuilder.DropColumn(
                name: "DistrictId",
                table: "EditionDrafts");

            migrationBuilder.DropColumn(
                name: "DistrictId",
                table: "Edition");
        }
    }
}
