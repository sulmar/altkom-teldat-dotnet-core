using Microsoft.EntityFrameworkCore.Migrations;

namespace Teldat.Vehicles.Infrastructure.Migrations
{
    public partial class AddHomeAddressToPerson : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "HomeAddress",
                table: "People",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HomeAddress",
                table: "People");
        }
    }
}
