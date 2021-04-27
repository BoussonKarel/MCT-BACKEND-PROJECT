using System.Threading;
using System;
using Microsoft.EntityFrameworkCore;
using MCT_BACKEND_PROJECT.Configuration;
using Microsoft.Extensions.Options;
using MCT_BACKEND_PROJECT.Models;
using System.Threading.Tasks;

namespace MCT_BACKEND_PROJECT.Data
{
    public interface ISpellenContext
    {
        DbSet<Spel> Spellen { get; set; }
        DbSet<SpelMateriaal> SpelMaterialen { get; set; }
        DbSet<Materiaal> Materialen { get; set; }
        DbSet<Categorie> Categorieen { get; set; }
        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }

    public class SpellenContext : DbContext, ISpellenContext
    {
        public DbSet<Spel> Spellen { get; set; }
        public DbSet<SpelMateriaal> SpelMaterialen { get; set; }
        public DbSet<Materiaal> Materialen { get; set; }
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
            // Samengestelde primaire sleutel (composite key) voor SpelMateriaal
            modelBuilder.Entity<SpelMateriaal>()
            .HasKey(sm => new { sm.MateriaalId, sm.SpelId });

            // Relatie van VariCombis proberen leggen
            // modelBuilder.Entity<VariCombi>()
            // .HasMany(v => v.SpelId1)
            // .WithMany(s => s.VariCombis);
            // GEEFT ERRORS

            // Dummy data
        }
    }
}
