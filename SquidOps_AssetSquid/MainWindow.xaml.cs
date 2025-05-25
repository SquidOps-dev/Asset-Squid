using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SquidOps_AssetSquid.Views;
using SquidOps_AssetSquid.Extensions;

namespace SquidOps_AssetSquid
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void ViewReports_Click(object sender, RoutedEventArgs e)
        {
            this.NavigateTo(new ReportsView());
            this.Close();
        }
        private void ViewDevices_Click(object sender, RoutedEventArgs e)
        {
            this.NavigateTo(new DevicesView());
            this.Close();
        }

        private void ViewLocations_Click(object sender, RoutedEventArgs e)
        {
            this.NavigateTo(new LocationsView());
            this.Close();
        }
    }
}