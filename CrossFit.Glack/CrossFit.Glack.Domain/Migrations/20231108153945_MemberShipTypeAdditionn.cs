using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CrossFit.Glack.Domain.Migrations
{
    public partial class MemberShipTypeAdditionn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NumberOfClasses",
                table: "MembershipType",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfMonths",
                table: "MembershipType",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumberOfClasses",
                table: "MembershipType");

            migrationBuilder.DropColumn(
                name: "NumberOfMonths",
                table: "MembershipType");
        }
    }
}
