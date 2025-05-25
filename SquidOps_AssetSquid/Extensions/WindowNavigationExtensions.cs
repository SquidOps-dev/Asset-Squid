// WindowNavigationExtensions.cs

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SquidOps_AssetSquid.Extensions
{
    public static class WindowNavigationExtensions
    {
        /// <summary>
        /// Opens the destination window using the same size/state/monitor as the source window, then closes the source.
        /// </summary>
        public static void NavigateTo(this Window source, Window destination)
        {
            // Ensure manual placement
            destination.WindowStartupLocation = WindowStartupLocation.Manual;

            if (source.WindowState == WindowState.Maximized)
            {
                // Use RestoreBounds so we stay on the same monitor and size when unmaximized
                var rb = source.RestoreBounds;
                destination.Left = rb.Left;
                destination.Top = rb.Top;
                destination.Width = rb.Width;
                destination.Height = rb.Height;
                destination.WindowState = WindowState.Maximized;
            }
            else
            {
                // Copy position & size exactly
                destination.Left = source.Left;
                destination.Top = source.Top;
                destination.Width = source.Width;
                destination.Height = source.Height;
                destination.WindowState = WindowState.Normal;
            }

            // Show the new window and close the old one
            destination.Show();
            source.Close();
        }
    }
}
