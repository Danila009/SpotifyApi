using Microsoft.EntityFrameworkCore.Migrations;

namespace SpotifyApi.Migrations
{
    public partial class FavorotePlaylists : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId1",
                table: "Playlists",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Playlists_UserId1",
                table: "Playlists",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Playlists_Users_UserId1",
                table: "Playlists",
                column: "UserId1",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Playlists_Users_UserId1",
                table: "Playlists");

            migrationBuilder.DropIndex(
                name: "IX_Playlists_UserId1",
                table: "Playlists");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Playlists");
        }
    }
}
