using Microsoft.EntityFrameworkCore.Migrations;

namespace Teldat.Vehicles.Infrastructure.Migrations
{
    public partial class AddColorToVehicle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "Vehicles",
                maxLength: 50,
                nullable: true);

            // migrationBuilder.Sql("")
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Color",
                table: "Vehicles");
        }
    }
}
