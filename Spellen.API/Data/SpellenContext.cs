using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Spellen.API.Configuration;
using Spellen.API.Models;

namespace Spellen.API.Data
{
    public interface ISpellenContext
    {
        DbSet<Spel> Spellen { get; set; }
        DbSet<Materiaal> Materiaal { get; set; }
        DbSet<Categorie> Categorieen { get; set; }
        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }

    public class SpellenContext : DbContext, ISpellenContext
    {
        public DbSet<Spel> Spellen { get; set; }
        public DbSet<Materiaal> Materiaal { get; set; }
        public DbSet<Categorie> Categorieen { get; set; }
        // public DbSet<VariCombi> VariCombis { get; set; }

        public ConnectionStrings _connectionStrings;

        public SpellenContext(DbContextOptions<SpellenContext> options, IOptions<ConnectionStrings> connectionStrings)
            : base(options)
        {
            _connectionStrings = connectionStrings.Value;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(_connectionStrings.SQL);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // modelBuilder.Entity<MateriaalSpel>()
            // .HasKey(ms => new { ms.MateriaalId, ms.SpelId });

            // modelBuilder.Entity<CategorieSpel>()
            // .HasKey(cs => new { cs.CategorieId, cs.SpelId });


            // Relatie van VariCombis proberen leggen
            // modelBuilder.Entity<VariCombi>()
            // .HasMany(v => v.SpelId1)
            // .WithMany(s => s.VariCombis);
            // GEEFT ERRORS

            // --- SEEDING ---

            // CATEGORIEEN
            Categorie catPleinspelen = new Categorie()
            {
                CategorieId = Guid.NewGuid(),
                Naam = "Pleinspelen"
            };
            Categorie catVerstoppen = new Categorie() {
                CategorieId = Guid.NewGuid(),
                Naam = "Verstoppen"
            };
            modelBuilder.Entity<Categorie>().HasData(
                catPleinspelen,
                catVerstoppen
            );

            // MATERIAAL
            Materiaal matPotje = new Materiaal()
            {
                MateriaalId = Guid.NewGuid(),
                Item = "Potjes",
            };
            modelBuilder.Entity<Materiaal>().HasData(
                matPotje
            );

            // SPELLEN
            modelBuilder.Entity<Spel>().HasData(
                new Spel(){
                    SpelId = Guid.NewGuid(),
                    Naam = "Tussen 2 vuren",
                    Uitleg = "Tussen 2 vuren is een spel met twee teams en een bal en je gooit de andere eraan. En O ja, er is ook iets met een kapitein.",
                    Duur = "15 tot 30 minuten",
                    Terrein = new List<string>() {"Buiten", "Grote zaal"},
                    Leeftijd_vanaf = 5,
                    Leeftijd_tot = 99,
                    Spelers_min = 6,
                    Spelers_max = 99,
                },
                new Spel() {
                    SpelId = Guid.NewGuid(),
                    Naam = "Kiekeboe",
                    Uitleg = "Kiekeboe is een verstopspel. Er is één zoeker die begint af te tellen vanaf 20. Binnen deze tijd moet iedereen zich verstopt hebben. De zoeker mag 3 stappen zetten en dan 'Kiekeboe' roepen, hij begint dan af te tellen vanaf 19, iedereen moet hem dan in die tijd aantikken en zich terug verstoppen... Wanneer je gevonden bent, mag je niet terug verstoppen.",
                    Duur = "5 tot 20 minuten",
                    Terrein = new List<string>() {"Buiten"},
                    Leeftijd_vanaf = 5,
                    Leeftijd_tot = 99,
                    Spelers_min = 3,
                    Spelers_max = 99,
                }
            );

            // MATERIAALSPEL
            // modelBuilder.Entity<MateriaalSpel>().HasData(
            //     new MateriaalSpel() {
            //         MateriaalId = matPotje.MateriaalId,
            //         SpelId = spTussenTweeVuren.SpelId
            //     }
            // );

            // CATEGORIESPEL
            // modelBuilder.Entity<CategorieSpel>().HasData(
            //     new CategorieSpel() {
            //         CategorieId = catPleinspelen.CategorieId,
            //         SpelId = spTussenTweeVuren.SpelId
            //     }
            // );
        }
    }
}
