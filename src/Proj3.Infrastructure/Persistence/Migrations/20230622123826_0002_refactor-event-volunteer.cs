using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Proj3.Infrastructure.Persistence.Migrations
{
    public partial class _0002_refactoreventvolunteer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "VolunteerId",
                table: "EventVolunteers",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid")
                .OldAnnotation("Relational:ColumnOrder", 1);

            migrationBuilder.AlterColumn<Guid>(
                name: "EventId",
                table: "EventVolunteers",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid")
                .OldAnnotation("Relational:ColumnOrder", 0);

            migrationBuilder.AddColumn<bool>(
                name: "Accepted",
                table: "EventVolunteers",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "EventVolunteers",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Accepted",
                table: "EventVolunteers");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "EventVolunteers");

            migrationBuilder.AlterColumn<Guid>(
                name: "VolunteerId",
                table: "EventVolunteers",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid")
                .Annotation("Relational:ColumnOrder", 1);

            migrationBuilder.AlterColumn<Guid>(
                name: "EventId",
                table: "EventVolunteers",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid")
                .Annotation("Relational:ColumnOrder", 0);
        }
    }
}
