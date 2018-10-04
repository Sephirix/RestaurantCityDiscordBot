using Microsoft.EntityFrameworkCore.Migrations;

namespace RestaurantCityDiscordBot.Migrations
{
    public partial class invite_link : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "inviteLink",
                table: "Trades",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "inviteLink",
                table: "Trades");
        }
    }
}
