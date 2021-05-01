using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Spellen.API.Migrations
{
    public partial class first : Migration
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
                name: "CategoryGame",
                columns: table => new
                {
                    CategoriesCategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GamesGameId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryGame", x => new { x.CategoriesCategoryId, x.GamesGameId });
                    table.ForeignKey(
                        name: "FK_CategoryGame_Categories_CategoriesCategoryId",
                        column: x => x.CategoriesCategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryGame_Games_GamesGameId",
                        column: x => x.GamesGameId,
                        principalTable: "Games",
                        principalColumn: "GameId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GameItem",
                columns: table => new
                {
                    GamesGameId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ItemsItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameItem", x => new { x.GamesGameId, x.ItemsItemId });
                    table.ForeignKey(
                        name: "FK_GameItem_Games_GamesGameId",
                        column: x => x.GamesGameId,
                        principalTable: "Games",
                        principalColumn: "GameId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameItem_Items_ItemsItemId",
                        column: x => x.ItemsItemId,
                        principalTable: "Items",
                        principalColumn: "ItemId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GameVariCombi",
                columns: table => new
                {
                    GamesGameId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VariCombisVariCombiId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameVariCombi", x => new { x.GamesGameId, x.VariCombisVariCombiId });
                    table.ForeignKey(
                        name: "FK_GameVariCombi_Games_GamesGameId",
                        column: x => x.GamesGameId,
                        principalTable: "Games",
                        principalColumn: "GameId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameVariCombi_VariCombis_VariCombisVariCombiId",
                        column: x => x.VariCombisVariCombiId,
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
                name: "IX_CategoryGame_GamesGameId",
                table: "CategoryGame",
                column: "GamesGameId");

            migrationBuilder.CreateIndex(
                name: "IX_GameItem_ItemsItemId",
                table: "GameItem",
                column: "ItemsItemId");

            migrationBuilder.CreateIndex(
                name: "IX_GameVariCombi_VariCombisVariCombiId",
                table: "GameVariCombi",
                column: "VariCombisVariCombiId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryGame");

            migrationBuilder.DropTable(
                name: "GameItem");

            migrationBuilder.DropTable(
                name: "GameVariCombi");

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
