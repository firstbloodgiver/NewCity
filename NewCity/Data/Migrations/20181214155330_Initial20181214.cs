using Microsoft.EntityFrameworkCore.Migrations;

namespace NewCity.Data.Migrations
{
    public partial class Initial20181214 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsTest",
                table: "StorySeries",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsTest",
                table: "StorySeries");
        }
    }
}
