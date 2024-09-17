using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FreightMana.Migrations
{
    /// <inheritdoc />
    public partial class mMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.Sql("IF OBJECT_ID('CusAccounts', 'U') IS NOT NULL DROP TABLE CusAccounts;");

			migrationBuilder.CreateTable(
                name: "CusAccounts",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    password = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_accounts", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Receivers",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    phoneNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    address = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_receivers", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Senders",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    phoneNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    address = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_senders", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Transports",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    cost = table.Column<float>(type: "real", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_transports", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Warehouses",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    hotline = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_warehouses", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    orderID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    product = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    numberOfProduct = table.Column<int>(type: "int", nullable: true),
                    transportID = table.Column<int>(type: "int", nullable: false),
                    cod = table.Column<float>(type: "real", nullable: true),
                    transportFee = table.Column<float>(type: "real", nullable: true),
                    note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    recordAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    cancelAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    confirmAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    completeAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    receiverID = table.Column<int>(type: "int", nullable: false),
                    senderID = table.Column<int>(type: "int", nullable: false),
                    warehouseID = table.Column<int>(type: "int", nullable: false),
                    status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    cusID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orders", x => x.orderID);
                    table.ForeignKey(
                        name: "FK_orders_accounts",
                        column: x => x.cusID,
                        principalTable: "CusAccounts",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_orders_receivers",
                        column: x => x.receiverID,
                        principalTable: "Receivers",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_orders_senders",
                        column: x => x.senderID,
                        principalTable: "Senders",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_orders_transports1",
                        column: x => x.transportID,
                        principalTable: "Transports",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_orders_warehouses",
                        column: x => x.warehouseID,
                        principalTable: "Warehouses",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Staffs",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    phoneNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    position = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    warehouseID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_employees", x => x.id);
                    table.ForeignKey(
                        name: "FK_employees_warehouses",
                        column: x => x.warehouseID,
                        principalTable: "Warehouses",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "WarehouseAccounts",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    password = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    warehouseID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_warehouseAccounts", x => x.id);
                    table.ForeignKey(
                        name: "FK_warehouseAccounts_warehouses",
                        column: x => x.warehouseID,
                        principalTable: "Warehouses",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Shifts",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    timeStart = table.Column<TimeOnly>(type: "time", nullable: true),
                    timeEnd = table.Column<TimeOnly>(type: "time", nullable: true),
                    employeeID = table.Column<int>(type: "int", nullable: true),
                    day = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_shifts", x => x.id);
                    table.ForeignKey(
                        name: "FK_shifts_employees",
                        column: x => x.employeeID,
                        principalTable: "Staffs",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_cusID",
                table: "Orders",
                column: "cusID");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_receiverID",
                table: "Orders",
                column: "receiverID");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_senderID",
                table: "Orders",
                column: "senderID");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_transportID",
                table: "Orders",
                column: "transportID");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_warehouseID",
                table: "Orders",
                column: "warehouseID");

            migrationBuilder.CreateIndex(
                name: "IX_Shifts_employeeID",
                table: "Shifts",
                column: "employeeID");

            migrationBuilder.CreateIndex(
                name: "IX_Staffs_warehouseID",
                table: "Staffs",
                column: "warehouseID");

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseAccounts_warehouseID",
                table: "WarehouseAccounts",
                column: "warehouseID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Shifts");

            migrationBuilder.DropTable(
                name: "WarehouseAccounts");

            migrationBuilder.DropTable(
                name: "CusAccounts");

            migrationBuilder.DropTable(
                name: "Receivers");

            migrationBuilder.DropTable(
                name: "Senders");

            migrationBuilder.DropTable(
                name: "Transports");

            migrationBuilder.DropTable(
                name: "Staffs");

            migrationBuilder.DropTable(
                name: "Warehouses");
        }
    }
}
