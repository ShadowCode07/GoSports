using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoSportsAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "locations",
                columns: table => new
                {
                    LocationId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Latitude = table.Column<double>(type: "double", nullable: false),
                    Longitude = table.Column<double>(type: "double", nullable: false),
                    CurrentLobbyCount = table.Column<int>(type: "int", nullable: false),
                    MaxLobbyCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_locations", x => x.LocationId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "sports",
                columns: table => new
                {
                    SportId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Name = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sports", x => x.SportId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "locationTypes",
                columns: table => new
                {
                    LocationTypeId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    LocationId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsIndoor = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Surface = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    HasLights = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_locationTypes", x => x.LocationTypeId);
                    table.ForeignKey(
                        name: "FK_locationTypes_locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "locations",
                        principalColumn: "LocationId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "lobbies",
                columns: table => new
                {
                    LobbyId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Name = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LocationId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    SportId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lobbies", x => x.LobbyId);
                    table.ForeignKey(
                        name: "FK_lobbies_locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "locations",
                        principalColumn: "LocationId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_lobbies_sports_SportId",
                        column: x => x.SportId,
                        principalTable: "sports",
                        principalColumn: "SportId",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "LocationSports",
                columns: table => new
                {
                    LocationsLocationId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    SportsSportId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocationSports", x => new { x.LocationsLocationId, x.SportsSportId });
                    table.ForeignKey(
                        name: "FK_LocationSports_locations_LocationsLocationId",
                        column: x => x.LocationsLocationId,
                        principalTable: "locations",
                        principalColumn: "LocationId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LocationSports_sports_SportsSportId",
                        column: x => x.SportsSportId,
                        principalTable: "sports",
                        principalColumn: "SportId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_lobbies_LocationId",
                table: "lobbies",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_lobbies_Name",
                table: "lobbies",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_lobbies_SportId",
                table: "lobbies",
                column: "SportId");

            migrationBuilder.CreateIndex(
                name: "IX_LocationSports_SportsSportId",
                table: "LocationSports",
                column: "SportsSportId");

            migrationBuilder.CreateIndex(
                name: "IX_locationTypes_LocationId",
                table: "locationTypes",
                column: "LocationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_sports_Name",
                table: "sports",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "lobbies");

            migrationBuilder.DropTable(
                name: "LocationSports");

            migrationBuilder.DropTable(
                name: "locationTypes");

            migrationBuilder.DropTable(
                name: "sports");

            migrationBuilder.DropTable(
                name: "locations");
        }
    }
}
