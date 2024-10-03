using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace GrudgeBookMvc.src.Model.Postgres.Migration;

public partial class DamazKronContext : DbContext
{
    public string ConnectionString { get; set; }

    public DamazKronContext()
    {
    }

    public DamazKronContext(DbContextOptions<DamazKronContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Grudge> Grudges { get; set; }

    /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("Host=localhost:5433;Username=postgres;Password=666Samotkitaksined;Database=DamazKron");*/

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension("pg_catalog", "adminpack");

        modelBuilder.Entity<UserData>(entity =>
        {
            entity.ToTable("grudges");

            entity.Property(e => e.Email).HasColumnName("Email");
            entity.Property(e => e.Id).HasColumnName("Id");
            entity.Property(e => e.UserName).HasColumnName("Email");
            entity.Property(e => e.Salt).HasColumnName("Salt");
            entity.Property(e => e.SaltedPassword).HasColumnName("SaltedPassword");
        });

        modelBuilder.Entity<Grudge>(entity =>
        {
            entity.ToTable("grudges");

            entity.Property(e => e.Id).HasColumnName("Id");
            entity.Property(e => e.TitleOfSin).HasColumnName("TitleOfSin");
            entity.Property(e => e.Timestamp).HasColumnName("Timestamp");
            entity.Property(e => e.Condition).HasColumnName("Condition");
            entity.Property(e => e.Details).HasColumnName("Details");
            entity.Property(e => e.Status).HasColumnName("Status");
            entity.Property(e => e.VisualizationUri).HasColumnName("VisualizationURI");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
