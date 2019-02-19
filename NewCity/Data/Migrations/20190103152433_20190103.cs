using Microsoft.EntityFrameworkCore.Migrations;

namespace NewCity.Data.Migrations
{
    public partial class _20190103 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Condition",
                table: "StoryOption",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Effect",
                table: "StoryOption",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Condition",
                table: "StoryOption");

            migrationBuilder.DropColumn(
                name: "Effect",
                table: "StoryOption");
        }
    }
}
