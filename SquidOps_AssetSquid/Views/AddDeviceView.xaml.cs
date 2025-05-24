using System.Windows; // Provides Window base class for WPF
using System.Windows.Controls; // Provides Control types like ComboBox, TextBox
using SquidOps_AssetSquid.DAL; // Data access layer for retrieving and storing data
using SquidOps_AssetSquid.Models; // Data models for Device and related entities

namespace SquidOps_AssetSquid.Views
{
    /// <summary>
    /// Window for adding a new device or editing an existing one
    /// </summary>
    public partial class AddDeviceView : Window
    {
        // Adapter for device CRUD operations
        private readonly DeviceAdapter _deviceAdapter = new DeviceAdapter();
        // Adapter for retrieving locations
        private readonly LocationAdapter _locationAdapter = new LocationAdapter();
        // Adapter for retrieving device types
        private readonly DeviceTypeAdapter _typeAdapter = new DeviceTypeAdapter();

        // Holds the ID of the device being edited; null when adding a new device
        private int? _editingId = null;

        /// <summary>
        /// Constructor for adding a new device
        /// </summary>
        public AddDeviceView()
        {
            InitializeComponent();       // Load components defined in XAML
            LoadDropDowns();             // Populate dropdowns for location and type
        }

        /// <summary>
        /// Constructor for editing an existing device
        /// </summary>
        /// <param name="device">Device to load into the form</param>
        public AddDeviceView(Device device)
        {
            InitializeComponent();       // Load components from XAML
            LoadDropDowns();             // Populate dropdown lists

            _editingId = device.DeviceId; // Set editing ID for update logic

            // Pre-fill form fields with device data
            NameBox.Text = device.Name;
            SerialBox.Text = device.SerialNumber;
            IpBox.Text = device.IpAddress;
            MacBox.Text = device.MacAddress;
            ModelBox.Text = device.DeviceModel;
            LocationCombo.SelectedValue = device.LocationId;
            TypeCombo.SelectedValue = device.DeviceTypeId;
        }

        /// <summary>
        /// Populates the LocationCombo and TypeCombo dropdown controls
        /// </summary>
        private void LoadDropDowns()
        {
            var locations = _locationAdapter.GetAll();           // Fetch list of locations
            LocationCombo.ItemsSource = locations;               // Bind to combo box
            LocationCombo.DisplayMemberPath = "Name";            // Display the Name property
            LocationCombo.SelectedValuePath = "LocationId";      // Use LocationId as value

            var types = _typeAdapter.GetAll();                   // Fetch list of types
            TypeCombo.ItemsSource = types;                       // Bind to combo box
            TypeCombo.DisplayMemberPath = "TypeName";            // Display the TypeName property
            TypeCombo.SelectedValuePath = "DeviceTypeId";        // Use DeviceTypeId as value

            // TODO: add default selection placeholder item if needed
        }

        /// <summary>
        /// Handles the click event for saving a device
        /// </summary>
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            // Construct a Device object from form inputs
            var device = new Device
            {
                Name = NameBox.Text.Trim(),
                SerialNumber = SerialBox.Text.Trim(),
                IpAddress = IpBox.Text.Trim(),
                MacAddress = MacBox.Text.Trim(),
                DeviceModel = ModelBox.Text.Trim(),
                LocationId = (int)LocationCombo.SelectedValue,
                DeviceTypeId = (int)TypeCombo.SelectedValue
            };

            if (_editingId.HasValue)
            {
                device.DeviceId = _editingId.Value;             // Set ID for update
                _deviceAdapter.UpdateDevice(device);             // Update existing device
                MessageBox.Show("Device updated!");              // Notify user
            }
            else
            {
                _deviceAdapter.InsertDevice(device);             // Insert new device
                MessageBox.Show("Device added!");                // Notify user
            }

            this.Close();                                        // Close the window
        }

        /// <summary>
        /// Handles selection changes in the TypeCombo dropdown
        /// </summary>
        private void TypeCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // TODO: implement logic when device type selection changes
        }
    }
}
