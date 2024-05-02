using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cargodi.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Initialv4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CarSchedule");

            migrationBuilder.DropTable(
                name: "TrailerSchedule");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CarSchedule",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CarId = table.Column<int>(type: "int", nullable: false),
                    Finish = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PlanFinish = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PlanStart = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Start = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarSchedule", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CarSchedule_Cars_CarId",
                        column: x => x.CarId,
                        principalTable: "Cars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TrailerSchedule",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrailerId = table.Column<int>(type: "int", nullable: false),
                    Finish = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PlanFinish = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PlanStart = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Start = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrailerSchedule", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrailerSchedule_Trailers_TrailerId",
                        column: x => x.TrailerId,
                        principalTable: "Trailers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CarSchedule_CarId",
                table: "CarSchedule",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_TrailerSchedule_TrailerId",
                table: "TrailerSchedule",
                column: "TrailerId");
        }
    }
}
