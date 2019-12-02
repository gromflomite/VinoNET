using Microsoft.EntityFrameworkCore.Migrations;

namespace Wineapp.Data.Migrations
{
    public partial class addProvinceInSource : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Province",
                table: "Sources",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Province",
                table: "Sources");
        }
    }
}
