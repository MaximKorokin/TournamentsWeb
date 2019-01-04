using Microsoft.EntityFrameworkCore.Migrations;

namespace Tournaments.Migrations
{
    public partial class tournamentUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsCompleted",
                table: "Tournaments",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "MembersCapacity",
                table: "Tournaments",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCompleted",
                table: "Tournaments");

            migrationBuilder.DropColumn(
                name: "MembersCapacity",
                table: "Tournaments");
        }
    }
}
