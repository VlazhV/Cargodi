using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cargodi.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class PayloadTypes_FK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payloads_PayloadType_PayloadTypeId",
                table: "Payloads");

            migrationBuilder.AlterColumn<int>(
                name: "PayloadTypeId",
                table: "Payloads",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PublicTypeId",
                table: "Payloads",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Payloads_PayloadType_PayloadTypeId",
                table: "Payloads",
                column: "PayloadTypeId",
                principalTable: "PayloadType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payloads_PayloadType_PayloadTypeId",
                table: "Payloads");

            migrationBuilder.DropColumn(
                name: "PublicTypeId",
                table: "Payloads");

            migrationBuilder.AlterColumn<int>(
                name: "PayloadTypeId",
                table: "Payloads",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Payloads_PayloadType_PayloadTypeId",
                table: "Payloads",
                column: "PayloadTypeId",
                principalTable: "PayloadType",
                principalColumn: "Id");
        }
    }
}
