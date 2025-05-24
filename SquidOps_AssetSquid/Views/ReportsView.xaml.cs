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

        //private readonly DeviceAdapter _deviceAdapter;
        private  LocationAdapter _locationAdapter = new LocationAdapter();
        private  DeviceTypeAdapter _deviceTypeAdapter = new DeviceTypeAdapter();
        private  DeviceAdapter _deviceAdapter = new DeviceAdapter();
        private List<Device> _allDevices;

        private bool _isPlaceholderActive = true;

        public ReportsView()
        {
            InitializeComponent();

            LoadFilters();
            LoadDevices();
        }
       
        private void LoadFilters()
        {
            var locations = _locationAdapter.GetAll().ToList();

            var types = _deviceTypeAdapter.GetAll().ToList();

            locations.Insert(0, new Location { LocationId = -1, Name = "Filter by Location..." });

            LocationFilter.ItemsSource = locations;
            
            LocationFilter.SelectedIndex = 0;

            types.Insert(0, new DeviceType { DeviceTypeId = -1, TypeName = "Filter by Type..." });
            
            TypeFilter.ItemsSource = types;
            
            TypeFilter.SelectedIndex = 0;
        }

        private void LoadDevices()
        {
            _allDevices = _deviceAdapter.GetAllWithDetails().ToList() ?? new List<Device>();
            ReportGrid.ItemsSource = _allDevices;
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            var filtered = _allDevices.AsEnumerable();

            if (LocationFilter.SelectedItem is Location selectedLocation)
                filtered = filtered.Where(d => d.LocationId == selectedLocation.LocationId);

            if (TypeFilter.SelectedItem is DeviceType selectedType)
                filtered = filtered.Where(d => d.DeviceTypeId == selectedType.DeviceTypeId);

            if (!string.IsNullOrWhiteSpace(SearchBox.Text) && SearchBox.Text != "Search devices...")
            {
                string keyword = SearchBox.Text.ToLower();
                filtered = filtered.Where(d =>
                    d.Name.ToLower().Contains(keyword) ||
                    d.SerialNumber.ToLower().Contains(keyword) ||
                    d.IpAddress.ToLower().Contains(keyword) ||
                    d.MacAddress.ToLower().Contains(keyword) ||
                    d.DeviceModel.ToLower().Contains(keyword));
            }

            ReportGrid.ItemsSource = filtered.ToList();
        }

        private void SearchBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (_isPlaceholderActive)
            {
                SearchBox.Text = string.Empty;
                _isPlaceholderActive = false;
            }
            SearchBox.Foreground = Brushes.White; 
            SearchBox.CaretIndex = 0;
        }


        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (_isPlaceholderActive) return;

            SearchBox.Foreground = Brushes.Black; // Ensure visible text
            string query = SearchBox.Text.Trim().ToLower();
            // search/filter logic here...
        }


        private void SearchBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(SearchBox.Text))
            {
                SearchBox.Text = "Search devices...";
                SearchBox.Foreground = Brushes.Gray;
                _isPlaceholderActive = true;
            }
        }

        // Navigate to MainWindow 
        private void Menu_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            this.Close();
        }

        // Navigate to DevicesView window
        private void Devices_Click(object sender, RoutedEventArgs e)
        {
            new DevicesView().Show();
            this.Close();
        }

        //Navigate to LocationsView window
        private void Locations_Click(object sender, RoutedEventArgs e)
        {
            new LocationsView().Show();
            this.Close();
        }

        // Navigate to PrivacyView window
        private void Privacy_Click(object sender, RoutedEventArgs e)
        {
            new PrivacyView().Show();
            this.Close(); // Close current window
        }
    }
}
