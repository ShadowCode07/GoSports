using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoSportsAPI.Migrations
{
    /// <inheritdoc />
    public partial class ImplementBase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LocationSports_locations_LocationsId",
                table: "LocationSports");

            migrationBuilder.DropForeignKey(
                name: "FK_LocationSports_sports_SportsId",
                table: "LocationSports");

            migrationBuilder.DropForeignKey(
                name: "FK_UserProfiles_lobbies_LobbyId",
                table: "UserProfiles");

            migrationBuilder.DropForeignKey(
                name: "FK_UserSports_sports_SportsId",
                table: "UserSports");

            migrationBuilder.DropTable(
                name: "lobbies");

            migrationBuilder.DropTable(
                name: "locationTypes");

            migrationBuilder.DropTable(
                name: "locations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_sports",
                table: "sports");

            migrationBuilder.DropIndex(
                name: "IX_sports_Name",
                table: "sports");

            migrationBuilder.RenameTable(
                name: "sports",
                newName: "Base");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Base",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Base",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CurrentLobbyCount",
                table: "Base",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CurrentPlayerCount",
                table: "Base",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Base",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Base",
                type: "nvarchar(13)",
                maxLength: 13,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "HasLights",
                table: "Base",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsIndoor",
                table: "Base",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Latitude",
                table: "Base",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "LocationId",
                table: "Base",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "LocationType_LocationId",
                table: "Base",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LocationType_Name",
                table: "Base",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Location_Name",
                table: "Base",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Longitude",
                table: "Base",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MaxLobbyCount",
                table: "Base",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MaxPlayerCount",
                table: "Base",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SportId",
                table: "Base",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Sport_Name",
                table: "Base",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Surface",
                table: "Base",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Base",
                table: "Base",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Base_LocationId",
                table: "Base",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Base_LocationType_LocationId",
                table: "Base",
                column: "LocationType_LocationId",
                unique: true,
                filter: "[LocationId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Base_Name",
                table: "Base",
                column: "Name",
                unique: true,
                filter: "[Name] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Base_Sport_Name",
                table: "Base",
                column: "Sport_Name",
                unique: true,
                filter: "[Name] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Base_SportId",
                table: "Base",
                column: "SportId");

            migrationBuilder.AddForeignKey(
                name: "FK_Base_Base_LocationId",
                table: "Base",
                column: "LocationId",
                principalTable: "Base",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Base_Base_LocationType_LocationId",
                table: "Base",
                column: "LocationType_LocationId",
                principalTable: "Base",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Base_Base_SportId",
                table: "Base",
                column: "SportId",
                principalTable: "Base",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LocationSports_Base_LocationsId",
                table: "LocationSports",
                column: "LocationsId",
                principalTable: "Base",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LocationSports_Base_SportsId",
                table: "LocationSports",
                column: "SportsId",
                principalTable: "Base",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserProfiles_Base_LobbyId",
                table: "UserProfiles",
                column: "LobbyId",
                principalTable: "Base",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserSports_Base_SportsId",
                table: "UserSports",
                column: "SportsId",
                principalTable: "Base",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Base_Base_LocationId",
                table: "Base");

            migrationBuilder.DropForeignKey(
                name: "FK_Base_Base_LocationType_LocationId",
                table: "Base");

            migrationBuilder.DropForeignKey(
                name: "FK_Base_Base_SportId",
                table: "Base");

            migrationBuilder.DropForeignKey(
                name: "FK_LocationSports_Base_LocationsId",
                table: "LocationSports");

            migrationBuilder.DropForeignKey(
                name: "FK_LocationSports_Base_SportsId",
                table: "LocationSports");

            migrationBuilder.DropForeignKey(
                name: "FK_UserProfiles_Base_LobbyId",
                table: "UserProfiles");

            migrationBuilder.DropForeignKey(
                name: "FK_UserSports_Base_SportsId",
                table: "UserSports");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Base",
                table: "Base");

            migrationBuilder.DropIndex(
                name: "IX_Base_LocationId",
                table: "Base");

            migrationBuilder.DropIndex(
                name: "IX_Base_LocationType_LocationId",
                table: "Base");

            migrationBuilder.DropIndex(
                name: "IX_Base_Name",
                table: "Base");

            migrationBuilder.DropIndex(
                name: "IX_Base_Sport_Name",
                table: "Base");

            migrationBuilder.DropIndex(
                name: "IX_Base_SportId",
                table: "Base");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "Base");

            migrationBuilder.DropColumn(
                name: "CurrentLobbyCount",
                table: "Base");

            migrationBuilder.DropColumn(
                name: "CurrentPlayerCount",
                table: "Base");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Base");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Base");

            migrationBuilder.DropColumn(
                name: "HasLights",
                table: "Base");

            migrationBuilder.DropColumn(
                name: "IsIndoor",
                table: "Base");

            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "Base");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "Base");

            migrationBuilder.DropColumn(
                name: "LocationType_LocationId",
                table: "Base");

            migrationBuilder.DropColumn(
                name: "LocationType_Name",
                table: "Base");

            migrationBuilder.DropColumn(
                name: "Location_Name",
                table: "Base");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "Base");

            migrationBuilder.DropColumn(
                name: "MaxLobbyCount",
                table: "Base");

            migrationBuilder.DropColumn(
                name: "MaxPlayerCount",
                table: "Base");

            migrationBuilder.DropColumn(
                name: "SportId",
                table: "Base");

            migrationBuilder.DropColumn(
                name: "Sport_Name",
                table: "Base");

            migrationBuilder.DropColumn(
                name: "Surface",
                table: "Base");

            migrationBuilder.RenameTable(
                name: "Base",
                newName: "sports");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "sports",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_sports",
                table: "sports",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "locations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CurrentLobbyCount = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Latitude = table.Column<double>(type: "float", nullable: false),
                    Longitude = table.Column<double>(type: "float", nullable: false),
                    MaxLobbyCount = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Version = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_locations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "lobbies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SportId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CurrentPlayerCount = table.Column<int>(type: "int", nullable: false),
                    MaxPlayerCount = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Version = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
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
                        name: "FK_lobbies_sports_SportId",
                        column: x => x.SportId,
                        principalTable: "sports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "locationTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HasLights = table.Column<bool>(type: "bit", nullable: false),
                    IsIndoor = table.Column<bool>(type: "bit", nullable: false),
                    LocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Surface = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Version = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
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

            migrationBuilder.CreateIndex(
                name: "IX_sports_Name",
                table: "sports",
                column: "Name",
                unique: true);

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
                name: "IX_locationTypes_LocationId",
                table: "locationTypes",
                column: "LocationId",
                unique: true);

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

            migrationBuilder.AddForeignKey(
                name: "FK_UserProfiles_lobbies_LobbyId",
                table: "UserProfiles",
                column: "LobbyId",
                principalTable: "lobbies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserSports_sports_SportsId",
                table: "UserSports",
                column: "SportsId",
                principalTable: "sports",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
