using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Proj3.Infrastructure.Persistence.Migrations
{
    public partial class _0003_voluntary_coordinates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Latitude",
                table: "Volunteers",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Longitude",
                table: "Volunteers",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "Volunteers");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "Volunteers");
        }
    }
}
