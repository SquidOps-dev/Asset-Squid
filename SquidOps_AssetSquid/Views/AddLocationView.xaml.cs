using System.Windows;
using SquidOps_AssetSquid.DAL;
using SquidOps_AssetSquid.Models;

namespace SquidOps_AssetSquid.Views
{
    /// <summary>
    /// Window
    /// </summary>
    public partial class AddLocationView : Window
    {
        private readonly LocationAdapter _adapter = new LocationAdapter();
        public AddLocationView()
        {
            InitializeComponent();
            _adapter = new LocationAdapter();
        }
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            var name = LocationNameBox.Text.Trim();
            if (!string.IsNullOrWhiteSpace(name))
            {
                _adapter.InsertLocation(new Location { Name = name });
                MessageBox.Show("Location added successfully.");
                this.Close();
            }
            else
            {
                MessageBox.Show("Please enter a location name.");
            }
        }
        private void AddLocation_Click(object sender, RoutedEventArgs e)
        {
            var addLocationWindow = new AddLocationView();
            addLocationWindow.ShowDialog();
        }
    }
}
