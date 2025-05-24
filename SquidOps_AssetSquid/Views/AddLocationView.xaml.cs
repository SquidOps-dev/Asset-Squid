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
        /// Handles the Save button click: validates input and inserts a new Location
        /// </summary>
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            // Read and trim the input from the LocationNameBox
            var name = LocationNameBox.Text.Trim();

            // Ensure the name is not empty or whitespace
            if (!string.IsNullOrWhiteSpace(name))
            {
                // Insert new Location record
                _adapter.InsertLocation(new Location { Name = name });
                // Notify user of success
                MessageBox.Show("Location added successfully.");
                // Close window to return to previous view
                this.Close();
            }
            else
            {
                // Prompt user to enter a valid name
                MessageBox.Show("Please enter a location name.");
            }
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
