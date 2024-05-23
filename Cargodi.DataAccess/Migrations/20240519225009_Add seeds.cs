using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Cargodi.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Addseeds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CarTypeCategory");

            migrationBuilder.CreateTable(
                name: "CarTypesCategories",
                columns: table => new
                {
                    CarTypeId = table.Column<int>(type: "int", nullable: false),
                    CategoryName = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarTypesCategories", x => new { x.CarTypeId, x.CategoryName });
                    table.ForeignKey(
                        name: "FK_CarTypesCategories_CarType_CarTypeId",
                        column: x => x.CarTypeId,
                        principalTable: "CarType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CarTypesCategories_Categories_CategoryName",
                        column: x => x.CategoryName,
                        principalTable: "Categories",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "CarType",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Truck" },
                    { 2, "Van" },
                    { 3, "Passenger Car" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                column: "Name",
                values: new object[]
                {
                    "B",
                    "BE",
                    "C",
                    "CE"
                });

            migrationBuilder.InsertData(
                table: "DriverStatuses",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Works" },
                    { 2, "Vacations" },
                    { 3, "Sick Leave" }
                });

            migrationBuilder.InsertData(
                table: "PayloadType",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Item" },
                    { 2, "Liquid" },
                    { 3, "Bulk" }
                });

            migrationBuilder.InsertData(
                table: "TrailerType",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Cistern" },
                    { 2, "Bulker" },
                    { 3, "VanTrailer" }
                });

            migrationBuilder.InsertData(
                table: "CarTypesCategories",
                columns: new[] { "CarTypeId", "CategoryName" },
                values: new object[,]
                {
                    { 1, "C" },
                    { 1, "CE" },
                    { 2, "B" },
                    { 2, "C" },
                    { 3, "B" },
                    { 3, "BE" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CarTypesCategories_CategoryName",
                table: "CarTypesCategories",
                column: "CategoryName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CarTypesCategories");

            migrationBuilder.DeleteData(
                table: "CarType",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "CarType",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "CarType",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Name",
                keyValue: "B");

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Name",
                keyValue: "BE");

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Name",
                keyValue: "C");

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Name",
                keyValue: "CE");

            migrationBuilder.DeleteData(
                table: "DriverStatuses",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "DriverStatuses",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "DriverStatuses",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "PayloadType",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "PayloadType",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "PayloadType",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "TrailerType",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TrailerType",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "TrailerType",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.CreateTable(
                name: "CarTypeCategory",
                columns: table => new
                {
                    CarTypesId = table.Column<int>(type: "int", nullable: false),
                    CategoriesName = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarTypeCategory", x => new { x.CarTypesId, x.CategoriesName });
                    table.ForeignKey(
                        name: "FK_CarTypeCategory_CarType_CarTypesId",
                        column: x => x.CarTypesId,
                        principalTable: "CarType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CarTypeCategory_Categories_CategoriesName",
                        column: x => x.CategoriesName,
                        principalTable: "Categories",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CarTypeCategory_CategoriesName",
                table: "CarTypeCategory",
                column: "CategoriesName");
        }
    }
}
