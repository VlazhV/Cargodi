using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cargodi.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class migration3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Drivers_DriverStatuses_DriverStatusId",
                table: "Drivers");

            migrationBuilder.AddColumn<int>(
                name: "ActualAutoparkId",
                table: "Trailers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AutoparkFinishId",
                table: "Ships",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AutoparkStartId",
                table: "Ships",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "DriverStatusId",
                table: "Drivers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ActualAutoparkId",
                table: "Drivers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ActualAutoparkId",
                table: "Cars",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Trailers_ActualAutoparkId",
                table: "Trailers",
                column: "ActualAutoparkId");

            migrationBuilder.CreateIndex(
                name: "IX_Ships_AutoparkFinishId",
                table: "Ships",
                column: "AutoparkFinishId");

            migrationBuilder.CreateIndex(
                name: "IX_Ships_AutoparkStartId",
                table: "Ships",
                column: "AutoparkStartId");

            migrationBuilder.CreateIndex(
                name: "IX_Drivers_ActualAutoparkId",
                table: "Drivers",
                column: "ActualAutoparkId");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_ActualAutoparkId",
                table: "Cars",
                column: "ActualAutoparkId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Autoparks_ActualAutoparkId",
                table: "Cars",
                column: "ActualAutoparkId",
                principalTable: "Autoparks",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Drivers_Autoparks_ActualAutoparkId",
                table: "Drivers",
                column: "ActualAutoparkId",
                principalTable: "Autoparks",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Drivers_DriverStatuses_DriverStatusId",
                table: "Drivers",
                column: "DriverStatusId",
                principalTable: "DriverStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ships_Autoparks_AutoparkFinishId",
                table: "Ships",
                column: "AutoparkFinishId",
                principalTable: "Autoparks",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Ships_Autoparks_AutoparkStartId",
                table: "Ships",
                column: "AutoparkStartId",
                principalTable: "Autoparks",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Trailers_Autoparks_ActualAutoparkId",
                table: "Trailers",
                column: "ActualAutoparkId",
                principalTable: "Autoparks",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Autoparks_ActualAutoparkId",
                table: "Cars");

            migrationBuilder.DropForeignKey(
                name: "FK_Drivers_Autoparks_ActualAutoparkId",
                table: "Drivers");

            migrationBuilder.DropForeignKey(
                name: "FK_Drivers_DriverStatuses_DriverStatusId",
                table: "Drivers");

            migrationBuilder.DropForeignKey(
                name: "FK_Ships_Autoparks_AutoparkFinishId",
                table: "Ships");

            migrationBuilder.DropForeignKey(
                name: "FK_Ships_Autoparks_AutoparkStartId",
                table: "Ships");

            migrationBuilder.DropForeignKey(
                name: "FK_Trailers_Autoparks_ActualAutoparkId",
                table: "Trailers");

            migrationBuilder.DropIndex(
                name: "IX_Trailers_ActualAutoparkId",
                table: "Trailers");

            migrationBuilder.DropIndex(
                name: "IX_Ships_AutoparkFinishId",
                table: "Ships");

            migrationBuilder.DropIndex(
                name: "IX_Ships_AutoparkStartId",
                table: "Ships");

            migrationBuilder.DropIndex(
                name: "IX_Drivers_ActualAutoparkId",
                table: "Drivers");

            migrationBuilder.DropIndex(
                name: "IX_Cars_ActualAutoparkId",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "ActualAutoparkId",
                table: "Trailers");

            migrationBuilder.DropColumn(
                name: "AutoparkFinishId",
                table: "Ships");

            migrationBuilder.DropColumn(
                name: "AutoparkStartId",
                table: "Ships");

            migrationBuilder.DropColumn(
                name: "ActualAutoparkId",
                table: "Drivers");

            migrationBuilder.DropColumn(
                name: "ActualAutoparkId",
                table: "Cars");

            migrationBuilder.AlterColumn<int>(
                name: "DriverStatusId",
                table: "Drivers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Drivers_DriverStatuses_DriverStatusId",
                table: "Drivers",
                column: "DriverStatusId",
                principalTable: "DriverStatuses",
                principalColumn: "Id");
        }
    }
}
