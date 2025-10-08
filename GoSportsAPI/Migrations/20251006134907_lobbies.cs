using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoSportsAPI.Migrations
{
    /// <inheritdoc />
    public partial class lobbies : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CurrentLobbyCount",
                table: "Location",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MaxLobbyCount",
                table: "Location",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "lobby",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lobby", x => x.Id);
                    table.ForeignKey(
                        name: "FK_lobby_Location_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Location",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_lobby_LocationId",
                table: "lobby",
                column: "LocationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "lobby");

            migrationBuilder.DropColumn(
                name: "CurrentLobbyCount",
                table: "Location");

            migrationBuilder.DropColumn(
                name: "MaxLobbyCount",
                table: "Location");
        }
    }
}
