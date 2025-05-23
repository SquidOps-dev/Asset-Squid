using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using SquidOps_AssetSquid.Models;
using SquidOps_AssetSquid.DAL;

namespace SquidOps_AssetSquid.Views
{
    public partial class DevicesView : Window
    {
        private readonly DeviceAdapter _adapter;
        public DevicesView()
        {
            InitializeComponent();

            _adapter = new DeviceAdapter();

            LoadDevices(); // optional if you're loading data here
        }

        private void LoadDevices()
        {
            List<Device> devices = _adapter.GetAll().ToList();
            DeviceGrid.ItemsSource = devices;
        }

        private void AddDevice_Click(object sender, RoutedEventArgs e)
        {
            var addWindow = new AddDeviceView();
            addWindow.Owner = this;
            addWindow.ShowDialog();
            LoadDevices(); // Refresh after adding
        }

        private void Menu_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            this.Close();
        }

        private void Locations_Click(object sender, RoutedEventArgs e)
        {
            // new LocationsView().Show(); // Make sure this view exists
            // this.Close();
            MessageBox.Show("Locations navigation not yet implemented.");
        }

        private void Privacy_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Privacy Policy goes here.", "Privacy", MessageBoxButton.OK, MessageBoxImage.Information);
        }

    }
}
