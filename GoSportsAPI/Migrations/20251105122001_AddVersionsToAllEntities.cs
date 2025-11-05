using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoSportsAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddVersionsToAllEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Version",
                table: "sports",
                type: "rowversion",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "Version",
                table: "lobbies",
                type: "rowversion",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Version",
                table: "sports");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "lobbies");
        }
    }
}
