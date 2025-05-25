using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using SquidOps_AssetSquid.Models;
using SquidOps_AssetSquid.DAL;
using SquidOps_AssetSquid.Extensions;

namespace SquidOps_AssetSquid.Views
{
    /// <summary>
    /// Window for viewing, adding, editing, deleting, and generating QR codes for devices
    /// </summary>
    public partial class DevicesView : Window
    {
        // Adapter for CRUD operations on Device entities
        private readonly DeviceAdapter _adapter;

        /// <summary>
        /// Constructor: initializes UI components and loads devices into the grid
        /// </summary>
        public DevicesView()
        {
            InitializeComponent(); // Load the XAML-defined UI

            _adapter = new DeviceAdapter(); // Instantiate the data adapter
            LoadDevices(); // Populate the DataGrid with device data
        }

        /// <summary>
        /// Retrieves all devices from the adapter and binds them to the DeviceGrid
        /// </summary>
        private void LoadDevices()
        {
            // GetAll() returns IEnumerable<Device>; ToList() materializes it into a List<Device>
            List<Device> devices = _adapter.GetAll().ToList();
            DeviceGrid.ItemsSource = devices; // Bind the list to the DataGrid
        }

        /// <summary>
        /// Opens the AddDeviceView to add a new device, then reloads the grid
        /// </summary>
        private void AddDevice_Click(object sender, RoutedEventArgs e)
        {
            var addWindow = new AddDeviceView
            {
                Owner = this // Set this window as owner for modal behavior
            };
            addWindow.ShowDialog(); // Show as modal dialog
            LoadDevices(); // Refresh device list after possible addition
        }

        /// <summary>
        /// Navigate to the main menu and close this view
        /// </summary>
        private void Menu_Click(object sender, RoutedEventArgs e)
        {
            this.NavigateTo(new MainWindow());
            this.Close();
        }

        /// <summary>
        /// Open the ReportsView window and close this view
        /// </summary>
        private void Reports_Click(object sender, RoutedEventArgs e)
        {
            this.NavigateTo(new ReportsView());
            this.Close();
        }

        /// <summary>
        /// Open the LocationsView window and close this view
        /// </summary>
        private void Locations_Click(object sender, RoutedEventArgs e)
        {
            this.NavigateTo(new LocationsView());
            this.Close();
        }

        /// <summary>
        /// Open the PrivacyView window and close this view
        /// </summary>
        private void Privacy_Click(object sender, RoutedEventArgs e)
        {
            this.NavigateTo(new PrivacyView());
            this.Close();
        }

        /// <summary>
        /// Opens AddDeviceView in edit mode for the device associated with the clicked button
        /// </summary>
        private void EditDevice_Click(object sender, RoutedEventArgs e)
        {
            // The Button's Tag property holds the Device instance
            if ((sender as Button)?.Tag is Device device)
            {
                var editWindow = new AddDeviceView(device); // Pass the device to the constructor overload
                editWindow.ShowDialog();
                LoadDevices(); // Refresh grid after editing
            }
        }

        /// <summary>
        /// Deletes the specified device after user confirmation
        /// </summary>
        private void DeleteDevice_Click(object sender, RoutedEventArgs e)
        {
            // The Button's Tag contains the Device to delete
            if ((sender as Button)?.Tag is Device device)
            {
                // Show a confirmation dialog
                var result = MessageBox.Show(
                    $"Delete device {device.Name}?", // Confirmation message showing device name
                    "Confirm Deletion",             // Dialog title
                    MessageBoxButton.YesNo,          // Buttons: Yes and No
                    MessageBoxImage.Warning          // Warning icon
                );

                if (result == MessageBoxResult.Yes)
                {
                    _adapter.DeleteDeviceById(device.DeviceId); // Perform the deletion
                    LoadDevices(); // Refresh the grid to reflect changes
                }
            }
        }

        /// <summary>
        /// Generates a QR code for the selected device by opening the QRCodeView
        /// </summary>
        private void GenerateQR_Click(object sender, RoutedEventArgs e)
        {
            // Ensure a device is selected in the grid
            if (DeviceGrid.SelectedItem is Device selectedDevice)
            {
                var qrWindow = new QRCodeView(selectedDevice); // Pass the device to display its QR
                qrWindow.ShowDialog(); // Show as modal dialog
            }
            else
            {
                // User needs to select a device first
                MessageBox.Show(
                    "Please select a device to generate a QR code.", // Informative message
                    "No Device Selected",                            // Dialog title
                    MessageBoxButton.OK,                             // OK button only
                    MessageBoxImage.Information                      // Information icon
                );
            }
        }
    }
}
