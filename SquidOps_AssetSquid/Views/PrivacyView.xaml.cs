using System.Windows;

namespace SquidOps_AssetSquid.Views
{
    /// <summary>
    /// Interaction logic for PrivacyView.xaml
    /// </summary>
    public partial class PrivacyView : Window
    {
        public PrivacyView()
        {
            InitializeComponent();
        }
        
        private void Menu_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show(); // Navigate to MainWindow
            this.Close(); // Close current window
        }

        // Navigate to DevicesView
        private void Devices_Click(object sender, RoutedEventArgs e)
        {
            new DevicesView().Show(); // Navigate to DeviceView window
            this.Close(); // Then close this window
        }

        //Navigate to LocationsView
        private void Locations_Click(object sender, RoutedEventArgs e)
        {
            new LocationsView().Show(); // Navigate to LocationsView
            this.Close(); // Then close current window
        }

        // Navigation to ReportsView
        private void Reports_Click(object sender, RoutedEventArgs e)
        {
            new ReportsView().Show(); // Navigate to ReportsView
            this.Close(); // Close current window
        }

    }
}
