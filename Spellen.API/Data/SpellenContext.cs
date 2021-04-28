using System.Linq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Spellen.API.Configuration;
using Spellen.API.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;

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
            // ---------------
            // INSTELLINGEN
            // ---------------
            // SPEL.TERREIN
            // List<string> is niet toegelaten bij EF core,
            // dus slaan we het op als 1 string met een separator
            modelBuilder.Entity<Spel>()
            .Property(s => s.Terrein)
            .HasConversion(
                v => string.Join(',', v), // Convert TO (string)
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList(), // Convert FROM (string)
                new ValueComparer<List<string>>(
                    (c1, c2) => c1.SequenceEqual(c2), // Zijn de lists gelijk?
                    c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())), // Hash code genereren
                    c => c.ToList() // Expressie om snapshot te maken van een value
                )
            );

            // MATERIAALSPEL & CATEGORIESPEL
            // Samengestelde sleutels moeten in OnModelCreating worden gedefinieerd

            // modelBuilder.Entity<MateriaalSpel>()
            // .HasKey(ms => new { ms.MateriaalId, ms.SpelId });

            // modelBuilder.Entity<CategorieSpel>()
            // .HasKey(cs => new { cs.CategorieId, cs.SpelId });


            // VARICOMBI
            // Deze relatie is lastiger te leggen
            // modelBuilder.Entity<VariCombi>()
            // .HasMany(v => v.SpelId1)
            // .WithMany(s => s.VariCombis);
            // GEEFT ERRORS

            // ---------------
            // SEEDING
            // ---------------
            // CATEGORIEEN
            modelBuilder.Entity<Categorie>().HasData(
                new Categorie()
                {
                    CategorieId = Guid.NewGuid(),
                    Naam = "Pleinspelen"
                },
                new Categorie() {
                    CategorieId = Guid.NewGuid(),
                    Naam = "Verstoppen"
                }
            );

            // MATERIAAL
            modelBuilder.Entity<Materiaal>().HasData(
                new Materiaal() {
                    MateriaalId = Guid.NewGuid(),
                    Item = "Potjes",
                }
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
        }
    }
}
