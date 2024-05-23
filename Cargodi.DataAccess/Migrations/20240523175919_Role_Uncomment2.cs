using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Cargodi.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Role_Uncomment2 : Migration
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
                    AcceptTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LoadAddressId = table.Column<long>(type: "bigint", nullable: false),
                    DeliverAddressId = table.Column<long>(type: "bigint", nullable: false),
                    ClientId = table.Column<long>(type: "bigint", nullable: false),
                    OrderStatusId = table.Column<int>(type: "int", nullable: false),
                    OperatorId = table.Column<int>(type: "int", nullable: false)
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
