using System.Collections.ObjectModel;
using WpfAppCatalogProduct.Models;
using WpfAppCatalogProduct.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows;

namespace WpfAppCatalogProduct.ViewModels;

public partial class VistaDosViewModel : ObservableObject
{
    private readonly IRepository<Product> _productoService;
    private readonly IRepository<Categoria> _categoriaService;

    [ObservableProperty]
    private ObservableCollection<Product> _productos = new();

    [ObservableProperty]
    private ObservableCollection<Categoria> _categorias = new();

    [ObservableProperty]
    private Product _productoSeleccionado = new();

    [ObservableProperty]
    private Categoria _categoriaSeleccionada = new();

    public VistaDosViewModel(IRepository<Product> productoService, IRepository<Categoria> categoriaService)
    {
        _productoService = productoService;
        _categoriaService = categoriaService;
        InitializeAsync();
    }

    private async void InitializeAsync()
    {
        try
        {
            await CargarDatosAsync();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error al cargar datos: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private async Task CargarDatosAsync()
    {
        try
        {
            var productos = await _productoService.ObtenerAsync();
            var categorias = await _categoriaService.ObtenerAsync();

            Application.Current.Dispatcher.Invoke(() =>
            {
                Productos = new ObservableCollection<Product>(productos);
                Categorias = new ObservableCollection<Categoria>(categorias);
            });

            System.Diagnostics.Debug.WriteLine($"Productos cargados: {Productos.Count}");
            System.Diagnostics.Debug.WriteLine($"Categorías cargadas: {Categorias.Count}");
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error al cargar datos: {ex.Message}");
            throw;
        }
    }

    [RelayCommand]
    private async Task CargarProductosAsync()
    {
        try
        {
            var lista = await _productoService.ObtenerAsync();
            Application.Current.Dispatcher.Invoke(() =>
            {
                Productos.Clear();
                foreach (var producto in lista)
                {
                    Productos.Add(producto);
                }
            });
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error al cargar productos: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    [RelayCommand]
    private async Task CargarCategoriasAsync()
    {
        try
        {
            var lista = await _categoriaService.ObtenerAsync();
            Application.Current.Dispatcher.Invoke(() =>
            {
                Categorias.Clear();
                foreach (var categoria in lista)
                {
                    Categorias.Add(categoria);
                }
            });
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error al cargar categorías: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    [RelayCommand]
    private async Task AgregarProductoAsync()
    {
        if (!string.IsNullOrWhiteSpace(ProductoSeleccionado?.Nombre) &&
            CategoriaSeleccionada != null)
        {
            try
            {
                var nuevoProducto = new Product
                {
                    Nombre = ProductoSeleccionado.Nombre,
                    Descripcion = ProductoSeleccionado.Descripcion,
                    Precio = ProductoSeleccionado.Precio,
                    ImageUri = ProductoSeleccionado.ImageUri ?? "https://picsum.photos/250/250",
                    CategoriaId = CategoriaSeleccionada.Id
                };

                await _productoService.AgregarAsync(nuevoProducto);
                await CargarProductosAsync();

                ProductoSeleccionado = new Product();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al agregar producto: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }

    [RelayCommand]
    private async Task PruebaCargaProductos()
    {
        await CargarProductosAsync();
        Console.WriteLine("Productos cargados manualmente.");
    }

    [RelayCommand]
    private async Task EliminarProductoAsync()
    {
        if (ProductoSeleccionado != null && ProductoSeleccionado.Id > 0)
        {
            try
            {
                await _productoService.EliminarAsync(ProductoSeleccionado.Id);
                await CargarProductosAsync();
                ProductoSeleccionado = new Product();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al eliminar producto: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
