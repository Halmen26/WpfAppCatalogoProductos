using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using WpfAppCatalogProduct.Data;
using WpfAppCatalogProduct.Models;

namespace WpfAppCatalogProduct.Services;

public class ProductoService : IRepository<Product>
{
    private readonly IDbContextFactory<AppDbContext> _contextFactory;

    public ProductoService(IDbContextFactory<AppDbContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task<List<Product>> ObtenerAsync()
    {
        try
        {
            using var context = await _contextFactory.CreateDbContextAsync();
            var productos = await context.Productos.ToListAsync();
            Debug.WriteLine($"Productos en BD: {productos.Count}");
            return productos;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error al obtener productos: {ex.Message}");
            throw;
        }
    }

    public async Task<List<Categoria>> ObtenerCategoriasAsync()
    {
        try
        {
            using var context = await _contextFactory.CreateDbContextAsync();
            return await context.Categorias.ToListAsync();
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error al obtener categorías: {ex.Message}");
            throw;
        }
    }

    public async Task<Product> ObtenerPorIdAsync(int id)
    {
        using var context = await _contextFactory.CreateDbContextAsync();
        return await context.Productos.FindAsync(id);
    }

    public async Task AgregarAsync(Product producto)
    {
        using var context = await _contextFactory.CreateDbContextAsync();
        context.Productos.Add(producto);
        await context.SaveChangesAsync();
    }

    public async Task ActualizarAsync(Product producto)
    {
        using var context = await _contextFactory.CreateDbContextAsync();
        context.Entry(producto).State = EntityState.Modified;
        await context.SaveChangesAsync();
    }

    public async Task EliminarAsync(int id)
    {
        using var context = await _contextFactory.CreateDbContextAsync();
        var producto = await context.Productos.FindAsync(id);
        if (producto != null)
        {
            context.Productos.Remove(producto);
            await context.SaveChangesAsync();
        }
    }
}
