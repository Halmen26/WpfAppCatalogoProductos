using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using WpfAppCatalogProduct.Data;
using WpfAppCatalogProduct.Models;
using WpfAppCatalogProduct.Services;
using WpfAppCatalogProduct.ViewModels;
using WpfAppCatalogProduct.Views;

namespace WpfAppCatalogProduct;

public partial class App : Application
{
    protected override async void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        ServiceCollection services = new();

        services.AddDbContextFactory<AppDbContext>(options =>
           options.UseSqlServer("Server=localhost,1433;Database=TuBaseDeDatos;User Id=sa;Password=Interfaces-2425;TrustServerCertificate=true;"));

        services.AddScoped<IRepository<Product>, ProductoService>();
        services.AddScoped<IRepository<Categoria>, CategoriaService>();

        services.AddTransient<MainViewModel>();
        services.AddTransient<VistaUnoViewModel>();
        services.AddTransient<VistaInicioViewModel>();
        services.AddTransient<VistaDosViewModel>();
        services.AddTransient<GraficoViewModel>();
        services.AddTransient<MainWindow>();

        var serviceProvider = services.BuildServiceProvider();

        using (var scope = serviceProvider.CreateScope())
        {
            var scopedProvider = scope.ServiceProvider;
            var dbContext = scopedProvider.GetRequiredService<AppDbContext>();
            dbContext.Database.EnsureCreated();

            if (!dbContext.Categorias.Any())
            {
                dbContext.Categorias.AddRange(
                    new Categoria { Nombre = "Cosas" },
                    new Categoria { Nombre = "Más cosas" }
                );

                dbContext.Productos.AddRange(
                    new Product { Nombre = "Cosa 1", Descripcion = "Descripción de la cosa 1", CategoriaId = 1, ImageUri = "https://picsum.photos/250/250" },
                    new Product { Nombre = "Cosa 2", Descripcion = "Descripción de la cosa 2", CategoriaId = 2, ImageUri = "https://picsum.photos/250/250" },
                    new Product { Nombre = "Cosa 3", Descripcion = "Descripción de la cosa 3", CategoriaId = 1, ImageUri = "https://picsum.photos/250/250" },
                    new Product { Nombre = "Cosa 4", Descripcion = "Descripción de la cosa 4", CategoriaId = 1, ImageUri = "https://picsum.photos/250/250" },
                    new Product { Nombre = "Cosa 5", Descripcion = "Descripción de la cosa 5", CategoriaId = 2, ImageUri = "https://picsum.photos/250/250" }
                );
                dbContext.SaveChanges();
            }
        }

        using (var scope = serviceProvider.CreateScope())
        {
            var scopedProvider = scope.ServiceProvider;

            try
            {
                var mainViewModel = scopedProvider.GetRequiredService<MainViewModel>();
                var mainWindow = scopedProvider.GetRequiredService<MainWindow>();
                mainWindow.DataContext = mainViewModel;

                mainWindow.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al inicializar la aplicación: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                throw;
            }
        }
    }
}



