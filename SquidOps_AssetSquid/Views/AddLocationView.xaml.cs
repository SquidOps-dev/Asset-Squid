using System.Windows;
using SquidOps_AssetSquid.DAL;
using SquidOps_AssetSquid.Models;

namespace SquidOps_AssetSquid.Views
{
    /// <summary>
    /// Window for adding new locations to the system
    /// </summary>
    public partial class AddLocationView : Window
    {
        // Adapter for CRUD operations on Location; consider injecting via constructor for better testability
        private readonly LocationAdapter _adapter = new LocationAdapter();

        private readonly Location? _editingLocation;

        /// <summary>
        /// Initializes the window and data adapter
        /// </summary>
        public AddLocationView()
        {
            InitializeComponent(); // Load the XAML-defined UI

            // Note: _adapter is already initialized above; this re-assignment is redundant
            // Consider removing one initialization or using a DI container to supply the adapter
            _adapter = new LocationAdapter();
        }

        /// <summary>
        /// Opens the dialog in “Edit” mode, pre-populating fields.
        /// </summary>
        public AddLocationView(Location existing) : this()
        {
            _editingLocation = existing;

            // Change the window title so the user knows they’re editing
            this.Title = "Edit Location";

            // Pre-fill UI fields
            LocationNameBox.Text = existing.Name; 
        }

        /// <summary>
        /// Handles the Save button click: validates input and inserts a new Location
        /// </summary>
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            // 1) Validation
            var name = LocationNameBox.Text.Trim();
            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show(
                    "Please enter a name.",
                    "Validation",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning
                );
                return;
            }

            // 2) EDIT mode?
            if (_editingLocation != null)
            {
                // Update the existing model
                _editingLocation.Name = name;
                _adapter.UpdateLocation(_editingLocation);  // Calls your adapter’s Update
            }
            else
            {
                // 3) ADD mode: exactly your existing logic
                var loc = new Location { Name = name };
                _adapter.InsertLocation(loc);
            }

            // 4) Close dialog with “OK”
            this.DialogResult = true;
        }

        /// <summary>
        /// Opens another AddLocationView window; may not be necessary in this context
        /// </summary>
        private void AddLocation_Click(object sender, RoutedEventArgs e)
        {
            // Creates a new instance of this same window
            var addLocationWindow = new AddLocationView();
            // Show modally so user must close before returning
            addLocationWindow.ShowDialog();
        }
    }
}
