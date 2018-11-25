using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NewCity.Data.Migrations
{
    public partial class Initial20181123 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "ActionPoints",
                table: "UserCharacter",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "Experience",
                table: "UserCharacter",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "Intelligence",
                table: "UserCharacter",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "Lucky",
                table: "UserCharacter",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "Moral",
                table: "UserCharacter",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "Speed",
                table: "UserCharacter",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "Status",
                table: "UserCharacter",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "Strength",
                table: "UserCharacter",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<bool>(
                name: "IsPlayed",
                table: "StorySeries",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "LocationID",
                table: "StorySeries",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "flag",
                table: "StorySeries",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CharacterItem",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    ItemID = table.Column<Guid>(nullable: false),
                    CharacterID = table.Column<Guid>(nullable: false),
                    Amount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterItem", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "CharacterLog",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    StoryID = table.Column<Guid>(nullable: false),
                    CharacterID = table.Column<Guid>(nullable: false),
                    Msg = table.Column<string>(nullable: true),
                    CreateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterLog", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Item",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Introduction = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Coordinate = table.Column<string>(nullable: true),
                    Price = table.Column<decimal>(nullable: false),
                    Effect = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Item", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ItemLog",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    ItemID = table.Column<Guid>(nullable: false),
                    CharacterID = table.Column<Guid>(nullable: false),
                    Msg = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemLog", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Location",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Introduction = table.Column<string>(nullable: true),
                    Coordinate = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Location", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CharacterItem");

            migrationBuilder.DropTable(
                name: "CharacterLog");

            migrationBuilder.DropTable(
                name: "Item");

            migrationBuilder.DropTable(
                name: "ItemLog");

            migrationBuilder.DropTable(
                name: "Location");

            migrationBuilder.DropColumn(
                name: "ActionPoints",
                table: "UserCharacter");

            migrationBuilder.DropColumn(
                name: "Experience",
                table: "UserCharacter");

            migrationBuilder.DropColumn(
                name: "Intelligence",
                table: "UserCharacter");

            migrationBuilder.DropColumn(
                name: "Lucky",
                table: "UserCharacter");

            migrationBuilder.DropColumn(
                name: "Moral",
                table: "UserCharacter");

            migrationBuilder.DropColumn(
                name: "Speed",
                table: "UserCharacter");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "UserCharacter");

            migrationBuilder.DropColumn(
                name: "Strength",
                table: "UserCharacter");

            migrationBuilder.DropColumn(
                name: "IsPlayed",
                table: "StorySeries");

            migrationBuilder.DropColumn(
                name: "LocationID",
                table: "StorySeries");

            migrationBuilder.DropColumn(
                name: "flag",
                table: "StorySeries");
        }
    }
}
