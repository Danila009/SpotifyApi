using Microsoft.EntityFrameworkCore.Migrations;

namespace SpotifyApi.Migrations
{
    public partial class Genre : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Musics_Genres_GenreId",
                table: "Musics");

            migrationBuilder.DropIndex(
                name: "IX_Musics_GenreId",
                table: "Musics");

            migrationBuilder.DropColumn(
                name: "GenreId",
                table: "Musics");

            migrationBuilder.AddColumn<string>(
                name: "WebIcon",
                table: "Musics",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "GenreMusic",
                columns: table => new
                {
                    GenreId = table.Column<int>(type: "int", nullable: false),
                    MusicsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GenreMusic", x => new { x.GenreId, x.MusicsId });
                    table.ForeignKey(
                        name: "FK_GenreMusic_Genres_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GenreMusic_Musics_MusicsId",
                        column: x => x.MusicsId,
                        principalTable: "Musics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_GenreMusic_MusicsId",
                table: "GenreMusic",
                column: "MusicsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GenreMusic");

            migrationBuilder.DropColumn(
                name: "WebIcon",
                table: "Musics");

            migrationBuilder.AddColumn<int>(
                name: "GenreId",
                table: "Musics",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Musics_GenreId",
                table: "Musics",
                column: "GenreId");

            migrationBuilder.AddForeignKey(
                name: "FK_Musics_Genres_GenreId",
                table: "Musics",
                column: "GenreId",
                principalTable: "Genres",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
