using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoSportsAPI.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "locations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CurrentLobbyCount = table.Column<int>(type: "int", nullable: false),
                    MaxLobbyCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_locations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "sports",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sports", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "lobbies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SportId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lobbies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_lobbies_locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_lobbies_locations_SportId",
                        column: x => x.SportId,
                        principalTable: "locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "locationTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsIndoor = table.Column<bool>(type: "bit", nullable: false),
                    Surface = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HasLights = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_locationTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_locationTypes_locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "locationSports",
                columns: table => new
                {
                    LocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SportId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_locationSports", x => new { x.LocationId, x.SportId });
                    table.ForeignKey(
                        name: "FK_locationSports_locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_locationSports_sports_SportId",
                        column: x => x.SportId,
                        principalTable: "sports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_lobbies_LocationId",
                table: "lobbies",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_lobbies_SportId",
                table: "lobbies",
                column: "SportId");

            migrationBuilder.CreateIndex(
                name: "IX_locationSports_SportId",
                table: "locationSports",
                column: "SportId");

            migrationBuilder.CreateIndex(
                name: "IX_locationTypes_LocationId",
                table: "locationTypes",
                column: "LocationId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "lobbies");

            migrationBuilder.DropTable(
                name: "locationSports");

            migrationBuilder.DropTable(
                name: "locationTypes");

            migrationBuilder.DropTable(
                name: "sports");

            migrationBuilder.DropTable(
                name: "locations");
        }
    }
}
