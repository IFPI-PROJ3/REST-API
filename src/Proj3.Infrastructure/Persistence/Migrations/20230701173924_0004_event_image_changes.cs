using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Proj3.Infrastructure.Persistence.Migrations
{
    public partial class _0004_event_image_changes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VolunteerCandidateLimit",
                table: "Events");

            migrationBuilder.RenameColumn(
                name: "Decription",
                table: "Events",
                newName: "Description");

            migrationBuilder.AddColumn<bool>(
                name: "ImageThumb",
                table: "EventImages",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageThumb",
                table: "EventImages");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Events",
                newName: "Decription");

            migrationBuilder.AddColumn<int>(
                name: "VolunteerCandidateLimit",
                table: "Events",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
