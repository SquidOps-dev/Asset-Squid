using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Documents;
using SquidOps_AssetSquid.DAL;
using SquidOps_AssetSquid.Models;

namespace SquidOps_AssetSquid.Views
{
    public partial class ReportsView : Window
    {
        private readonly LocationAdapter _locationAdapter = new LocationAdapter();
        private readonly DeviceTypeAdapter _deviceTypeAdapter = new DeviceTypeAdapter();
        private readonly DeviceAdapter _deviceAdapter = new DeviceAdapter();
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
            locations.Insert(0, new Location { LocationId = -1, Name = "Filter by Location..." });
            LocationFilter.ItemsSource = locations;
            LocationFilter.SelectedIndex = 0;

            var types = _deviceTypeAdapter.GetAll().ToList();
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
            if (LocationFilter.SelectedItem is Location loc && loc.LocationId != -1)
                filtered = filtered.Where(d => d.LocationId == loc.LocationId);
            if (TypeFilter.SelectedItem is DeviceType dt && dt.DeviceTypeId != -1)
                filtered = filtered.Where(d => d.DeviceTypeId == dt.DeviceTypeId);

            if (!_isPlaceholderActive && !string.IsNullOrWhiteSpace(SearchBox.Text))
            {
                var kw = SearchBox.Text.ToLower();
                filtered = filtered.Where(d =>
                    d.Name.ToLower().Contains(kw) ||
                    d.SerialNumber.ToLower().Contains(kw) ||
                    d.IpAddress.ToLower().Contains(kw) ||
                    d.MacAddress.ToLower().Contains(kw) ||
                    d.DeviceModel.ToLower().Contains(kw)
                );
            }

            ReportGrid.ItemsSource = filtered.ToList();
        }

        private void SearchBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (_isPlaceholderActive)
            {
                SearchBox.Text = "";
                _isPlaceholderActive = false;
                SearchBox.Foreground = Brushes.Black;
            }
        }

        private void SearchBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(SearchBox.Text))
            {
                _isPlaceholderActive = true;
                SearchBox.Text = "Search devices...";
                SearchBox.Foreground = Brushes.Gray;
            }
        }

        private void Menu_Click(object sender, RoutedEventArgs e) { new MainWindow().Show(); this.Close(); }
        private void Devices_Click(object sender, RoutedEventArgs e) { new DevicesView().Show(); this.Close(); }
        private void Locations_Click(object sender, RoutedEventArgs e) { new LocationsView().Show(); this.Close(); }
        private void Privacy_Click(object sender, RoutedEventArgs e) { new PrivacyView().Show(); this.Close(); }

        private void Print_Click(object sender, RoutedEventArgs e)
        {
            // 1) Show the standard Print dialog
            var dlg = new PrintDialog();
            if (dlg.ShowDialog() != true)
                return;

            // 2) Snapshot your DataGrid with printer-friendly colors
            var origBg = ReportGrid.Background;
            var origRowBg = ReportGrid.RowBackground;
            var origAltRow = ReportGrid.AlternatingRowBackground;
            var origFg = ReportGrid.Foreground;

            ReportGrid.Background = Brushes.White;
            ReportGrid.RowBackground = Brushes.White;
            ReportGrid.AlternatingRowBackground = Brushes.LightGray;
            ReportGrid.Foreground = Brushes.Black;

            // 3) Ensure proper measurement before printing
            var printableSize = new Size(dlg.PrintableAreaWidth, dlg.PrintableAreaHeight);
            ReportGrid.Measure(printableSize);
            ReportGrid.Arrange(new Rect(printableSize));

            // 4) Print just the grid
            dlg.PrintVisual(ReportGrid, "Device Report");

            // 5) Restore original styling
            ReportGrid.Background = origBg;
            ReportGrid.RowBackground = origRowBg;
            ReportGrid.AlternatingRowBackground = origAltRow;
            ReportGrid.Foreground = origFg;
        }
    }
}
