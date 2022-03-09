using Microsoft.EntityFrameworkCore.Migrations;

namespace SpotifyApi.Migrations
{
    public partial class HistorySearch : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "HistorySearches",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Title = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistorySearches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HistorySearches_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_HistorySearches_UserId",
                table: "HistorySearches",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HistorySearches");

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
    }
}
