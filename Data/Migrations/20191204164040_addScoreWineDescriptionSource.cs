using Microsoft.EntityFrameworkCore.Migrations;

namespace Wineapp.Data.Migrations
{
    public partial class addScoreWineDescriptionSource : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Score",
                table: "Wines",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Sources",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Score",
                table: "Wines");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Sources");
        }
    }
}
