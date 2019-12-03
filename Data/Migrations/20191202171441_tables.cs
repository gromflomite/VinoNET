using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Wineapp.Data.Migrations
{
    public partial class tables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "AspNetUsers",
                maxLength: 60,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Company",
                table: "AspNetUsers",
                maxLength: 35,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "AspNetUsers",
                maxLength: 25,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Surname",
                table: "AspNetUsers",
                maxLength: 30,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Colours",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ColourType = table.Column<string>(maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Colours", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sources",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SourceType = table.Column<string>(maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sources", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sweetness",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SweetnesType = table.Column<string>(maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sweetness", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WineLists",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ListName = table.Column<string>(maxLength: 30, nullable: false),
                    Description = table.Column<string>(maxLength: 140, nullable: true),
                    ListDate = table.Column<DateTime>(nullable: true),
                    AppUserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WineLists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WineLists_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ColourTastes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Score = table.Column<int>(nullable: false),
                    AppUserId = table.Column<string>(nullable: true),
                    ColourId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ColourTastes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ColourTastes_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ColourTastes_Colours_ColourId",
                        column: x => x.ColourId,
                        principalTable: "Colours",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SourceTastes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Score = table.Column<int>(nullable: false),
                    AppUserId = table.Column<string>(nullable: true),
                    SourceId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SourceTastes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SourceTastes_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SourceTastes_Sources_SourceId",
                        column: x => x.SourceId,
                        principalTable: "Sources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SweetnessTastes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Score = table.Column<int>(nullable: false),
                    AppUserId = table.Column<string>(nullable: true),
                    SweetnesId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SweetnessTastes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SweetnessTastes_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SweetnessTastes_Sweetness_SweetnesId",
                        column: x => x.SweetnesId,
                        principalTable: "Sweetness",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Wines",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ColourId = table.Column<int>(nullable: true),
                    SweetnesId = table.Column<int>(nullable: true),
                    SourceId = table.Column<int>(nullable: true),
                    Name = table.Column<string>(maxLength: 30, nullable: false),
                    Price = table.Column<double>(nullable: true),
                    Company = table.Column<string>(maxLength: 30, nullable: false),
                    Sparkling = table.Column<bool>(nullable: false),
                    Year = table.Column<int>(maxLength: 4, nullable: false),
                    Publish = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Wines_Colours_ColourId",
                        column: x => x.ColourId,
                        principalTable: "Colours",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Wines_Sources_SourceId",
                        column: x => x.SourceId,
                        principalTable: "Sources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Wines_Sweetness_SweetnesId",
                        column: x => x.SweetnesId,
                        principalTable: "Sweetness",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserScores",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VoteValue = table.Column<int>(nullable: false),
                    VoteDate = table.Column<DateTime>(nullable: false),
                    AppUserId = table.Column<string>(nullable: true),
                    WineId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserScores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserScores_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserScores_Wines_WineId",
                        column: x => x.WineId,
                        principalTable: "Wines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WineListWines",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WineListId = table.Column<int>(nullable: false),
                    WineId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WineListWines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WineListWines_Wines_WineId",
                        column: x => x.WineId,
                        principalTable: "Wines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WineListWines_WineLists_WineListId",
                        column: x => x.WineListId,
                        principalTable: "WineLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ColourTastes_AppUserId",
                table: "ColourTastes",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ColourTastes_ColourId",
                table: "ColourTastes",
                column: "ColourId");

            migrationBuilder.CreateIndex(
                name: "IX_SourceTastes_AppUserId",
                table: "SourceTastes",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SourceTastes_SourceId",
                table: "SourceTastes",
                column: "SourceId");

            migrationBuilder.CreateIndex(
                name: "IX_SweetnessTastes_AppUserId",
                table: "SweetnessTastes",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SweetnessTastes_SweetnesId",
                table: "SweetnessTastes",
                column: "SweetnesId");

            migrationBuilder.CreateIndex(
                name: "IX_UserScores_AppUserId",
                table: "UserScores",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserScores_WineId",
                table: "UserScores",
                column: "WineId");

            migrationBuilder.CreateIndex(
                name: "IX_WineLists_AppUserId",
                table: "WineLists",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_WineListWines_WineId",
                table: "WineListWines",
                column: "WineId");

            migrationBuilder.CreateIndex(
                name: "IX_WineListWines_WineListId",
                table: "WineListWines",
                column: "WineListId");

            migrationBuilder.CreateIndex(
                name: "IX_Wines_ColourId",
                table: "Wines",
                column: "ColourId");

            migrationBuilder.CreateIndex(
                name: "IX_Wines_SourceId",
                table: "Wines",
                column: "SourceId");

            migrationBuilder.CreateIndex(
                name: "IX_Wines_SweetnesId",
                table: "Wines",
                column: "SweetnesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ColourTastes");

            migrationBuilder.DropTable(
                name: "SourceTastes");

            migrationBuilder.DropTable(
                name: "SweetnessTastes");

            migrationBuilder.DropTable(
                name: "UserScores");

            migrationBuilder.DropTable(
                name: "WineListWines");

            migrationBuilder.DropTable(
                name: "Wines");

            migrationBuilder.DropTable(
                name: "WineLists");

            migrationBuilder.DropTable(
                name: "Colours");

            migrationBuilder.DropTable(
                name: "Sources");

            migrationBuilder.DropTable(
                name: "Sweetness");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Company",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Surname",
                table: "AspNetUsers");
        }
    }
}
