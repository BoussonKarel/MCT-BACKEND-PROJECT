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
        DbSet<Game> Spellen { get; set; }
        DbSet<Item> Materiaal { get; set; }
        DbSet<Category> Categorieen { get; set; }
        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }

    public class SpellenContext : DbContext, ISpellenContext
    {
        public DbSet<Game> Spellen { get; set; }
        public DbSet<Item> Materiaal { get; set; }
        public DbSet<Category> Categorieen { get; set; }
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
            modelBuilder.Entity<Game>()
            .Property(s => s.Terrain)
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
            modelBuilder.Entity<Category>().HasData(
                new Category()
                {
                    CategoryId = Guid.NewGuid(),
                    Name = "Pleinspelen"
                },
                new Category() {
                    CategoryId = Guid.NewGuid(),
                    Name = "Verstoppen"
                }
            );

            // MATERIAAL
            modelBuilder.Entity<Item>().HasData(
                new Item() {
                    ItemId = Guid.NewGuid(),
                    Name = "Potjes",
                }
            );

            // SPELLEN
            modelBuilder.Entity<Game>().HasData(
                new Game(){
                    GameId = Guid.NewGuid(),
                    Name = "Tussen 2 vuren",
                    Explanation = "Tussen 2 vuren is een spel met twee teams en een bal en je gooit de andere eraan. En O ja, er is ook iets met een kapitein.",
                    Duration = "15 tot 30 minuten",
                    Terrain = new List<string>() {"Buiten", "Grote zaal"},
                    AgeFrom = 5,
                    AgeTo = 99,
                    PlayersMin = 6,
                    PlayersMax = 99,
                },
                new Game() {
                    GameId = Guid.NewGuid(),
                    Name = "Kiekeboe",
                    Explanation = "Kiekeboe is een verstopspel. Er is één zoeker die begint af te tellen vanaf 20. Binnen deze tijd moet iedereen zich verstopt hebben. De zoeker mag 3 stappen zetten en dan 'Kiekeboe' roepen, hij begint dan af te tellen vanaf 19, iedereen moet hem dan in die tijd aantikken en zich terug verstoppen... Wanneer je gevonden bent, mag je niet terug verstoppen.",
                    Duration = "5 tot 20 minuten",
                    Terrain = new List<string>() {"Buiten"},
                    AgeFrom = 5,
                    AgeTo = 99,
                    PlayersMin = 3,
                    PlayersMax = 99,
                }
            );
        }
    }
}
