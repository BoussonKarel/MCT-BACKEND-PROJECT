using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MCT_BACKEND_PROJECT.Migrations
{
    public partial class first : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categorieen",
                columns: table => new
                {
                    CategorieId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Naam = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorieen", x => x.CategorieId);
                });

            migrationBuilder.CreateTable(
                name: "Materialen",
                columns: table => new
                {
                    MateriaalId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Item = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Materialen", x => x.MateriaalId);
                });

            migrationBuilder.CreateTable(
                name: "Spellen",
                columns: table => new
                {
                    SpelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Naam = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Uitleg = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Duur = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Terrein = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Leeftijd_vanaf = table.Column<int>(type: "int", nullable: false),
                    Leeftijd_tot = table.Column<int>(type: "int", nullable: false),
                    Spelers_min = table.Column<int>(type: "int", nullable: false),
                    Spelers_max = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Spellen", x => x.SpelId);
                });

            migrationBuilder.CreateTable(
                name: "CategorieSpel",
                columns: table => new
                {
                    CategorieenCategorieId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SpellenSpelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategorieSpel", x => new { x.CategorieenCategorieId, x.SpellenSpelId });
                    table.ForeignKey(
                        name: "FK_CategorieSpel_Categorieen_CategorieenCategorieId",
                        column: x => x.CategorieenCategorieId,
                        principalTable: "Categorieen",
                        principalColumn: "CategorieId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategorieSpel_Spellen_SpellenSpelId",
                        column: x => x.SpellenSpelId,
                        principalTable: "Spellen",
                        principalColumn: "SpelId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SpelMaterialen",
                columns: table => new
                {
                    MateriaalId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SpelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Aantal = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpelMaterialen", x => new { x.MateriaalId, x.SpelId });
                    table.ForeignKey(
                        name: "FK_SpelMaterialen_Materialen_MateriaalId",
                        column: x => x.MateriaalId,
                        principalTable: "Materialen",
                        principalColumn: "MateriaalId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SpelMaterialen_Spellen_SpelId",
                        column: x => x.SpelId,
                        principalTable: "Spellen",
                        principalColumn: "SpelId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CategorieSpel_SpellenSpelId",
                table: "CategorieSpel",
                column: "SpellenSpelId");

            migrationBuilder.CreateIndex(
                name: "IX_SpelMaterialen_SpelId",
                table: "SpelMaterialen",
                column: "SpelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategorieSpel");

            migrationBuilder.DropTable(
                name: "SpelMaterialen");

            migrationBuilder.DropTable(
                name: "Categorieen");

            migrationBuilder.DropTable(
                name: "Materialen");

            migrationBuilder.DropTable(
                name: "Spellen");
        }
    }
}
