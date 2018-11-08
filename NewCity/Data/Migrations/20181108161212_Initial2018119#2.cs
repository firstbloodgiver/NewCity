using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NewCity.Data.Migrations
{
    public partial class Initial20181192 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserCharacter",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    CharacterName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCharacter", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "CharacterSchedule",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CharacterID = table.Column<Guid>(nullable: false),
                    StorySeriesID = table.Column<Guid>(nullable: false),
                    StoryCardID = table.Column<Guid>(nullable: false),
                    IsMain = table.Column<bool>(nullable: false),
                    UserCharacterID = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterSchedule", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CharacterSchedule_UserCharacter_UserCharacterID",
                        column: x => x.UserCharacterID,
                        principalTable: "UserCharacter",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CharacterSchedule_UserCharacterID",
                table: "CharacterSchedule",
                column: "UserCharacterID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CharacterSchedule");

            migrationBuilder.DropTable(
                name: "UserCharacter");
        }
    }
}
