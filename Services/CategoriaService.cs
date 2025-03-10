using Microsoft.EntityFrameworkCore;
using WpfAppCatalogProduct.Data;
using WpfAppCatalogProduct.Models;

namespace WpfAppCatalogProduct.Services;

public class CategoriaService : IRepository<Categoria>
{
    private readonly IDbContextFactory<AppDbContext> _contextFactory;

    public CategoriaService(IDbContextFactory<AppDbContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task<List<Categoria>> ObtenerAsync()
    {
        using var context = await _contextFactory.CreateDbContextAsync();
        return await context.Categorias.ToListAsync();
    }

    public async Task<Categoria> ObtenerPorIdAsync(int id)
    {
        using var context = await _contextFactory.CreateDbContextAsync();
        return await context.Categorias.FindAsync(id);
    }

    public async Task AgregarAsync(Categoria categoria)
    {
        using var context = await _contextFactory.CreateDbContextAsync();
        context.Categorias.Add(categoria);
        await context.SaveChangesAsync();
    }

    public async Task ActualizarAsync(Categoria categoria)
    {
        using var context = await _contextFactory.CreateDbContextAsync();
        context.Entry(categoria).State = EntityState.Modified;
        await context.SaveChangesAsync();
    }

    public async Task EliminarAsync(int id)
    {
        using var context = await _contextFactory.CreateDbContextAsync();
        var categoria = await context.Categorias.FindAsync(id);
        if (categoria != null)
        {
            context.Categorias.Remove(categoria);
            await context.SaveChangesAsync();
        }
    }
}
