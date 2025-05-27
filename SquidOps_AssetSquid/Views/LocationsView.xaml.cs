using System.Windows;
using SquidOps_AssetSquid.DAL;
using SquidOps_AssetSquid.Models;
using SquidOps_AssetSquid.Extensions;
using System.Collections.Generic;
using System.Linq;



namespace SquidOps_AssetSquid.Views
{
    /// <summary>
    /// Window for managing location records: display list, add new entries, and navigate
    /// </summary>
    public partial class LocationsView : Window
    {
        // Adapter instance for fetching and saving Location data
        private readonly LocationAdapter _adapter;

        /// <summary>
        /// Constructor initializes the UI and loads location data
        /// </summary>
        public LocationsView()
        {
            InitializeComponent(); // Load XAML-defined layout and controls

            // Instantiate the data adapter (consider constructor injection for testability)
            _adapter = new LocationAdapter();

            // Populate the DataGrid with existing locations
            LoadLocations();
        }

        /// <summary>
        /// Retrieves all locations from the adapter and binds them to the grid
        /// </summary>
        private void LoadLocations()
        {
            // Fetch the list of locations; calling ToList executes the query immediately
            List<Location> locations = _adapter.GetAll().ToList();
            // Bind the retrieved list to the grid's ItemsSource for display
            LocationGrid.ItemsSource = locations;
        }

        /// <summary>
        /// Opens the AddLocationView dialog to add a new location, then refreshes the grid
        /// </summary>
        private void AddLocation_Click(object sender, RoutedEventArgs e)
        {
            // Create and show the modal add-location window
            var addWindow = new AddLocationView
            {
                Owner = this // Set this window as the owner for proper modal behavior
            };
            addWindow.ShowDialog(); // Blocks until the dialog is closed

            // After closing, reload the locations to reflect any new additions
            LoadLocations();
        }

        /// <summary>
        /// Navigate back to the main menu, close this window
        /// </summary>
        private void GoToMenu_Click(object sender, RoutedEventArgs e)
        {
            this.NavigateTo(new MainWindow());
            this.Close();
        }

        /// <summary>
        /// Navigate to the Reports view and close this window
        /// </summary>
        private void GoToReports_Click(object sender, RoutedEventArgs e)
        {
            this.NavigateTo(new ReportsView());
            this.Close();
        }

        /// <summary>
        /// Navigate to the Devices view and close this window
        /// </summary>
        private void GoToDevices_Click(object sender, RoutedEventArgs e)
        {
            this.NavigateTo(new DevicesView());
            this.Close();
        }

        /// <summary>
        /// Navigate to the Privacy view and close this window
        /// </summary>
        private void GoToPrivacy_Click(object sender, RoutedEventArgs e)
        {
            this.NavigateTo(new PrivacyView());
            this.Close();
        }

        /// <summary>
        /// Called by AppMenu when “Edit Location” is selected.
        /// </summary>
        public void EditSelectedLocation()
        {
            if (LocationGrid.SelectedItem is Location loc)
            {
                // Use the new constructor overload
                var dlg = new AddLocationView(loc) { Owner = this };
                if (dlg.ShowDialog() == true)
                    LoadLocations();  // refresh the grid
            }
            else
            {
                MessageBox.Show(
                  "Please select a location to edit.",
                  "No Selection",
                  MessageBoxButton.OK,
                  MessageBoxImage.Information);
            }
        }

        /// <summary>
        /// Called by AppMenu when “Delete Location” is selected.
        /// </summary>
        public void DeleteSelectedLocation()
        {
            if (LocationGrid.SelectedItem is Location l)
            {
                // Ask user to confirm deletion
                var result = MessageBox.Show(
                    $"Delete location: \"{l.Name}\"?",
                    "Confirm Deletion",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning
                );

                if (result == MessageBoxResult.Yes)
                {
                    // Perform deletion and refresh
                    _adapter.DeleteLocationById(l.LocationId);
                    MessageBox.Show($"Location has been deleted");
                    LoadLocations();
                }
            }
            else
            {
                MessageBox.Show(
                    "Please select a location to delete.",
                    "No location Selected",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information
                );
            }
        }
    }
}
