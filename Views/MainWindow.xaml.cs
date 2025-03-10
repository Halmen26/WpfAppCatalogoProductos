using System.Windows;
using WpfAppCatalogProduct.ViewModels;

namespace WpfAppCatalogProduct.Views;

public partial class MainWindow : Window
{
    public MainWindow(MainViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }



    //private void ChangeLanguageEn_Click(object sender, RoutedEventArgs e)
    //{
    //    Properties.Settings.Default.Idioma = "en";
    //    Properties.Settings.Default.Save();
    //    MessageBox.Show("Idioma cambiado a inglés");
    //    RecargarInterfaz();
    //}
    //private void ChangeLanguageEs_Click(object sender, RoutedEventArgs e)
    //{
    //    Properties.Settings.Default.Idioma = "es";
    //    Properties.Settings.Default.Save();
    //    MessageBox.Show("Idioma cambiado a español");
    //    RecargarInterfaz();
    //}

    //private void RecargarInterfaz()
    //{
    //    Thread.CurrentThread.CurrentUICulture = new CultureInfo(Properties.Settings.Default.Idioma);
    //    Thread.CurrentThread.CurrentCulture = Thread.CurrentThread.CurrentUICulture;
    //    this.Close();
    //    App.Current.MainWindow = new MainWindow(App.Current.Services.GetService<MainViewModel>()); // 🔹 Usa DI correctamente
    //    App.Current.MainWindow.Show();
    //}
    //private void IdiomasButton_Click(object sender, RoutedEventArgs e)
    //{
    //    IdiomaContextMenu.IsOpen = true;
    //}
}