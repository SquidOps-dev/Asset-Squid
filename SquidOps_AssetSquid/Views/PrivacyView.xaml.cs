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
        // Go Back to Menu
        private void Menu_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            this.Close();
        }

        // Navigate to DevicesView
        private void Devices_Click(object sender, RoutedEventArgs e)
        {
            new DevicesView().Show();
            this.Close();
        }

        //Navigate to LocationsView
        private void Locations_Click(object sender, RoutedEventArgs e)
        {
            new LocationsView().Show();
            this.Close();
        }

        // Navigation to ReportsView
        private void Reports_Click(object sender, RoutedEventArgs e)
        {
            new ReportsView().Show();
            this.Close();
        }

    }
}
