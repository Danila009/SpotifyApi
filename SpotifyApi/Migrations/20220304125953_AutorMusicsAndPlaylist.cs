using Microsoft.EntityFrameworkCore.Migrations;

namespace SpotifyApi.Migrations
{
    public partial class AutorMusicsAndPlaylist : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Autors_Musics_MusicId",
                table: "Autors");

            migrationBuilder.DropIndex(
                name: "IX_Autors_MusicId",
                table: "Autors");

            migrationBuilder.DropColumn(
                name: "MusicId",
                table: "Autors");

            migrationBuilder.AddColumn<int>(
                name: "AutorId",
                table: "Playlists",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AutorMusic",
                columns: table => new
                {
                    AutorsId = table.Column<int>(type: "int", nullable: false),
                    MusicsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AutorMusic", x => new { x.AutorsId, x.MusicsId });
                    table.ForeignKey(
                        name: "FK_AutorMusic_Autors_AutorsId",
                        column: x => x.AutorsId,
                        principalTable: "Autors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AutorMusic_Musics_MusicsId",
                        column: x => x.MusicsId,
                        principalTable: "Musics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Playlists_AutorId",
                table: "Playlists",
                column: "AutorId");

            migrationBuilder.CreateIndex(
                name: "IX_AutorMusic_MusicsId",
                table: "AutorMusic",
                column: "MusicsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Playlists_Autors_AutorId",
                table: "Playlists",
                column: "AutorId",
                principalTable: "Autors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Playlists_Autors_AutorId",
                table: "Playlists");

            migrationBuilder.DropTable(
                name: "AutorMusic");

            migrationBuilder.DropIndex(
                name: "IX_Playlists_AutorId",
                table: "Playlists");

            migrationBuilder.DropColumn(
                name: "AutorId",
                table: "Playlists");

            migrationBuilder.AddColumn<int>(
                name: "MusicId",
                table: "Autors",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Autors_MusicId",
                table: "Autors",
                column: "MusicId");

            migrationBuilder.AddForeignKey(
                name: "FK_Autors_Musics_MusicId",
                table: "Autors",
                column: "MusicId",
                principalTable: "Musics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
