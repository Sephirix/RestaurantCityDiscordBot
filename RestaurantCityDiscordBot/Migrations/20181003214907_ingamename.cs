using Microsoft.EntityFrameworkCore.Migrations;

namespace RestaurantCityDiscordBot.Migrations
{
    public partial class ingamename : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "inGameName",
                table: "Trades",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "inGameName",
                table: "Trades");
        }
    }
}
