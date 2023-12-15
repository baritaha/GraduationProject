using Microsoft.EntityFrameworkCore.Migrations;

namespace Project_Test.Migrations
{
    public partial class opmig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stocks_AspNetUsers_BankNameId",
                table: "Stocks");

            migrationBuilder.DropIndex(
                name: "IX_Stocks_BankNameId",
                table: "Stocks");

            migrationBuilder.DropColumn(
                name: "BankNameId",
                table: "Stocks");

            migrationBuilder.AddColumn<string>(
                name: "BankName",
                table: "Stocks",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BankName",
                table: "Stocks");

            migrationBuilder.AddColumn<string>(
                name: "BankNameId",
                table: "Stocks",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Stocks_BankNameId",
                table: "Stocks",
                column: "BankNameId");

            migrationBuilder.AddForeignKey(
                name: "FK_Stocks_AspNetUsers_BankNameId",
                table: "Stocks",
                column: "BankNameId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
