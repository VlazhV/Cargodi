using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cargodi.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Add_dbset_CarTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_CarType_CarTypeId",
                table: "Cars");

            migrationBuilder.DropForeignKey(
                name: "FK_CarTypesCategories_CarType_CarTypeId",
                table: "CarTypesCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_Drivers_CarType_CarTypeId",
                table: "Drivers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CarType",
                table: "CarType");

            migrationBuilder.RenameTable(
                name: "CarType",
                newName: "CarTypes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CarTypes",
                table: "CarTypes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_CarTypes_CarTypeId",
                table: "Cars",
                column: "CarTypeId",
                principalTable: "CarTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CarTypesCategories_CarTypes_CarTypeId",
                table: "CarTypesCategories",
                column: "CarTypeId",
                principalTable: "CarTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Drivers_CarTypes_CarTypeId",
                table: "Drivers",
                column: "CarTypeId",
                principalTable: "CarTypes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_CarTypes_CarTypeId",
                table: "Cars");

            migrationBuilder.DropForeignKey(
                name: "FK_CarTypesCategories_CarTypes_CarTypeId",
                table: "CarTypesCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_Drivers_CarTypes_CarTypeId",
                table: "Drivers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CarTypes",
                table: "CarTypes");

            migrationBuilder.RenameTable(
                name: "CarTypes",
                newName: "CarType");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CarType",
                table: "CarType",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_CarType_CarTypeId",
                table: "Cars",
                column: "CarTypeId",
                principalTable: "CarType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CarTypesCategories_CarType_CarTypeId",
                table: "CarTypesCategories",
                column: "CarTypeId",
                principalTable: "CarType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Drivers_CarType_CarTypeId",
                table: "Drivers",
                column: "CarTypeId",
                principalTable: "CarType",
                principalColumn: "Id");
        }
    }
}
