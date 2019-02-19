using Microsoft.EntityFrameworkCore.Migrations;

namespace NewCity.Data.Migrations
{
    public partial class _2018124 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsMain",
                table: "Location");

            migrationBuilder.AddColumn<string>(
                name: "IMG",
                table: "StorySeries",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Text",
                table: "StorySeries",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "flag",
                table: "StoryCard",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsStory",
                table: "CharacterSchedule",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IMG",
                table: "StorySeries");

            migrationBuilder.DropColumn(
                name: "Text",
                table: "StorySeries");

            migrationBuilder.DropColumn(
                name: "flag",
                table: "StoryCard");

            migrationBuilder.DropColumn(
                name: "IsStory",
                table: "CharacterSchedule");

            migrationBuilder.AddColumn<bool>(
                name: "IsMain",
                table: "Location",
                nullable: false,
                defaultValue: false);
        }
    }
}
