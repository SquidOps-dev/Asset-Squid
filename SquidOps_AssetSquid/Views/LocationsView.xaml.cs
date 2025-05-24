using System.Windows;
using SquidOps_AssetSquid.DAL;
using SquidOps_AssetSquid.Models;

namespace SquidOps_AssetSquid.Views
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    
    public partial class LocationsView : Window
    {
        private readonly LocationAdapter _adapter;
        public LocationsView()
        {
            InitializeComponent();

            // Load data
            _adapter = new LocationAdapter();
            LoadLocations();
        }

        private void LoadLocations()
        {
            List<Location> locations = _adapter.GetAll().ToList();
            LocationGrid.ItemsSource = locations;
        }

        private void AddLocation_Click(object sender, RoutedEventArgs e)
        {
            var addWindow = new AddLocationView();
            addWindow.Owner = this;
            addWindow.ShowDialog();
            LoadLocations(); // Refresh after adding
        }

        private void GoToMenu_Click(object sender, RoutedEventArgs e)
        {
            var menu = new MainWindow();
            menu.Show();
            this.Close();
        }
        private void GoToReports_Click(object sender, RoutedEventArgs e)
        {
            new ReportsView().Show();
            this.Close();
        }
        private void GoToDevices_Click(object sender, RoutedEventArgs e)
        {
            var devices = new DevicesView();
            devices.Show();
            this.Close();
        }

        private void GoToPrivacy_Click(object sender, RoutedEventArgs e)
        {
            new PrivacyView().Show();
            this.Close();
        }
    }
}
