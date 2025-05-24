using System.Windows;
using System.Windows.Controls;
using SquidOps_AssetSquid.DAL;
using SquidOps_AssetSquid.Models;

namespace SquidOps_AssetSquid.Views
{
    /// <summary>
    /// Interaction logic for AddDeviceView.xaml
    /// </summary>
    public partial class AddDeviceView : Window
    {
        private readonly DeviceAdapter _deviceAdapter = new DeviceAdapter();
        private readonly LocationAdapter _locationAdapter = new LocationAdapter();
        private readonly DeviceTypeAdapter _typeAdapter = new DeviceTypeAdapter();

        private int? _editingId = null;

        public AddDeviceView()
        {
            InitializeComponent();
            LoadDropDowns();
        }

        public AddDeviceView(Device device)
        {
            InitializeComponent();
            LoadDropDowns();

            _editingId = device.DeviceId;
            NameBox.Text = device.Name;
            SerialBox.Text = device.SerialNumber;
            IpBox.Text = device.IpAddress;
            MacBox.Text = device.MacAddress;
            ModelBox.Text = device.DeviceModel;
            LocationCombo.SelectedValue = device.LocationId;
            TypeCombo.SelectedValue = device.DeviceTypeId;
        }

        private void LoadDropDowns()
        {
            var locationAdapter = new LocationAdapter();
            var locations = locationAdapter.GetAll();
            LocationCombo.ItemsSource = locations;
            LocationCombo.DisplayMemberPath = "Name";  // property to display
            LocationCombo.SelectedValuePath = "LocationId";  // property used as value

            var typeAdapter = new DeviceTypeAdapter();
            var types = typeAdapter.GetAll();
            TypeCombo.ItemsSource = types;
            TypeCombo.DisplayMemberPath = "TypeName";
            TypeCombo.SelectedValuePath = "DeviceTypeId";
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            var device = new Device
            {
                Name = NameBox.Text,
                SerialNumber = SerialBox.Text,
                IpAddress = IpBox.Text,
                MacAddress = MacBox.Text,
                DeviceModel = ModelBox.Text,
                LocationId = (int)LocationCombo.SelectedValue,
                DeviceTypeId = (int)TypeCombo.SelectedValue
            };

            _deviceAdapter.InsertDevice(device);
            MessageBox.Show("Device added!");
            Close();
        }

       private void TypeCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
