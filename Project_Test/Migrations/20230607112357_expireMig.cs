using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Project_Test.Migrations
{
    public partial class expireMig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Bloods",
                newName: "Start_Date");

            migrationBuilder.AddColumn<DateTime>(
                name: "Expire_Date",
                table: "Bloods",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Expire_Date",
                table: "Bloods");

            migrationBuilder.RenameColumn(
                name: "Start_Date",
                table: "Bloods",
                newName: "Date");
        }
    }
}
