
using Epik.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Epik.Api.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options
    ) : base(options)
    {
    }

    // Tabla Persona
    public DbSet<Persona> Personas => Set<Persona>();

    // Vista de mujeres
    public DbSet<MujerView> Mujeres => Set<MujerView>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configuración de la tabla Persona
        modelBuilder.Entity<Persona>(entity =>
        {
            entity.ToTable("Persona");

            entity.HasKey(p => p.Identificacion);

            entity.Property(p => p.Identificacion)
                .HasMaxLength(20)
                .IsRequired();

            entity.Property(p => p.Nombres)
                .HasMaxLength(100)
                .IsRequired();

            entity.Property(p => p.Apellidos)
                .HasMaxLength(100)
                .IsRequired();

            entity.Property(p => p.Edad)
                .IsRequired();

            entity.Property(p => p.Genero)
                .HasMaxLength(10)
                .IsRequired();
        });

        // Configuración de la vista vw_mujeres
        modelBuilder.Entity<MujerView>(entity =>
        {
            entity.HasNoKey();

            entity.ToView("vw_mujeres", "public");

            entity.Property(p => p.Identificacion)
                .HasColumnName("Identificacion");

            entity.Property(p => p.Nombres)
                .HasColumnName("Nombres");

            entity.Property(p => p.Apellidos)
                .HasColumnName("Apellidos");

            entity.Property(p => p.Edad)
                .HasColumnName("Edad");

            entity.Property(p => p.Genero)
                .HasColumnName("Genero");
        });
    }
}