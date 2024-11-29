using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeatherProject.Migrations
{
    /// <inheritdoc />
    public partial class AddPlatsTemperaturLuftfuktighet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Höst",
                table: "VäderData");

            migrationBuilder.DropColumn(
                name: "Medelluftfuktighet",
                table: "VäderData");

            migrationBuilder.RenameColumn(
                name: "Vinter",
                table: "VäderData",
                newName: "Plats");

            migrationBuilder.RenameColumn(
                name: "MögelRisk",
                table: "VäderData",
                newName: "Temperatur");

            migrationBuilder.RenameColumn(
                name: "Medeltemperatur",
                table: "VäderData",
                newName: "Luftfuktighet");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Temperatur",
                table: "VäderData",
                newName: "MögelRisk");

            migrationBuilder.RenameColumn(
                name: "Plats",
                table: "VäderData",
                newName: "Vinter");

            migrationBuilder.RenameColumn(
                name: "Luftfuktighet",
                table: "VäderData",
                newName: "Medeltemperatur");

            migrationBuilder.AddColumn<string>(
                name: "Höst",
                table: "VäderData",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "Medelluftfuktighet",
                table: "VäderData",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
