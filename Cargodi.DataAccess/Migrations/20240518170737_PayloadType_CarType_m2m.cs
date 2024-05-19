using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cargodi.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class PayloadType_CarType_m2m : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PayloadTypeId",
                table: "Payloads",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PayloadType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PayloadType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CarTypePayloadType",
                columns: table => new
                {
                    CarTypesId = table.Column<int>(type: "int", nullable: false),
                    PayloadTypesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarTypePayloadType", x => new { x.CarTypesId, x.PayloadTypesId });
                    table.ForeignKey(
                        name: "FK_CarTypePayloadType_CarType_CarTypesId",
                        column: x => x.CarTypesId,
                        principalTable: "CarType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CarTypePayloadType_PayloadType_PayloadTypesId",
                        column: x => x.PayloadTypesId,
                        principalTable: "PayloadType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Payloads_PayloadTypeId",
                table: "Payloads",
                column: "PayloadTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_CarTypePayloadType_PayloadTypesId",
                table: "CarTypePayloadType",
                column: "PayloadTypesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Payloads_PayloadType_PayloadTypeId",
                table: "Payloads",
                column: "PayloadTypeId",
                principalTable: "PayloadType",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payloads_PayloadType_PayloadTypeId",
                table: "Payloads");

            migrationBuilder.DropTable(
                name: "CarTypePayloadType");

            migrationBuilder.DropTable(
                name: "PayloadType");

            migrationBuilder.DropIndex(
                name: "IX_Payloads_PayloadTypeId",
                table: "Payloads");

            migrationBuilder.DropColumn(
                name: "PayloadTypeId",
                table: "Payloads");
        }
    }
}
