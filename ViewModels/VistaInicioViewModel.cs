using System.Collections.ObjectModel;
using WpfAppCatalogProduct.Models;
using WpfAppCatalogProduct.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace WpfAppCatalogProduct.ViewModels;

public partial class VistaInicioViewModel : ObservableObject
{
    private readonly IRepository<Product> _productoService;
    private readonly IRepository<Categoria> _categoriaService;

    [ObservableProperty]
    private ObservableCollection<Product> productos;

    [ObservableProperty]
    private ObservableCollection<Categoria> categorias;

    public VistaInicioViewModel(IRepository<Product> productoService, IRepository<Categoria> categoriaService)
    {
        _productoService = productoService;
        _categoriaService = categoriaService;
        productos = new ObservableCollection<Product>();
        categorias = new ObservableCollection<Categoria>();
        _ = CargarDatosAsync();
    }

    [RelayCommand]
    private async Task CargarDatosAsync()
    {
        productos.Clear();
        categorias.Clear();

        var listaProductos = await _productoService.ObtenerAsync();
        var listaCategorias = await _categoriaService.ObtenerAsync();

        foreach (var producto in listaProductos)
        {
            productos.Add(producto);
        }

        foreach (var categoria in listaCategorias)
        {
            categorias.Add(categoria);
        }
    }
}

