using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoSportsAPI.Migrations
{
    /// <inheritdoc />
    public partial class nameAdjust : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_lobby_Location_LocationId",
                table: "lobby");

            migrationBuilder.DropForeignKey(
                name: "FK_locationType_Location_LocationId",
                table: "locationType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_locationType",
                table: "locationType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Location",
                table: "Location");

            migrationBuilder.DropPrimaryKey(
                name: "PK_lobby",
                table: "lobby");

            migrationBuilder.RenameTable(
                name: "locationType",
                newName: "locationTypes");

            migrationBuilder.RenameTable(
                name: "Location",
                newName: "locations");

            migrationBuilder.RenameTable(
                name: "lobby",
                newName: "lobbies");

            migrationBuilder.RenameIndex(
                name: "IX_locationType_LocationId",
                table: "locationTypes",
                newName: "IX_locationTypes_LocationId");

            migrationBuilder.RenameIndex(
                name: "IX_lobby_LocationId",
                table: "lobbies",
                newName: "IX_lobbies_LocationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_locationTypes",
                table: "locationTypes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_locations",
                table: "locations",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_lobbies",
                table: "lobbies",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_lobbies_locations_LocationId",
                table: "lobbies",
                column: "LocationId",
                principalTable: "locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_locationTypes_locations_LocationId",
                table: "locationTypes",
                column: "LocationId",
                principalTable: "locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_lobbies_locations_LocationId",
                table: "lobbies");

            migrationBuilder.DropForeignKey(
                name: "FK_locationTypes_locations_LocationId",
                table: "locationTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_locationTypes",
                table: "locationTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_locations",
                table: "locations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_lobbies",
                table: "lobbies");

            migrationBuilder.RenameTable(
                name: "locationTypes",
                newName: "locationType");

            migrationBuilder.RenameTable(
                name: "locations",
                newName: "Location");

            migrationBuilder.RenameTable(
                name: "lobbies",
                newName: "lobby");

            migrationBuilder.RenameIndex(
                name: "IX_locationTypes_LocationId",
                table: "locationType",
                newName: "IX_locationType_LocationId");

            migrationBuilder.RenameIndex(
                name: "IX_lobbies_LocationId",
                table: "lobby",
                newName: "IX_lobby_LocationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_locationType",
                table: "locationType",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Location",
                table: "Location",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_lobby",
                table: "lobby",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_lobby_Location_LocationId",
                table: "lobby",
                column: "LocationId",
                principalTable: "Location",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_locationType_Location_LocationId",
                table: "locationType",
                column: "LocationId",
                principalTable: "Location",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
