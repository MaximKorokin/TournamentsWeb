using Microsoft.EntityFrameworkCore.Migrations;

namespace Tournaments.Migrations
{
    public partial class tourGameWinner : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TournamentsGamesPlayers_Users_UserId",
                table: "TournamentsGamesPlayers");

            migrationBuilder.DropIndex(
                name: "IX_TournamentsGamesPlayers_UserId",
                table: "TournamentsGamesPlayers");

            migrationBuilder.AddColumn<int>(
                name: "UserId1",
                table: "TournamentsGamesPlayers",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserTournamentsGameId",
                table: "TournamentsGamesPlayers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TournamentsGamesPlayers_UserTournamentsGameId_UserId1",
                table: "TournamentsGamesPlayers",
                columns: new[] { "UserTournamentsGameId", "UserId1" });

            migrationBuilder.AddForeignKey(
                name: "FK_TournamentsGamesPlayers_TournamentsGamesPlayers_UserTournamentsGameId_UserId1",
                table: "TournamentsGamesPlayers",
                columns: new[] { "UserTournamentsGameId", "UserId1" },
                principalTable: "TournamentsGamesPlayers",
                principalColumns: new[] { "TournamentsGameId", "UserId" },
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TournamentsGamesPlayers_TournamentsGamesPlayers_UserTournamentsGameId_UserId1",
                table: "TournamentsGamesPlayers");

            migrationBuilder.DropIndex(
                name: "IX_TournamentsGamesPlayers_UserTournamentsGameId_UserId1",
                table: "TournamentsGamesPlayers");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "TournamentsGamesPlayers");

            migrationBuilder.DropColumn(
                name: "UserTournamentsGameId",
                table: "TournamentsGamesPlayers");

            migrationBuilder.CreateIndex(
                name: "IX_TournamentsGamesPlayers_UserId",
                table: "TournamentsGamesPlayers",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_TournamentsGamesPlayers_Users_UserId",
                table: "TournamentsGamesPlayers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
