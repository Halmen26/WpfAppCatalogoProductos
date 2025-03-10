using System.Collections.ObjectModel;
using WpfAppCatalogProduct.Models;
using WpfAppCatalogProduct.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace WpfAppCatalogProduct.ViewModels;

public partial class VistaUnoViewModel : ObservableObject
{
    private readonly CategoriaService _categoriaService;

    [ObservableProperty]
    private ObservableCollection<Categoria> _categorias;

    [ObservableProperty]
    private Categoria _categoriaSeleccionada;

    public VistaUnoViewModel(IRepository<Categoria> categoriaService)
    {
        _categoriaService = (CategoriaService)categoriaService;
        Categorias = new ObservableCollection<Categoria>();
        CategoriaSeleccionada = new Categoria();
        _ = CargarCategoriasAsync();
    }

    [RelayCommand]
    private async Task CargarCategoriasAsync()
    {
        Categorias.Clear();
        var lista = await _categoriaService.ObtenerAsync();
        foreach (var categoria in lista)
        {
            Categorias.Add(categoria);
        }
    }

    [RelayCommand]
    private async Task AgregarCategoriaAsync()
    {
        if (!string.IsNullOrWhiteSpace(CategoriaSeleccionada?.Nombre))
        {
            var nuevaCategoria = new Categoria { Nombre = CategoriaSeleccionada.Nombre };
            await _categoriaService.AgregarAsync(nuevaCategoria);
            await CargarCategoriasAsync();
            CategoriaSeleccionada = new Categoria();
            Console.WriteLine("Categoría agregada correctamente.");
        }
    }



    [RelayCommand]
    private async Task ActualizarCategoriaAsync()
    {
        if (CategoriaSeleccionada != null && CategoriaSeleccionada.Id > 0)
        {
            await _categoriaService.ActualizarAsync(CategoriaSeleccionada);
            await CargarCategoriasAsync();
            CategoriaSeleccionada = new Categoria();
            Console.WriteLine("Categoría actualizada correctamente.");
        }
    }


    [RelayCommand]
    private async Task EliminarCategoriaAsync()
    {
        if (CategoriaSeleccionada != null && CategoriaSeleccionada.Id > 0)
        {
            await _categoriaService.EliminarAsync(CategoriaSeleccionada.Id);
            Categorias.Remove(CategoriaSeleccionada);
            CategoriaSeleccionada = new Categoria();
            Console.WriteLine("Categoría eliminada correctamente.");
        }
    }

}
