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
            var reportsView = new ReportsView();
            reportsView.Owner = this;
            reportsView.ShowDialog();
            this.Close();
        }
        private void ViewDevices_Click(object sender, RoutedEventArgs e)
        {
            var devicesView = new DevicesView();
            devicesView.Owner = this;
            devicesView.ShowDialog();
            this.Close();
        }

        private void ViewLocations_Click(object sender, RoutedEventArgs e)
        {
            var locationsView = new LocationsView();
            locationsView.Owner = this;
            locationsView.ShowDialog();
            this.Close();
        }
    }
}