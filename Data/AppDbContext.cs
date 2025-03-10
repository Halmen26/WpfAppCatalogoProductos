using Microsoft.EntityFrameworkCore;
using WpfAppCatalogProduct.Models;


namespace WpfAppCatalogProduct.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public virtual DbSet<Categoria> Categorias { get; set; }
    public virtual DbSet<Product> Productos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder) =>
        modelBuilder.Entity<Product>(entity =>
        {
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.Property(e => e.ImageUri)
                .IsRequired(false);

            entity.HasOne(d => d.Categoria)
                .WithMany(p => p.Productos)
                .HasForeignKey(d => d.CategoriaId);
        });
}