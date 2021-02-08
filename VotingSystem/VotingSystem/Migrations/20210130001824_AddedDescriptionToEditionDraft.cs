using Microsoft.EntityFrameworkCore.Migrations;

namespace VotingSystem.Migrations
{
    public partial class AddedDescriptionToEditionDraft : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "EditionDrafts",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "EditionDrafts");
        }
    }
}
