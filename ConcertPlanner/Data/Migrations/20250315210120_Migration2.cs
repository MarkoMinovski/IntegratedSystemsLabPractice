using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConcertPlanner.Data.Migrations
{
    /// <inheritdoc />
    public partial class Migration2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Concerts",
                columns: table => new
                {
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConcertName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConcertPlace = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConcertDate = table.Column<DateOnly>(type: "date", nullable: false),
                    ConcertPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Concerts", x => x.Guid);
                });

            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NumberOfPeople = table.Column<int>(type: "int", nullable: false),
                    ConcertGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PurchaserID = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.Guid);
                    table.ForeignKey(
                        name: "FK_Tickets_AspNetUsers_PurchaserID",
                        column: x => x.PurchaserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tickets_Concerts_ConcertGuid",
                        column: x => x.ConcertGuid,
                        principalTable: "Concerts",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_ConcertGuid",
                table: "Tickets",
                column: "ConcertGuid");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_PurchaserID",
                table: "Tickets",
                column: "PurchaserID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tickets");

            migrationBuilder.DropTable(
                name: "Concerts");
        }
    }
}
