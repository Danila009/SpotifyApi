using Microsoft.EntityFrameworkCore.Migrations;

namespace SpotifyApi.Migrations
{
    public partial class HasKeyMusicPast4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "AutorId",
                table: "Autors");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Autors",
                table: "Autors",
                column: "AutorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Autors",
                table: "Autors");

            migrationBuilder.AddPrimaryKey(
                name: "AutorId",
                table: "Autors",
                column: "AutorId");
        }
    }
}
