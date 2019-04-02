using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NewCity.Data.Migrations
{
    public partial class _20190328 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "News",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Type = table.Column<string>(nullable: true),
                    Img = table.Column<string>(nullable: true),
                    Content = table.Column<string>(nullable: true),
                    Creator = table.Column<Guid>(nullable: false),
                    Create = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_News", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "News");
        }
    }
}
