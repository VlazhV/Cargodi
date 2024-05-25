using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Cargodi.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Init_with_seeds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Latitude = table.Column<double>(type: "float", nullable: false),
                    Longitude = table.Column<double>(type: "float", nullable: false),
                    IsWest = table.Column<bool>(type: "bit", nullable: false),
                    IsNorth = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CarTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "DriverStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DriverStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderStatuses", x => x.Id);
                });

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

            migrationBuilder.CreateTable(
                name: "Autoparks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AddressId = table.Column<long>(type: "bigint", nullable: false),
                    Capacity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Autoparks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Autoparks_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<long>(type: "bigint", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    RoleId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Clients_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                        name: "FK_CarTypesCategories_CarTypes_CarTypeId",
                        column: x => x.CarTypeId,
                        principalTable: "CarTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CarTypesCategories_Categories_CategoryName",
                        column: x => x.CategoryName,
                        principalTable: "Categories",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LicenseNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Mark = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Range = table.Column<int>(type: "int", nullable: false),
                    Carrying = table.Column<int>(type: "int", nullable: false),
                    TankVolume = table.Column<int>(type: "int", nullable: false),
                    CapacityLength = table.Column<int>(type: "int", nullable: false),
                    CapacityWidth = table.Column<int>(type: "int", nullable: false),
                    CapacityHeight = table.Column<int>(type: "int", nullable: false),
                    AutoparkId = table.Column<int>(type: "int", nullable: false),
                    ActualAutoparkId = table.Column<int>(type: "int", nullable: false),
                    CarTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cars_Autoparks_ActualAutoparkId",
                        column: x => x.ActualAutoparkId,
                        principalTable: "Autoparks",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Cars_Autoparks_AutoparkId",
                        column: x => x.AutoparkId,
                        principalTable: "Autoparks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cars_CarTypes_CarTypeId",
                        column: x => x.CarTypeId,
                        principalTable: "CarTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Drivers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AutoparkId = table.Column<int>(type: "int", nullable: false),
                    ActualAutoparkId = table.Column<int>(type: "int", nullable: false),
                    License = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SecondName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MiddleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    DriverStatusId = table.Column<int>(type: "int", nullable: false),
                    EmployDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FireDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CarTypeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drivers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Drivers_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Drivers_Autoparks_ActualAutoparkId",
                        column: x => x.ActualAutoparkId,
                        principalTable: "Autoparks",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Drivers_Autoparks_AutoparkId",
                        column: x => x.AutoparkId,
                        principalTable: "Autoparks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Drivers_CarTypes_CarTypeId",
                        column: x => x.CarTypeId,
                        principalTable: "CarTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Drivers_DriverStatuses_DriverStatusId",
                        column: x => x.DriverStatusId,
                        principalTable: "DriverStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Operators",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AutoparkId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    SecondName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MiddleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmployDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FireDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Operators", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Operators_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Operators_Autoparks_AutoparkId",
                        column: x => x.AutoparkId,
                        principalTable: "Autoparks",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Trailers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LicenseNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CapacityLength = table.Column<int>(type: "int", nullable: false),
                    CapacityWidth = table.Column<int>(type: "int", nullable: false),
                    CapacityHeight = table.Column<int>(type: "int", nullable: false),
                    Carrying = table.Column<int>(type: "int", nullable: false),
                    ActualAutoparkId = table.Column<int>(type: "int", nullable: false),
                    AutoparkId = table.Column<int>(type: "int", nullable: false),
                    TrailerTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trailers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Trailers_Autoparks_ActualAutoparkId",
                        column: x => x.ActualAutoparkId,
                        principalTable: "Autoparks",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Trailers_Autoparks_AutoparkId",
                        column: x => x.AutoparkId,
                        principalTable: "Autoparks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Trailers_TrailerType_TrailerTypeId",
                        column: x => x.TrailerTypeId,
                        principalTable: "TrailerType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CategoryDriver",
                columns: table => new
                {
                    CategoriesName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DriversId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryDriver", x => new { x.CategoriesName, x.DriversId });
                    table.ForeignKey(
                        name: "FK_CategoryDriver_Categories_CategoriesName",
                        column: x => x.CategoriesName,
                        principalTable: "Categories",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryDriver_Drivers_DriversId",
                        column: x => x.DriversId,
                        principalTable: "Drivers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AcceptTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LoadAddressId = table.Column<long>(type: "bigint", nullable: false),
                    DeliverAddressId = table.Column<long>(type: "bigint", nullable: false),
                    ClientId = table.Column<long>(type: "bigint", nullable: false),
                    OrderStatusId = table.Column<int>(type: "int", nullable: false),
                    OperatorId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Addresses_DeliverAddressId",
                        column: x => x.DeliverAddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Orders_Addresses_LoadAddressId",
                        column: x => x.LoadAddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Orders_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_Operators_OperatorId",
                        column: x => x.OperatorId,
                        principalTable: "Operators",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Orders_OrderStatuses_OrderStatusId",
                        column: x => x.OrderStatusId,
                        principalTable: "OrderStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ships",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CarId = table.Column<int>(type: "int", nullable: false),
                    TrailerId = table.Column<int>(type: "int", nullable: true),
                    Start = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Finish = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OperatorId = table.Column<int>(type: "int", nullable: false),
                    AutoparkStartId = table.Column<int>(type: "int", nullable: false),
                    AutoparkFinishId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ships", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ships_Autoparks_AutoparkFinishId",
                        column: x => x.AutoparkFinishId,
                        principalTable: "Autoparks",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Ships_Autoparks_AutoparkStartId",
                        column: x => x.AutoparkStartId,
                        principalTable: "Autoparks",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Ships_Cars_CarId",
                        column: x => x.CarId,
                        principalTable: "Cars",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Ships_Operators_OperatorId",
                        column: x => x.OperatorId,
                        principalTable: "Operators",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ships_Trailers_TrailerId",
                        column: x => x.TrailerId,
                        principalTable: "Trailers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Payloads",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Length = table.Column<int>(type: "int", nullable: false),
                    Width = table.Column<int>(type: "int", nullable: false),
                    Height = table.Column<int>(type: "int", nullable: false),
                    Weight = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrderId = table.Column<long>(type: "bigint", nullable: false),
                    PayloadTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payloads", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payloads_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Payloads_PayloadType_PayloadTypeId",
                        column: x => x.PayloadTypeId,
                        principalTable: "PayloadType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DriverShip",
                columns: table => new
                {
                    DriversId = table.Column<int>(type: "int", nullable: false),
                    ShipsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DriverShip", x => new { x.DriversId, x.ShipsId });
                    table.ForeignKey(
                        name: "FK_DriverShip_Drivers_DriversId",
                        column: x => x.DriversId,
                        principalTable: "Drivers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DriverShip_Ships_ShipsId",
                        column: x => x.ShipsId,
                        principalTable: "Ships",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Stops",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<short>(type: "smallint", nullable: false),
                    ShipId = table.Column<int>(type: "int", nullable: false),
                    OrderId = table.Column<long>(type: "bigint", nullable: false),
                    Time = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stops", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Stops_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Stops_Ships_ShipId",
                        column: x => x.ShipId,
                        principalTable: "Ships",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Addresses",
                columns: new[] { "Id", "IsNorth", "IsWest", "Latitude", "Longitude", "Name" },
                values: new object[,]
                {
                    { 1L, true, false, 53.935541000000001, 27.626106, "г. Минск, ул. Светлая, 23" },
                    { 2L, true, false, 53.945487, 27.094536000000002, "Минск р-н, ул. Родниковая, 2" },
                    { 3L, true, false, 53.825226999999998, 27.536000000000001, "Минск р-н, Сеница, Слуцкая улица, 37А" },
                    { 4L, true, false, 53.873240000000003, 27.625467, "г. Минск, ул. Народная, 29" },
                    { 5L, true, false, 53.913356999999998, 27.525995999999999, "г. Минск, ул. Москвина, 1" },
                    { 6L, true, false, 53.950822000000002, 27.569049, "г. Минск, улица Стефании Станюты, 17" },
                    { 7L, true, false, 53.903579000000001, 27.554373999999999, "Минск, площадь Свободы, 11" },
                    { 8L, true, false, 53.916353000000001, 27.549897000000001, "г. Минск, проспект Машерова, 35А" },
                    { 9L, true, false, 53.976832000000002, 27.544625, "р-н Минск, Якубовичи, Луговая улица, 26" },
                    { 10L, true, false, 53.990738, 27.627642000000002, "Любимая улица, 2, деревня Дроздово, Боровлянский сельсовет, Минский район" },
                    { 11L, true, false, 53.955250999999997, 27.776284, "Минская улица, 7, агрогородок Колодищи, Минский район" },
                    { 12L, true, false, 53.746403999999998, 27.566379000000001, "Парковая улица, 65, агрогородок Чуриловичи, Михановичский сельсовет, Минский район" }
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { 1L, null, "client", "CLIENT" },
                    { 2L, null, "manager", "MANAGER" },
                    { 3L, null, "admin", "ADMIN" },
                    { 4L, null, "driver", "DRIVER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { 1L, 0, "1ce8f54c-8405-4d82-a3aa-797ab2b45550", "admin@", false, false, null, "ADMIN@MAIL.RU", "ADMIN", "AQAAAAIAAYagAAAAEPY7WVyiuYib5RDDYNF3XssipR4zfeSYWmJCBMUK0QRuLHmIlcIYuWqdt5eYK3kF3A==", "+375441114488", false, null, false, "admin" },
                    { 2L, 0, "2586ec42-77b4-47f3-b8f4-7b636dd678d3", "operator1@mail.ru", false, false, null, "OPERATOR1@MAIL.RU", "OPERATOR1", "AQAAAAIAAYagAAAAEH/G2ct0XwgOoXaRTJgWkU46UKxDqXUiRQvfn4x00qh/zkq5V1XNx4ChedR0p63gDA==", "+375445114488", false, null, false, "operator1" },
                    { 3L, 0, "bb3c7e16-3044-432e-a2f0-3fb528cfd048", "client1@mail.ru", false, false, null, "CLIENT1@MAIL.RU", "CLIENT1", "AQAAAAIAAYagAAAAELYjdYNv1Bu8vDEoo2K88b9JlZ5nnKaj0mWHfnpsLNxVbhgr2sI38TT+OeybjlQQTQ==", "+375442114488", false, null, false, "client1" },
                    { 4L, 0, "0747c06e-9ea8-4716-8c1c-facd90a2684b", "client2@mail.ru", false, false, null, "CLIENT3@MAIL.RU", "CLIENT2", "AQAAAAIAAYagAAAAECnMN9e0UKaIefkZrStZ+cLcg3tnXmByAVeP1EAFT5/klg1w5sbWtIBftabugKpg5Q==", "+375443114488", false, null, false, "client2" },
                    { 5L, 0, "9dc1d642-115f-4eb6-8c68-059c55b715c1", "driver1@mail.ru", false, false, null, "DRIVER1@MAIL.RU", "DRIVER1", "AQAAAAIAAYagAAAAEH4egC8xBaRAzXjWavUWB3vBXn4asC7mzChJRWRYUSZIu8hg0DWadayXzCGY/+crjg==", "+375447114488", false, null, false, "driver1" },
                    { 6L, 0, "c2f7b8e0-6e77-44b9-8761-7f4a4167ceaa", "driver2@mail.ru", false, false, null, "DRIVER2@MAIL.RU", "DRIVER2", "AQAAAAIAAYagAAAAEDb0E7W3P+NIVtSt3hKKYig8Qnu3IXDQFxlnjCE+OHyeP0vhjLNimpFMxY9rDJoixQ==", "+375448114488", false, null, false, "driver2" }
                });

            migrationBuilder.InsertData(
                table: "CarTypes",
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
                table: "OrderStatuses",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "processing" },
                    { 2, "accepted" },
                    { 4, "declined" }
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
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { 3L, 1L },
                    { 2L, 2L },
                    { 1L, 3L },
                    { 1L, 4L },
                    { 4L, 5L },
                    { 4L, 6L }
                });

            migrationBuilder.InsertData(
                table: "Autoparks",
                columns: new[] { "Id", "AddressId", "Capacity" },
                values: new object[,]
                {
                    { 1, 1L, 250 },
                    { 2, 2L, 200 }
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

            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "Id", "Name", "UserId" },
                values: new object[,]
                {
                    { 1L, "Афанасий", 3L },
                    { 2L, "Валерий", 4L }
                });

            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "Id", "ActualAutoparkId", "AutoparkId", "CapacityHeight", "CapacityLength", "CapacityWidth", "CarTypeId", "Carrying", "LicenseNumber", "Mark", "Range", "TankVolume" },
                values: new object[,]
                {
                    { 1, 1, 1, 1575, 6516, 1888, 3, 6002618, "CA7586", "Mercedes", 306, 78 },
                    { 2, 2, 2, 1319, 1292, 1338, 3, 4509976, "YP2095", "Volkswagen", 952, 38 },
                    { 3, 1, 1, 2238, 6460, 1066, 2, 6647269, "EB6567", "Volkswagen", 748, 99 },
                    { 4, 1, 1, 1553, 1190, 1387, 3, 1837588, "KT6096", "Nissan", 513, 45 },
                    { 5, 1, 1, 1519, 6329, 2100, 3, 2156464, "AO2408", "Ford", 482, 99 },
                    { 6, 1, 1, 2883, 3501, 2842, 3, 3733785, "NC9615", "Honda", 509, 42 },
                    { 7, 1, 1, 1981, 4535, 2950, 1, 1480816, "EE6163", "Ford", 523, 51 },
                    { 8, 2, 2, 2351, 2955, 2341, 1, 5985121, "PP3625", "Mercedes", 360, 78 },
                    { 9, 2, 2, 2286, 6899, 2558, 3, 1603531, "PY6886", "Hyundai", 131, 76 },
                    { 10, 1, 1, 1824, 3895, 1921, 2, 2926249, "YH4938", "Mercedes", 455, 95 }
                });

            migrationBuilder.InsertData(
                table: "Drivers",
                columns: new[] { "Id", "ActualAutoparkId", "AutoparkId", "CarTypeId", "DriverStatusId", "EmployDate", "FireDate", "FirstName", "License", "MiddleName", "SecondName", "UserId" },
                values: new object[,]
                {
                    { 1, 1, 1, null, 1, new DateTime(2024, 4, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Иванов", "HE24086580", "Иванович", "Иван", 5L },
                    { 2, 2, 2, null, 2, new DateTime(2021, 6, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Василий", "AY66995610", "Васильевич", "Васильев", 6L }
                });

            migrationBuilder.InsertData(
                table: "Operators",
                columns: new[] { "Id", "AutoparkId", "EmployDate", "FireDate", "FirstName", "MiddleName", "SecondName", "UserId" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2022, 2, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Дмитрий", "Владимирович", "Попов", 1L },
                    { 2, 1, new DateTime(2023, 10, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Михаил", "Михаилович", "Шумахер", 2L }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "AcceptTime", "ClientId", "DeliverAddressId", "LoadAddressId", "OperatorId", "OrderStatusId", "Time" },
                values: new object[,]
                {
                    { 1L, new DateTime(2024, 5, 25, 20, 49, 19, 263, DateTimeKind.Utc).AddTicks(9356), 2L, 8L, 3L, null, 2, new DateTime(2024, 2, 11, 15, 16, 13, 0, DateTimeKind.Unspecified) },
                    { 2L, new DateTime(2024, 5, 25, 20, 49, 19, 263, DateTimeKind.Utc).AddTicks(9375), 2L, 9L, 4L, null, 2, new DateTime(2024, 3, 5, 16, 37, 44, 0, DateTimeKind.Unspecified) },
                    { 3L, new DateTime(2024, 5, 25, 20, 49, 19, 263, DateTimeKind.Utc).AddTicks(9386), 2L, 10L, 5L, null, 2, new DateTime(2024, 2, 6, 18, 55, 30, 0, DateTimeKind.Unspecified) },
                    { 4L, new DateTime(2024, 5, 25, 20, 49, 19, 263, DateTimeKind.Utc).AddTicks(9397), 2L, 11L, 6L, null, 2, new DateTime(2024, 2, 22, 13, 23, 30, 0, DateTimeKind.Unspecified) },
                    { 5L, null, 2L, 12L, 7L, null, 1, new DateTime(2024, 1, 25, 17, 36, 53, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Trailers",
                columns: new[] { "Id", "ActualAutoparkId", "AutoparkId", "CapacityHeight", "CapacityLength", "CapacityWidth", "Carrying", "LicenseNumber", "TrailerTypeId" },
                values: new object[,]
                {
                    { 1, 2, 2, 2016, 4222, 1538, 2756070, "BB1679", 2 },
                    { 2, 2, 2, 2422, 3332, 1507, 5094253, "KE1010", 3 },
                    { 3, 2, 2, 1050, 4318, 2100, 1343608, "ET2336", 2 },
                    { 4, 1, 1, 1135, 3128, 1420, 3951118, "NA5607", 1 },
                    { 5, 1, 1, 1763, 4644, 1015, 649325, "TB7979", 3 },
                    { 6, 2, 2, 1193, 6715, 1795, 3960815, "BP5298", 2 },
                    { 7, 1, 1, 1575, 5546, 1949, 5241592, "MC8780", 1 },
                    { 8, 1, 1, 2138, 1910, 2820, 6786452, "BA3861", 3 },
                    { 9, 2, 2, 2995, 2014, 2544, 4818141, "YN8685", 1 },
                    { 10, 2, 2, 2650, 5461, 1326, 2281930, "NC4381", 3 }
                });

            migrationBuilder.InsertData(
                table: "Payloads",
                columns: new[] { "Id", "Description", "Height", "Length", "OrderId", "PayloadTypeId", "Weight", "Width" },
                values: new object[,]
                {
                    { 1L, "", 4611, 5645, 3L, 3, 4273898, 5932 },
                    { 2L, "", 6690, 1334, 2L, 2, 3341359, 4529 },
                    { 3L, "", 1782, 5789, 5L, 1, 3313001, 2990 },
                    { 4L, "", 3404, 3778, 5L, 3, 2064781, 6451 },
                    { 5L, "", 570, 3622, 2L, 1, 3811111, 5444 },
                    { 6L, "", 672, 4566, 5L, 2, 4580979, 1023 },
                    { 7L, "", 5102, 3140, 3L, 1, 2883991, 3600 },
                    { 8L, "", 1890, 6876, 3L, 1, 2506404, 3494 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Autoparks_AddressId",
                table: "Autoparks",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_ActualAutoparkId",
                table: "Cars",
                column: "ActualAutoparkId");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_AutoparkId",
                table: "Cars",
                column: "AutoparkId");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_CarTypeId",
                table: "Cars",
                column: "CarTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_LicenseNumber",
                table: "Cars",
                column: "LicenseNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CarTypesCategories_CategoryName",
                table: "CarTypesCategories",
                column: "CategoryName");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryDriver_DriversId",
                table: "CategoryDriver",
                column: "DriversId");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_UserId",
                table: "Clients",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Drivers_ActualAutoparkId",
                table: "Drivers",
                column: "ActualAutoparkId");

            migrationBuilder.CreateIndex(
                name: "IX_Drivers_AutoparkId",
                table: "Drivers",
                column: "AutoparkId");

            migrationBuilder.CreateIndex(
                name: "IX_Drivers_CarTypeId",
                table: "Drivers",
                column: "CarTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Drivers_DriverStatusId",
                table: "Drivers",
                column: "DriverStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Drivers_UserId",
                table: "Drivers",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DriverShip_ShipsId",
                table: "DriverShip",
                column: "ShipsId");

            migrationBuilder.CreateIndex(
                name: "IX_Operators_AutoparkId",
                table: "Operators",
                column: "AutoparkId");

            migrationBuilder.CreateIndex(
                name: "IX_Operators_UserId",
                table: "Operators",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ClientId",
                table: "Orders",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_DeliverAddressId",
                table: "Orders",
                column: "DeliverAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_LoadAddressId",
                table: "Orders",
                column: "LoadAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_OperatorId",
                table: "Orders",
                column: "OperatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_OrderStatusId",
                table: "Orders",
                column: "OrderStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Payloads_OrderId",
                table: "Payloads",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Payloads_PayloadTypeId",
                table: "Payloads",
                column: "PayloadTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Ships_AutoparkFinishId",
                table: "Ships",
                column: "AutoparkFinishId");

            migrationBuilder.CreateIndex(
                name: "IX_Ships_AutoparkStartId",
                table: "Ships",
                column: "AutoparkStartId");

            migrationBuilder.CreateIndex(
                name: "IX_Ships_CarId",
                table: "Ships",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_Ships_OperatorId",
                table: "Ships",
                column: "OperatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Ships_TrailerId",
                table: "Ships",
                column: "TrailerId");

            migrationBuilder.CreateIndex(
                name: "IX_Stops_OrderId",
                table: "Stops",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Stops_ShipId",
                table: "Stops",
                column: "ShipId");

            migrationBuilder.CreateIndex(
                name: "IX_Trailers_ActualAutoparkId",
                table: "Trailers",
                column: "ActualAutoparkId");

            migrationBuilder.CreateIndex(
                name: "IX_Trailers_AutoparkId",
                table: "Trailers",
                column: "AutoparkId");

            migrationBuilder.CreateIndex(
                name: "IX_Trailers_LicenseNumber",
                table: "Trailers",
                column: "LicenseNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Trailers_TrailerTypeId",
                table: "Trailers",
                column: "TrailerTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "CarTypesCategories");

            migrationBuilder.DropTable(
                name: "CategoryDriver");

            migrationBuilder.DropTable(
                name: "DriverShip");

            migrationBuilder.DropTable(
                name: "Payloads");

            migrationBuilder.DropTable(
                name: "Stops");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Drivers");

            migrationBuilder.DropTable(
                name: "PayloadType");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Ships");

            migrationBuilder.DropTable(
                name: "DriverStatuses");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "OrderStatuses");

            migrationBuilder.DropTable(
                name: "Cars");

            migrationBuilder.DropTable(
                name: "Operators");

            migrationBuilder.DropTable(
                name: "Trailers");

            migrationBuilder.DropTable(
                name: "CarTypes");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Autoparks");

            migrationBuilder.DropTable(
                name: "TrailerType");

            migrationBuilder.DropTable(
                name: "Addresses");
        }
    }
}
