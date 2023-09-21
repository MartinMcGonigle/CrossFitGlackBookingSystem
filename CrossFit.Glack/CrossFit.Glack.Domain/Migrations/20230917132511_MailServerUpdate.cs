using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CrossFit.Glack.Domain.Migrations
{
    public partial class MailServerUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MailServer",
                columns: table => new
                {
                    MailServerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MailServerName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MailServerIP = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MailServerUserName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MailServerPassword = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MailServerPort = table.Column<int>(type: "int", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MailServer", x => x.MailServerId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MailServer");
        }
    }
}
