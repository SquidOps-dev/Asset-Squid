using System.Windows;
using SquidOps_AssetSquid.Extensions;

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
            this.NavigateTo(new MainWindow());
            this.Close();
        }

        // Navigate to DevicesView
        private void Devices_Click(object sender, RoutedEventArgs e)
        {
            this.NavigateTo(new DevicesView());
            this.Close();
        }

        //Navigate to LocationsView
        private void Locations_Click(object sender, RoutedEventArgs e)
        {
            this.NavigateTo(new LocationsView());
            this.Close();
        }

        // Navigation to ReportsView
        private void Reports_Click(object sender, RoutedEventArgs e)
        {
            this.NavigateTo(new ReportsView());
            this.Close();
        }

    }
}
