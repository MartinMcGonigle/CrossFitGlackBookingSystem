using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CrossFit.Glack.Domain.Migrations
{
    public partial class AddedColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RegistrationTime",
                table: "ClassRegistration");

            migrationBuilder.AddColumn<bool>(
                name: "Present",
                table: "ClassRegistration",
                type: "bit",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Present",
                table: "ClassRegistration");

            migrationBuilder.AddColumn<DateTime>(
                name: "RegistrationTime",
                table: "ClassRegistration",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
