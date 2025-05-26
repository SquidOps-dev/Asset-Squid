// UserControls/AppMenu.xaml.cs
// A shared application menu bar for File, Settings, and Help across all views.

using Microsoft.Data.Sqlite;            // For SQLite backup API
using Microsoft.Win32;                  // For file dialogs
using System;
using System.Collections.Generic;
using System.Diagnostics;               // For launching external processes
using System.IO;
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
using System.Windows.Navigation;

namespace SquidOps_AssetSquid.UserControls
{
    /// <summary>
    /// Interaction logic for AppMenu.xaml
    /// </summary>
    public partial class AppMenu : UserControl
    {
        public AppMenu() => InitializeComponent();  // Load the XAML-defined UI

        // === File Menu Commands ===

        /// <summary>
        /// Handles the "Backup Database" menu item click.
        /// Prompts the user for a file path and uses SQLite Backup API to copy the database.
        /// </summary>
        private void BackupDb_Click(object sender, RoutedEventArgs e)
        {
            // Configure save dialog to pick backup file destination
            var dlg = new SaveFileDialog
            {
                Title = "Backup Inventory DB",
                Filter = "SQLite DB|*.db",
                FileName = $"inventory-backup-{DateTime.Today:yyyy-MM-dd}.db"
            };

            // If user cancels, abort
            if (dlg.ShowDialog() != true) return;

            // Path to the live database in application folder
            var src = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "inventory.db");

            try
            {
                // Open source and destination connections and perform backup
                using var c1 = new SqliteConnection($"Data Source={src}");
                c1.Open();
                using var c2 = new SqliteConnection($"Data Source={dlg.FileName}");
                c2.Open();
                c1.BackupDatabase(c2);

                // Inform user of success
                MessageBox.Show("Backup successful!", "Backup", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                // Show error details on failure
                MessageBox.Show($"Backup failed:\n{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Handles the "Restore Database" menu item click.
        /// Prompts user for a backup file and overwrites the current database.
        /// </summary>
        private void RestoreDb_Click(object sender, RoutedEventArgs e)
        {
            // Configure open dialog to select backup file
            var dlg = new OpenFileDialog
            {
                Title = "Select Backup to Restore",
                Filter = "SQLite DB|*.db"
            };

            // If user cancels, abort
            if (dlg.ShowDialog() != true) return;

            // Confirm overwrite action
            if (MessageBox.Show("This will overwrite the current database. Continue?",
                                "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Warning)
                != MessageBoxResult.Yes) return;

            // Destination path back to the live database
            var destPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "inventory.db");
            try
            {
                // Open backup and live DB connections, then copy pages over
                using var source = new SqliteConnection($"Data Source={dlg.FileName}");
                source.Open();
                using var dest = new SqliteConnection($"Data Source={destPath}");
                dest.Open();
                source.BackupDatabase(dest);

                // Inform user and prompt restart
                MessageBox.Show("Restore successful! Please restart.", "Restore", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                // Show error if restore fails
                MessageBox.Show($"Restore failed:\n{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Print_Click(object sender, RoutedEventArgs e)
        {
            // Determine which window this toolbar belongs to
            var win = Window.GetWindow(this);
            if (win is SquidOps_AssetSquid.Views.ReportsView reportsView)
            {
                // Delegate to the ReportsView-specific print method
                reportsView.Print_Click(sender, e);
            }
            else
            {
                // Generic print for other windows: print the entire window visual
                var dlg = new PrintDialog();
                if (dlg.ShowDialog() == true && win != null)
                {
                    dlg.PrintVisual(win, win.Title);
                }
            }
        }

        // === Exit Command ===

        /// <summary>
        /// Handles the "Exit" menu item click and terminates the application.
        /// </summary>
        private void Exit_Click(object sender, RoutedEventArgs e)
            => Application.Current.Shutdown();


        // === Settings Menu Commands ===

        /// <summary>
        /// Opens the Windows Printers & Scanners settings pane.
        /// </summary>
        private void PrinterSetup_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Launch system settings for printers
                Process.Start(new ProcessStartInfo("ms-settings:printers") { UseShellExecute = true });
            }
            catch
            {
                // Fallback if direct launch fails
                MessageBox.Show(
                    "Unable to open printer settings. Please open manually via Settings > Devices > Printers & scanners.",
                    "Printer Setup", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        /// <summary>
        /// Placeholder for application preferences dialog (not yet implemented).
        /// </summary>
        private void Preferences_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(
                "Preferences dialog is not yet implemented.",
                "Preferences", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        // === Help Menu Commands ===

        /// <summary>
        /// Shows About information and offers to start an email to support.
        /// </summary>
        private void About_Click(object sender, RoutedEventArgs e)
        {
            // Show about info with an option to email support
            var result = MessageBox.Show(
                "SquidOps Asset Manager v1.0\n© 2025 YourCompany.\n" +
                "Click Yes to contact SquidOps-dev support via email.",
                "About", MessageBoxButton.YesNo, MessageBoxImage.Information);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    // Open default email client with support address
                    Process.Start(new ProcessStartInfo("mailto:zac2225704@maricopa.edu") { UseShellExecute = true });
                }
                catch
                {
                    // Inform user to email manually if launch fails
                    MessageBox.Show(
                        "Unable to open email client. Please email zac2225704@maricopa.edu manually.",
                        "Email Support", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }    
    }
}
