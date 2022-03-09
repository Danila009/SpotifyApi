using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SpotifyApi.Migrations
{
    public partial class MusicPalylistHasMany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Musics_Playlists_PlaylistId",
                table: "Musics");

            migrationBuilder.DropIndex(
                name: "IX_Musics_PlaylistId",
                table: "Musics");

            migrationBuilder.DropColumn(
                name: "AutorId",
                table: "Musics");

            migrationBuilder.DropColumn(
                name: "GenreId",
                table: "Musics");

            migrationBuilder.DropColumn(
                name: "PlaylistId",
                table: "Musics");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "HistorySearches",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "MusicPlaylist",
                columns: table => new
                {
                    MusicsId = table.Column<int>(type: "int", nullable: false),
                    PlaylistsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MusicPlaylist", x => new { x.MusicsId, x.PlaylistsId });
                    table.ForeignKey(
                        name: "FK_MusicPlaylist_Musics_MusicsId",
                        column: x => x.MusicsId,
                        principalTable: "Musics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MusicPlaylist_Playlists_PlaylistsId",
                        column: x => x.PlaylistsId,
                        principalTable: "Playlists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_MusicPlaylist_PlaylistsId",
                table: "MusicPlaylist",
                column: "PlaylistsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MusicPlaylist");

            migrationBuilder.AddColumn<int>(
                name: "AutorId",
                table: "Musics",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "GenreId",
                table: "Musics",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PlaylistId",
                table: "Musics",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "HistorySearches",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.CreateIndex(
                name: "IX_Musics_PlaylistId",
                table: "Musics",
                column: "PlaylistId");

            migrationBuilder.AddForeignKey(
                name: "FK_Musics_Playlists_PlaylistId",
                table: "Musics",
                column: "PlaylistId",
                principalTable: "Playlists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
