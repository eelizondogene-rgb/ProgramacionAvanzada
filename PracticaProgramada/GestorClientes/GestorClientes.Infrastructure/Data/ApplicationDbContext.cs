
using Microsoft.EntityFrameworkCore;
using GestorClientes.Domain;

namespace GestorClientes.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Cliente> Clientes => Set<Cliente>();

    public DbSet<Telefono> Telefonos => Set<Telefono>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.ToTable("Clientes");

            entity.Property(c => c.Cedula).IsRequired().HasMaxLength(20);
            entity.Property(c => c.Nombre).IsRequired().HasMaxLength(100);
            entity.Property(c => c.Apellidos).IsRequired().HasMaxLength(150);

            entity.HasIndex(c => c.Cedula).IsUnique();

            entity.HasMany(c => c.Telefonos)
                .WithOne(t => t.Cliente)
                .HasForeignKey(t => t.ClienteId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Telefono>(entity =>
        {
            entity.ToTable("Telefonos");

            entity.Property(t => t.Numero).IsRequired().HasMaxLength(20);
        });
    }
}

