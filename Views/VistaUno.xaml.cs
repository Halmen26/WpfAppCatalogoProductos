using System.Windows.Controls;
using WpfAppCatalogProduct.ViewModels;

namespace WpfAppCatalogProduct.Views;


public partial class VistaUno : UserControl
{
    public VistaUno()
    {
        InitializeComponent();
    }

    public VistaUno(VistaUnoViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}
