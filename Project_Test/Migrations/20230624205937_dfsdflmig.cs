using Microsoft.EntityFrameworkCore.Migrations;

namespace Project_Test.Migrations
{
    public partial class dfsdflmig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BankName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Driver_Age",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Driver_FirstName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Driver_Gender",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Driver_LastName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Driver_NationalityNum",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BankName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Driver_Age",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Driver_FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Driver_Gender",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Driver_LastName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Driver_NationalityNum",
                table: "AspNetUsers");
        }
    }
}
