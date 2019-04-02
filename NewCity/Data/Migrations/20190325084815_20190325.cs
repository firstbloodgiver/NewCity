using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NewCity.Data.Migrations
{
    public partial class _20190325 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActivate",
                table: "UserCharacter",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                table: "Location",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Img",
                table: "Location",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Location",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StorySeries_LocationID",
                table: "StorySeries",
                column: "LocationID");

           
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StorySeries_Location_LocationID",
                table: "StorySeries");

            migrationBuilder.DropIndex(
                name: "IX_StorySeries_LocationID",
                table: "StorySeries");

            migrationBuilder.DropColumn(
                name: "IsActivate",
                table: "UserCharacter");

            migrationBuilder.DropColumn(
                name: "CreationTime",
                table: "Location");

            migrationBuilder.DropColumn(
                name: "Img",
                table: "Location");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Location");
        }
    }
}
