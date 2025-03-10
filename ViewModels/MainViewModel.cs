using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace WpfAppCatalogProduct.ViewModels;

public partial class MainViewModel : ObservableObject
{
    [ObservableProperty]
    private object _activeView;

    [ObservableProperty] private bool _isInicioSelected = true;
    [ObservableProperty] private bool _isVistaUnoSelected;
    [ObservableProperty] private bool _isVistaDosSelected;
    [ObservableProperty] private bool _isGraficoSelected;

    private readonly VistaUnoViewModel _vistaUnoViewModel;
    private readonly VistaDosViewModel _vistaDosViewModel;
    private readonly VistaInicioViewModel _vistaInicioViewModel;
    private readonly GraficoViewModel _graficoViewModel;

    public MainViewModel(
        VistaUnoViewModel vistaUnoViewModel,
        VistaDosViewModel vistaDosViewModel,
        VistaInicioViewModel vistaInicioViewModel,
        GraficoViewModel graficoViewModel)
    {
        _vistaUnoViewModel = vistaUnoViewModel;
        _vistaDosViewModel = vistaDosViewModel;
        _vistaInicioViewModel = vistaInicioViewModel;
        _graficoViewModel = graficoViewModel;

        ActiveView = _vistaInicioViewModel;
    }
   


    [RelayCommand]
    private void ActivateVistaInicio()
    {
        ActiveView = _vistaInicioViewModel;
        IsInicioSelected = true;
        IsVistaUnoSelected = false;
        IsVistaDosSelected = false;
        IsGraficoSelected = false;
    }

    [RelayCommand]
    private void ActivateVistaUno()
    {
        ActiveView = _vistaUnoViewModel;
        IsInicioSelected = false;
        IsVistaUnoSelected = true;
        IsVistaDosSelected = false;
        IsGraficoSelected = false;
    }

    [RelayCommand]
    private void ActivateVistaDos()
    {
        try
        {
            ActiveView = _vistaDosViewModel;
            IsInicioSelected = false;
            IsVistaUnoSelected = false;
            IsVistaDosSelected = true;
            IsGraficoSelected = false;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error al activar vista de productos: {ex.Message}");
            throw;
        }
    }

    [RelayCommand]
    private void ActivateGrafico()
    {
        ActiveView = _graficoViewModel;
        IsInicioSelected = false;
        IsVistaUnoSelected = false;
        IsVistaDosSelected = false;
        IsGraficoSelected = true;
    }
}
