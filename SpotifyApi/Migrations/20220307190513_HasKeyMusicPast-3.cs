using Microsoft.EntityFrameworkCore.Migrations;

namespace SpotifyApi.Migrations
{
    public partial class HasKeyMusicPast3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PlaylistId",
                table: "Playlists");

            migrationBuilder.DropPrimaryKey(
                name: "GenreId",
                table: "Genres");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Playlists",
                table: "Playlists",
                column: "PlaylistId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Genres",
                table: "Genres",
                column: "GenreId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Playlists",
                table: "Playlists");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Genres",
                table: "Genres");

            migrationBuilder.AddPrimaryKey(
                name: "PlaylistId",
                table: "Playlists",
                column: "PlaylistId");

            migrationBuilder.AddPrimaryKey(
                name: "GenreId",
                table: "Genres",
                column: "GenreId");
        }
    }
}
