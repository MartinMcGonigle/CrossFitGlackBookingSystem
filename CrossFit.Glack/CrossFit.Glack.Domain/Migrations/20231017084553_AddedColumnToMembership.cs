using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CrossFit.Glack.Domain.Migrations
{
    public partial class AddedColumnToMembership : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "MembershipAutoRenew",
                table: "Membership",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MembershipAutoRenew",
                table: "Membership");
        }
    }
}
