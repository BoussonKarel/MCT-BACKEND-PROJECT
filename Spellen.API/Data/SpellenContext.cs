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
    public interface IGameContext
    {
        DbSet<Game> Games { get; set; }
        DbSet<Item> Items { get; set; }
        DbSet<Category> Categories { get; set; }
        DbSet<VariCombi> VariCombis { get; set; }
        DbSet<GameCategory> GameCategories { get; set; }
        DbSet<GameItem> GameItems { get; set; }
        DbSet<GameVariCombi> GameVariCombis { get; set; }
        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }

    public class GameContext : DbContext, IGameContext
    {
        public DbSet<Game> Games { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Category> Categories { get; set; }
        // VARI COMBIS ARE LEFT OUT FOR NOW BECAUSE OF TIME
        // THEY CAN BE ADDED IN A NEXT UPDATE, A V2 OF THE API
        public DbSet<VariCombi> VariCombis { get; set; }
        public DbSet<GameCategory> GameCategories { get; set; }
        public DbSet<GameItem> GameItems { get; set; }
        // VARI COMBIS ARE LEFT OUT FOR NOW BECAUSE OF TIME
        // THEY CAN BE ADDED IN A NEXT UPDATE, A V2 OF THE API
        public DbSet<GameVariCombi> GameVariCombis { get; set; }

        public ConnectionStrings _connectionStrings;

        public GameContext(DbContextOptions<GameContext> options, IOptions<ConnectionStrings> connectionStrings)
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
            modelBuilder.Entity<GameCategory>()
            .HasKey(cs => new { cs.CategoryId, cs.GameId });

            modelBuilder.Entity<GameItem>()
            .HasKey(cs => new { cs.ItemId, cs.GameId });

            modelBuilder.Entity<GameVariCombi>()
            .HasKey(cs => new { cs.VariCombiId, cs.GameId });

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

            // ---------------
            // SEEDING
            // ---------------
            // CATEGORIEEN
            modelBuilder.Entity<Category>().HasData(
                new Category() {
                    CategoryId = new Guid("f03502ed-f8c6-45d4-a283-62cd9a36865a"),
                    Name = "Pleinspelen"
                },
                new Category() {
                    CategoryId = new Guid("2d261a66-3fa0-4c5b-91fb-7ff75999733b"),
                    Name = "Verstoppen"
                },
                new Category() {
                    CategoryId = new Guid("cc78fbbb-a46b-437e-8978-ae20d9243a29"),
                    Name = "Tikken / Tikkertje"
                }
            );

            // MATERIAAL
            modelBuilder.Entity<Item>().HasData(
                new Item() {
                    ItemId = new Guid("1f816b47-d874-4458-8acb-02812c8c4366"),
                    Name = "Potjes",
                },
                new Item() {
                    ItemId = new Guid("dacd3f2e-302b-4211-99a3-5de850821102"),
                    Name = "Kommel / Touw",
                }
            );
        }
    }
}
