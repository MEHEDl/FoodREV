using Microsoft.EntityFrameworkCore.Migrations;

namespace FoodREV.DataAccess.Migrations
{
    public partial class addPaymentGateway : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "OrderTotal",
                table: "OrderHeader",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderTotal",
                table: "OrderHeader");
        }
    }
}
