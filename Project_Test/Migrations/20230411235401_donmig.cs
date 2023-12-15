using Microsoft.EntityFrameworkCore.Migrations;

namespace Project_Test.Migrations
{
    public partial class donmig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Donations",
                columns: table => new
                {
                    DonationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Donor_Location = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Patient_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Patient_Age = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Patient_Gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Patient_BloodType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Patient_Hospital = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Patient_NationalityNum = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Diseases = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Donation_Status = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Donations", x => x.DonationID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Donations");
        }
    }
}
