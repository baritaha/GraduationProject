using Microsoft.EntityFrameworkCore.Migrations;

namespace Project_Test.Migrations
{
    public partial class lopMig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StockBloodStockID",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_StockBloodStockID",
                table: "AspNetUsers",
                column: "StockBloodStockID");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Stocks_StockBloodStockID",
                table: "AspNetUsers",
                column: "StockBloodStockID",
                principalTable: "Stocks",
                principalColumn: "BloodStockID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Stocks_StockBloodStockID",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_StockBloodStockID",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "StockBloodStockID",
                table: "AspNetUsers");
        }
    }
}
