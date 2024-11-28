using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeatherProject.Migrations
{
    /// <inheritdoc />
    public partial class WeatherDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VäderData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Datum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Medeltemperatur = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Medelluftfuktighet = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MögelRisk = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Vinter = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Höst = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VäderData", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VäderData");
        }
    }
}
