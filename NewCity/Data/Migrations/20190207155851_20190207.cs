using Microsoft.EntityFrameworkCore.Migrations;

namespace NewCity.Data.Migrations
{
    public partial class _20190207 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "flag",
                table: "StoryCard");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "flag",
                table: "StoryCard",
                nullable: true);
        }
    }
}
