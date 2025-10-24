using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoSportsAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddLatitudeAndLongitude : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Latitude",
                table: "locations",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Longitude",
                table: "locations",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "locations");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "locations");
        }
    }
}
