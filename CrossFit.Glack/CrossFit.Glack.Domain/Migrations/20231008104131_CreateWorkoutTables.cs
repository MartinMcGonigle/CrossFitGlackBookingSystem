using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CrossFit.Glack.Domain.Migrations
{
    public partial class CreateWorkoutTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Strength",
                columns: table => new
                {
                    StrengthId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Strength", x => x.StrengthId);
                });

            migrationBuilder.CreateTable(
                name: "Warmup",
                columns: table => new
                {
                    WarmupId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Warmup", x => x.WarmupId);
                });

            migrationBuilder.CreateTable(
                name: "WOD",
                columns: table => new
                {
                    WODId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WOD", x => x.WODId);
                });

            migrationBuilder.CreateTable(
                name: "Workout",
                columns: table => new
                {
                    WorkoutId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    WarmupId = table.Column<long>(type: "bigint", nullable: true),
                    StrengthId = table.Column<long>(type: "bigint", nullable: true),
                    WODId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workout", x => x.WorkoutId);
                    table.ForeignKey(
                        name: "FK_Workout_Strength_StrengthId",
                        column: x => x.StrengthId,
                        principalTable: "Strength",
                        principalColumn: "StrengthId");
                    table.ForeignKey(
                        name: "FK_Workout_Warmup_WarmupId",
                        column: x => x.WarmupId,
                        principalTable: "Warmup",
                        principalColumn: "WarmupId");
                    table.ForeignKey(
                        name: "FK_Workout_WOD_WODId",
                        column: x => x.WODId,
                        principalTable: "WOD",
                        principalColumn: "WODId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Workout_StrengthId",
                table: "Workout",
                column: "StrengthId");

            migrationBuilder.CreateIndex(
                name: "IX_Workout_WarmupId",
                table: "Workout",
                column: "WarmupId");

            migrationBuilder.CreateIndex(
                name: "IX_Workout_WODId",
                table: "Workout",
                column: "WODId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Workout");

            migrationBuilder.DropTable(
                name: "Strength");

            migrationBuilder.DropTable(
                name: "Warmup");

            migrationBuilder.DropTable(
                name: "WOD");
        }
    }
}
