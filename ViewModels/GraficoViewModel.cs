using CommunityToolkit.Mvvm.ComponentModel;
using WpfAppCatalogProduct.Services;
using System.Collections.ObjectModel;
using WpfAppCatalogProduct.Models;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;


namespace WpfAppCatalogProduct.ViewModels;

public partial class GraficoViewModel : ObservableObject
{
    private readonly IRepository<Product> _productService;
    private readonly IRepository<Categoria> _categoriaService;

    [ObservableProperty]
    private ObservableCollection<ISeries> _pieSeries = new();

    public GraficoViewModel(IRepository<Categoria> categoryService, IRepository<Product> productService)
    {
        _categoriaService = categoryService;
        _productService = productService;
        Task.Run(async () => await ConfigurePieSeriesAsync());

    }

    private async Task ConfigurePieSeriesAsync()
    {
        var products = await _productService.ObtenerAsync();
        if (products == null || !products.Any())
            return;

        PieSeries.Clear();
        PieSeries = new ObservableCollection<ISeries>(
            products.GroupBy(p => p.CategoriaId)
                    .Select(g => new PieSeries<int>
                    {
                        Values = new[] { g.Count() },
                        Name = g.Key.ToString()
                    })
        );
    }

}

