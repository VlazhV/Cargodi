using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cargodi.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Remove_Odd_Relationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CarTypePayloadType");

            migrationBuilder.DeleteData(
                table: "OrderStatuses",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.AddColumn<int>(
                name: "TrailerTypeId",
                table: "Trailers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "TrailerType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrailerType", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "OrderStatuses",
                columns: new[] { "Id", "Name" },
                values: new object[] { 4, "declined" });

            migrationBuilder.CreateIndex(
                name: "IX_Trailers_TrailerTypeId",
                table: "Trailers",
                column: "TrailerTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Trailers_TrailerType_TrailerTypeId",
                table: "Trailers",
                column: "TrailerTypeId",
                principalTable: "TrailerType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trailers_TrailerType_TrailerTypeId",
                table: "Trailers");

            migrationBuilder.DropTable(
                name: "TrailerType");

            migrationBuilder.DropIndex(
                name: "IX_Trailers_TrailerTypeId",
                table: "Trailers");

            migrationBuilder.DeleteData(
                table: "OrderStatuses",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DropColumn(
                name: "TrailerTypeId",
                table: "Trailers");

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

            migrationBuilder.InsertData(
                table: "OrderStatuses",
                columns: new[] { "Id", "Name" },
                values: new object[] { 3, "declined" });

            migrationBuilder.CreateIndex(
                name: "IX_CarTypePayloadType_PayloadTypesId",
                table: "CarTypePayloadType",
                column: "PayloadTypesId");
        }
    }
}
