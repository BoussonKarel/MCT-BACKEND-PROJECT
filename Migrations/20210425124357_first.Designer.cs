﻿// <auto-generated />
using System;
using MCT_BACKEND_PROJECT.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MCT_BACKEND_PROJECT.Migrations
{
    [DbContext(typeof(SpellenContext))]
    [Migration("20210425124357_first")]
    partial class first
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.5")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CategorieSpel", b =>
                {
                    b.Property<Guid>("CategorieenCategorieId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("SpellenSpelId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("CategorieenCategorieId", "SpellenSpelId");

                    b.HasIndex("SpellenSpelId");

                    b.ToTable("CategorieSpel");
                });

            modelBuilder.Entity("MCT_BACKEND_PROJECT.Models.Categorie", b =>
                {
                    b.Property<Guid>("CategorieId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Naam")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CategorieId");

                    b.ToTable("Categorieen");
                });

            modelBuilder.Entity("MCT_BACKEND_PROJECT.Models.Materiaal", b =>
                {
                    b.Property<Guid>("MateriaalId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Item")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MateriaalId");

                    b.ToTable("Materialen");
                });

            modelBuilder.Entity("MCT_BACKEND_PROJECT.Models.Spel", b =>
                {
                    b.Property<Guid>("SpelId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Duur")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Leeftijd_tot")
                        .HasColumnType("int");

                    b.Property<int>("Leeftijd_vanaf")
                        .HasColumnType("int");

                    b.Property<string>("Naam")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Spelers_max")
                        .HasColumnType("int");

                    b.Property<int>("Spelers_min")
                        .HasColumnType("int");

                    b.Property<string>("Terrein")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Uitleg")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SpelId");

                    b.ToTable("Spellen");
                });

            modelBuilder.Entity("MCT_BACKEND_PROJECT.Models.SpelMateriaal", b =>
                {
                    b.Property<Guid>("MateriaalId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("SpelId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Aantal")
                        .HasColumnType("int");

                    b.HasKey("MateriaalId", "SpelId");

                    b.HasIndex("SpelId");

                    b.ToTable("SpelMaterialen");
                });

            modelBuilder.Entity("CategorieSpel", b =>
                {
                    b.HasOne("MCT_BACKEND_PROJECT.Models.Categorie", null)
                        .WithMany()
                        .HasForeignKey("CategorieenCategorieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MCT_BACKEND_PROJECT.Models.Spel", null)
                        .WithMany()
                        .HasForeignKey("SpellenSpelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MCT_BACKEND_PROJECT.Models.SpelMateriaal", b =>
                {
                    b.HasOne("MCT_BACKEND_PROJECT.Models.Materiaal", null)
                        .WithMany("Spellen")
                        .HasForeignKey("MateriaalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MCT_BACKEND_PROJECT.Models.Spel", null)
                        .WithMany("SpelMateriaal")
                        .HasForeignKey("SpelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MCT_BACKEND_PROJECT.Models.Materiaal", b =>
                {
                    b.Navigation("Spellen");
                });

            modelBuilder.Entity("MCT_BACKEND_PROJECT.Models.Spel", b =>
                {
                    b.Navigation("SpelMateriaal");
                });
#pragma warning restore 612, 618
        }
    }
}
