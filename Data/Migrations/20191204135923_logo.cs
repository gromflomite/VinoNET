using Microsoft.EntityFrameworkCore.Migrations;

namespace Wineapp.Data.Migrations
{
    public partial class logo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_AspNetUserTokens_IdentityUser_UserId",
            //    table: "AspNetUserTokens");

            //migrationBuilder.DropTable(
            //    name: "IdentityUser");

            //migrationBuilder.AddColumn<string>(
            //    name: "Image",
            //    table: "Wines",
            //    nullable: true);

            //migrationBuilder.AddColumn<string>(
            //    name: "Image",
            //    table: "Sources",
            //    nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Logo",
                table: "Sources",
                nullable: true);

            //migrationBuilder.AddColumn<string>(
            //    name: "Image",
            //    table: "AspNetUsers",
            //    nullable: true);

            //migrationBuilder.AddColumn<bool>(
            //    name: "Survey",
            //    table: "AspNetUsers",
            //    nullable: true);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_AspNetUserTokens_AspNetUsers_UserId",
            //    table: "AspNetUserTokens",
            //    column: "UserId",
            //    principalTable: "AspNetUsers",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_AspNetUserTokens_AspNetUsers_UserId",
            //    table: "AspNetUserTokens");

            //migrationBuilder.DropColumn(
            //    name: "Image",
            //    table: "Wines");

            //migrationBuilder.DropColumn(
            //    name: "Image",
            //    table: "Sources");

            migrationBuilder.DropColumn(
                name: "Logo",
                table: "Sources");

            //migrationBuilder.DropColumn(
            //    name: "Image",
            //    table: "AspNetUsers");

            //migrationBuilder.DropColumn(
            //    name: "Survey",
            //    table: "AspNetUsers");

            //migrationBuilder.CreateTable(
            //    name: "IdentityUser",
            //    columns: table => new
            //    {
            //        TempId1 = table.Column<string>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.UniqueConstraint("AK_IdentityUser_TempId1", x => x.TempId1);
            //    });

            //migrationBuilder.AddForeignKey(
            //    name: "FK_AspNetUserTokens_IdentityUser_UserId",
            //    table: "AspNetUserTokens",
            //    column: "UserId",
            //    principalTable: "IdentityUser",
            //    principalColumn: "TempId1",
            //    onDelete: ReferentialAction.Cascade);
        }
    }
}
