using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Spellen.API.Migrations
{
    public partial class restartAgain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    GameId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Explanation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Duration = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Terrain = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AgeFrom = table.Column<int>(type: "int", nullable: false),
                    AgeTo = table.Column<int>(type: "int", nullable: false),
                    PlayersMin = table.Column<int>(type: "int", nullable: false),
                    PlayersMax = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.GameId);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    ItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.ItemId);
                });

            migrationBuilder.CreateTable(
                name: "VariCombis",
                columns: table => new
                {
                    VariCombiId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Explanation = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VariCombis", x => x.VariCombiId);
                });

            migrationBuilder.CreateTable(
                name: "GameCategories",
                columns: table => new
                {
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GameId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameCategories", x => new { x.CategoryId, x.GameId });
                    table.ForeignKey(
                        name: "FK_GameCategories_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameCategories_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "GameId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GameItems",
                columns: table => new
                {
                    ItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GameId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameItems", x => new { x.ItemId, x.GameId });
                    table.ForeignKey(
                        name: "FK_GameItems_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "GameId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameItems_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "ItemId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GameVariCombis",
                columns: table => new
                {
                    VariCombiId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GameId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameVariCombis", x => new { x.VariCombiId, x.GameId });
                    table.ForeignKey(
                        name: "FK_GameVariCombis_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "GameId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameVariCombis_VariCombis_VariCombiId",
                        column: x => x.VariCombiId,
                        principalTable: "VariCombis",
                        principalColumn: "VariCombiId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "Name" },
                values: new object[,]
                {
                    { new Guid("f03502ed-f8c6-45d4-a283-62cd9a36865a"), "Pleinspelen" },
                    { new Guid("2d261a66-3fa0-4c5b-91fb-7ff75999733b"), "Verstoppen" },
                    { new Guid("cc78fbbb-a46b-437e-8978-ae20d9243a29"), "Tikken / Tikkertje" }
                });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "ItemId", "Name" },
                values: new object[,]
                {
                    { new Guid("1f816b47-d874-4458-8acb-02812c8c4366"), "Potjes" },
                    { new Guid("dacd3f2e-302b-4211-99a3-5de850821102"), "Kommel / Touw" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_GameCategories_GameId",
                table: "GameCategories",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_GameItems_GameId",
                table: "GameItems",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_GameVariCombis_GameId",
                table: "GameVariCombis",
                column: "GameId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GameCategories");

            migrationBuilder.DropTable(
                name: "GameItems");

            migrationBuilder.DropTable(
                name: "GameVariCombis");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "Games");

            migrationBuilder.DropTable(
                name: "VariCombis");
        }
    }
}
