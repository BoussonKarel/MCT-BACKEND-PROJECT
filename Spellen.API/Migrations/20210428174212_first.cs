using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Spellen.API.Migrations
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
                name: "Materiaal",
                columns: table => new
                {
                    MateriaalId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Item = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Materiaal", x => x.MateriaalId);
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
                name: "MateriaalSpel",
                columns: table => new
                {
                    MateriaalId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SpellenSpelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MateriaalSpel", x => new { x.MateriaalId, x.SpellenSpelId });
                    table.ForeignKey(
                        name: "FK_MateriaalSpel_Materiaal_MateriaalId",
                        column: x => x.MateriaalId,
                        principalTable: "Materiaal",
                        principalColumn: "MateriaalId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MateriaalSpel_Spellen_SpellenSpelId",
                        column: x => x.SpellenSpelId,
                        principalTable: "Spellen",
                        principalColumn: "SpelId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categorieen",
                columns: new[] { "CategorieId", "Naam" },
                values: new object[,]
                {
                    { new Guid("92c8e6ca-d4c9-4d30-9566-dd1d0ec7f08f"), "Pleinspelen" },
                    { new Guid("761846ce-2682-4ca4-a166-06731d5e491f"), "Verstoppen" }
                });

            migrationBuilder.InsertData(
                table: "Materiaal",
                columns: new[] { "MateriaalId", "Item" },
                values: new object[] { new Guid("78d6af00-655e-48f2-ad2e-5ba2cb5534fd"), "Potjes" });

            migrationBuilder.InsertData(
                table: "Spellen",
                columns: new[] { "SpelId", "Duur", "Leeftijd_tot", "Leeftijd_vanaf", "Naam", "Spelers_max", "Spelers_min", "Terrein", "Uitleg" },
                values: new object[,]
                {
                    { new Guid("eb0c83ff-9cb0-424d-9cc3-579d2b40e466"), "15 tot 30 minuten", 99, 5, "Tussen 2 vuren", 99, 6, "Buiten,Grote zaal", "Tussen 2 vuren is een spel met twee teams en een bal en je gooit de andere eraan. En O ja, er is ook iets met een kapitein." },
                    { new Guid("4cc8e6a7-b6d6-42d3-8995-36cace9b29b0"), "5 tot 20 minuten", 99, 5, "Kiekeboe", 99, 3, "Buiten", "Kiekeboe is een verstopspel. Er is één zoeker die begint af te tellen vanaf 20. Binnen deze tijd moet iedereen zich verstopt hebben. De zoeker mag 3 stappen zetten en dan 'Kiekeboe' roepen, hij begint dan af te tellen vanaf 19, iedereen moet hem dan in die tijd aantikken en zich terug verstoppen... Wanneer je gevonden bent, mag je niet terug verstoppen." }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CategorieSpel_SpellenSpelId",
                table: "CategorieSpel",
                column: "SpellenSpelId");

            migrationBuilder.CreateIndex(
                name: "IX_MateriaalSpel_SpellenSpelId",
                table: "MateriaalSpel",
                column: "SpellenSpelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategorieSpel");

            migrationBuilder.DropTable(
                name: "MateriaalSpel");

            migrationBuilder.DropTable(
                name: "Categorieen");

            migrationBuilder.DropTable(
                name: "Materiaal");

            migrationBuilder.DropTable(
                name: "Spellen");
        }
    }
}
