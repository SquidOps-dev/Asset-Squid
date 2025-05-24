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
using QRCoder;
using SquidOps_AssetSquid.DAL;
using SquidOps_AssetSquid.Models;

namespace SquidOps_AssetSquid.Views
{
    /// <summary>
    /// Interaction logic for ReportsView.xaml
    /// </summary>
    public partial class ReportsView : Window
    {
        // Adapter for locations; consider injecting this via constructor for testability
        private LocationAdapter _locationAdapter = new LocationAdapter();
        // Adapter for device types; direct instantiation couples this class tightly to implementation
        private DeviceTypeAdapter _deviceTypeAdapter = new DeviceTypeAdapter();
        // Adapter for devices; think about marking as readonly and using DI
        private DeviceAdapter _deviceAdapter = new DeviceAdapter();
        // Master list of all devices retrieved; used as the source for filtering
        private List<Device> _allDevices;

        // Tracks whether the search box is currently showing placeholder text
        private bool _isPlaceholderActive = true;

        /// <summary>
        /// Constructor: initializes UI components and loads the filters and devices
        /// </summary>
        public ReportsView()
        {
            InitializeComponent();  // Load XAML

            LoadFilters();         // Populate filter dropdowns
            LoadDevices();         // Load data into grid
        }

        /// <summary>
        /// Populates the location and type filters with a default "Filter by..." option
        /// </summary>
        private void LoadFilters()
        {
            // Fetch all locations from adapter
            var locations = _locationAdapter.GetAll().ToList();
            // Fetch all device types from adapter
            var types = _deviceTypeAdapter.GetAll().ToList();

            // Insert a "no filter" placeholder at the front of the list
            locations.Insert(0, new Location { LocationId = -1, Name = "Filter by Location..." });
            // Bind to the dropdown and set default
            LocationFilter.ItemsSource = locations;
            LocationFilter.SelectedIndex = 0;

            // Insert a "no filter" placeholder for device types
            types.Insert(0, new DeviceType { DeviceTypeId = -1, TypeName = "Filter by Type..." });
            // Bind type dropdown and set default
            TypeFilter.ItemsSource = types;
            TypeFilter.SelectedIndex = 0;
        }

        /// <summary>
        /// Loads device data into the grid; handles nulls gracefully
        /// </summary>
        private void LoadDevices()
        {
            // Retrieve devices, include details; fallback to empty list if null
            _allDevices = _deviceAdapter.GetAllWithDetails().ToList() ?? new List<Device>();
            // Set the grid's data source to the full list
            ReportGrid.ItemsSource = _allDevices;
        }

        /// <summary>
        /// Applies filters based on selected location, type, and search keyword
        /// </summary>
        private void Search_Click(object sender, RoutedEventArgs e)
        {
            // Start filtering from the master device list
            var filtered = _allDevices.AsEnumerable();

            // Filter by selected location if not the placeholder
            if (LocationFilter.SelectedItem is Location selectedLocation && selectedLocation.LocationId != -1)
                filtered = filtered.Where(d => d.LocationId == selectedLocation.LocationId);

            // Filter by selected device type if not the placeholder
            if (TypeFilter.SelectedItem is DeviceType selectedType && selectedType.DeviceTypeId != -1)
                filtered = filtered.Where(d => d.DeviceTypeId == selectedType.DeviceTypeId);

            // Text-based search: ignore placeholder text
            if (!string.IsNullOrWhiteSpace(SearchBox.Text) && SearchBox.Text != "Search devices...")
            {
                string keyword = SearchBox.Text.ToLower();
                // Perform case-insensitive Contains on multiple fields
                filtered = filtered.Where(d =>
                    d.Name.ToLower().Contains(keyword) ||
                    d.SerialNumber.ToLower().Contains(keyword) ||
                    d.IpAddress.ToLower().Contains(keyword) ||
                    d.MacAddress.ToLower().Contains(keyword) ||
                    d.DeviceModel.ToLower().Contains(keyword)
                );
            }

            // Update the grid to show only filtered results
            ReportGrid.ItemsSource = filtered.ToList();
        }

        /// <summary>
        /// Clears placeholder text when the search box gains focus
        /// </summary>
        private void SearchBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (_isPlaceholderActive)
            {
                SearchBox.Text = string.Empty;   // Remove placeholder
                _isPlaceholderActive = false;    // Mark placeholder as inactive
            }
            // Ensure user typing appears in white for contrast
            SearchBox.Foreground = Brushes.White;
            SearchBox.CaretIndex = 0;            // Move caret to start
        }

        /// <summary>
        /// Potential live-search hook: only active when placeholder is gone
        /// </summary>
        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (_isPlaceholderActive) return;    // Skip if still showing placeholder

            SearchBox.Foreground = Brushes.Black; // Switch to black for user input
            string query = SearchBox.Text.Trim().ToLower();
            // TODO: implement live filtering here if desired
        }

        /// <summary>
        /// Restores placeholder text if the search box is left empty
        /// </summary>
        private void SearchBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(SearchBox.Text))
            {
                SearchBox.Text = "Search devices...";  // Reapply placeholder
                SearchBox.Foreground = Brushes.Gray;   // Dim placeholder text
                _isPlaceholderActive = true;           // Mark placeholder as active
            }
        }

        // Navigation: open MainWindow and close this view
        private void Menu_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            this.Close();
        }

        // Navigation: open DevicesView and close this view
        private void Devices_Click(object sender, RoutedEventArgs e)
        {
            new DevicesView().Show();
            this.Close();
        }

        // Navigation: open LocationsView and close this view
        private void Locations_Click(object sender, RoutedEventArgs e)
        {
            new LocationsView().Show();
            this.Close();
        }

        // Navigation: open PrivacyView and close this view
        private void Privacy_Click(object sender, RoutedEventArgs e)
        {
            new PrivacyView().Show();
            this.Close(); // Close current window
        }
    }
}

