using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NewCity.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StorySeries",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    SeriesName = table.Column<string>(nullable: true),
                    Author = table.Column<Guid>(nullable: false),
                    Creationdate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StorySeries", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "StoryCard",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    StorySeriesID = table.Column<Guid>(nullable: false),
                    StoryName = table.Column<string>(nullable: true),
                    Text = table.Column<string>(nullable: true),
                    IMG = table.Column<string>(nullable: true),
                    BackgroundIMG = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoryCard", x => x.ID);
                    table.ForeignKey(
                        name: "FK_StoryCard_StorySeries_StorySeriesID",
                        column: x => x.StorySeriesID,
                        principalTable: "StorySeries",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StoryOption",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    StoryCardID = table.Column<Guid>(nullable: false),
                    Text = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoryOption", x => x.ID);
                    table.ForeignKey(
                        name: "FK_StoryOption_StoryCard_StoryCardID",
                        column: x => x.StoryCardID,
                        principalTable: "StoryCard",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StoryCard_StorySeriesID",
                table: "StoryCard",
                column: "StorySeriesID");

            migrationBuilder.CreateIndex(
                name: "IX_StoryOption_StoryCardID",
                table: "StoryOption",
                column: "StoryCardID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StoryOption");

            migrationBuilder.DropTable(
                name: "StoryCard");

            migrationBuilder.DropTable(
                name: "StorySeries");
        }
    }
}
