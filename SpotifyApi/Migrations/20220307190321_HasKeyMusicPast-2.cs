using Microsoft.EntityFrameworkCore.Migrations;

namespace SpotifyApi.Migrations
{
    public partial class HasKeyMusicPast2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AutorMusic_Autors_AutorsId",
                table: "AutorMusic");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Playlists",
                newName: "PlaylistId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Genres",
                newName: "GenreId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Autors",
                newName: "AutorId");

            migrationBuilder.RenameColumn(
                name: "AutorsId",
                table: "AutorMusic",
                newName: "AutorsAutorId");

            migrationBuilder.AddForeignKey(
                name: "FK_AutorMusic_Autors_AutorsAutorId",
                table: "AutorMusic",
                column: "AutorsAutorId",
                principalTable: "Autors",
                principalColumn: "AutorId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AutorMusic_Autors_AutorsAutorId",
                table: "AutorMusic");

            migrationBuilder.RenameColumn(
                name: "PlaylistId",
                table: "Playlists",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "GenreId",
                table: "Genres",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "AutorId",
                table: "Autors",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "AutorsAutorId",
                table: "AutorMusic",
                newName: "AutorsId");

            migrationBuilder.AddForeignKey(
                name: "FK_AutorMusic_Autors_AutorsId",
                table: "AutorMusic",
                column: "AutorsId",
                principalTable: "Autors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
