using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoSportsAPI.Migrations
{
    /// <inheritdoc />
    public partial class RedoBridge : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LocationSport_locations_LocationsId",
                table: "LocationSport");

            migrationBuilder.DropForeignKey(
                name: "FK_LocationSport_sports_SportsId",
                table: "LocationSport");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LocationSport",
                table: "LocationSport");

            migrationBuilder.RenameTable(
                name: "LocationSport",
                newName: "LocationSports");

            migrationBuilder.RenameIndex(
                name: "IX_LocationSport_SportsId",
                table: "LocationSports",
                newName: "IX_LocationSports_SportsId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LocationSports",
                table: "LocationSports",
                columns: new[] { "LocationsId", "SportsId" });

            migrationBuilder.AddForeignKey(
                name: "FK_LocationSports_locations_LocationsId",
                table: "LocationSports",
                column: "LocationsId",
                principalTable: "locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LocationSports_sports_SportsId",
                table: "LocationSports",
                column: "SportsId",
                principalTable: "sports",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LocationSports_locations_LocationsId",
                table: "LocationSports");

            migrationBuilder.DropForeignKey(
                name: "FK_LocationSports_sports_SportsId",
                table: "LocationSports");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LocationSports",
                table: "LocationSports");

            migrationBuilder.RenameTable(
                name: "LocationSports",
                newName: "LocationSport");

            migrationBuilder.RenameIndex(
                name: "IX_LocationSports_SportsId",
                table: "LocationSport",
                newName: "IX_LocationSport_SportsId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LocationSport",
                table: "LocationSport",
                columns: new[] { "LocationsId", "SportsId" });

            migrationBuilder.AddForeignKey(
                name: "FK_LocationSport_locations_LocationsId",
                table: "LocationSport",
                column: "LocationsId",
                principalTable: "locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LocationSport_sports_SportsId",
                table: "LocationSport",
                column: "SportsId",
                principalTable: "sports",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
