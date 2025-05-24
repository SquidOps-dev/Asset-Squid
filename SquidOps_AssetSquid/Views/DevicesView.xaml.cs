using System.Windows;
using System.Windows.Controls;
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

            LoadDevices();
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

        // Navigate to MainWindow
        private void Menu_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            this.Close(); // Close this window.
        }

        // Navigate to ReportsView Window
        private void Reports_Click(object sender, RoutedEventArgs e)
        {
            new ReportsView().Show();
            this.Close();
        }

        // Navigate to LocationsView Window
        private void Locations_Click(object sender, RoutedEventArgs e)
        {
            new LocationsView().Show(); // Make sure this view exists
            this.Close(); // Close current window
        }

        // Navigate to PrivacyView Window
        private void Privacy_Click(object sender, RoutedEventArgs e)
        {
            new PrivacyView().Show();
            this.Close(); // Close current window
        }

        private void EditDevice_Click(object sender, RoutedEventArgs e)
        {
            var device = (sender as Button)?.Tag as Device;
            if (device == null) return;

            var editWindow = new AddDeviceView(device); // Overload constructor for editing
            editWindow.ShowDialog();
            LoadDevices();
        }

        private void DeleteDevice_Click(object sender, RoutedEventArgs e)
        {
            var device = (sender as Button)?.Tag as Device;
            if (device == null) return;

            var result = MessageBox.Show($"Delete device {device.Name}?", "Confirm", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                _adapter.DeleteDeviceById(device.DeviceId);
                LoadDevices();
            }
        }

        private void GenerateQR_Click(object sender, RoutedEventArgs e)
        {
            // Ensure a device is selected
            if (DeviceGrid.SelectedItem is Device selectedDevice)
            {
                // Open the QRCodeView and pass the device
                var qrWindow = new QRCodeView(selectedDevice);
                qrWindow.ShowDialog();
            }
            else
            {
                MessageBox.Show("Please select a device to generate a QR code.", "No Device Selected", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
