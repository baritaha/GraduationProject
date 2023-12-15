using Microsoft.EntityFrameworkCore.Migrations;

namespace Project_Test.Migrations
{
    public partial class edituinmig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Current_Qty",
                table: "AspNetUsers",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Current_Qty",
                table: "AspNetUsers");
        }
    }
}
