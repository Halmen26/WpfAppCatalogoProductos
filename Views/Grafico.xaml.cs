using System.Windows.Controls;
using WpfAppCatalogProduct.ViewModels;

namespace WpfAppCatalogProduct.Views
{
    public partial class Grafico : UserControl
    {
        public Grafico()
        {
            InitializeComponent();
            
        }

        public Grafico(GraficoViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
