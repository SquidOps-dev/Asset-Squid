using QRCoder;
using SquidOps_AssetSquid.Models;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace SquidOps_AssetSquid.Views
{
    public partial class QRCodeView : Window
    {
        // Constructor: receives a Device object and builds the QR code display
        public QRCodeView(Device device)
        {
            InitializeComponent(); // Load XAML UI elements

            // Combine key device properties into the QR payload text
            string qrText =
                $"Name: {device.Name}\n" +
                $"IP: {device.IpAddress}\n" +
                $"SN: {device.SerialNumber}\n" +
                $"MAC: {device.MacAddress}\n" +
                $"Model: {device.DeviceModel}";

            // Create the QR code data with ECC level Q for high error tolerance
            using var qrGenerator = new QRCodeGenerator();
            using var qrData = qrGenerator.CreateQrCode(qrText, QRCodeGenerator.ECCLevel.Q);
            var qrCode = new QRCode(qrData); // Wrap data into a QRCode object

            // Render the QR code graphic to a bitmap at module size 20
            using Bitmap qrBitmap = qrCode.GetGraphic(20);
            using var stream = new MemoryStream();
            qrBitmap.Save(stream, ImageFormat.Png); // Export as PNG
            stream.Seek(0, SeekOrigin.Begin); // Reset stream position for reading

            // Load the PNG from memory into a WPF-friendly BitmapImage
            var bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.StreamSource = stream;
            bitmapImage.CacheOption = BitmapCacheOption.OnLoad; // Ensure it stays valid after stream closes
            bitmapImage.EndInit();

            // Assign the generated BitmapImage to the Image control in the UI
            QrImage.Source = bitmapImage;
        }

        // Print button click: wraps the QrImage control in a Visual and sends it to the printer
        private void Print_Click(object sender, RoutedEventArgs e)
        {
            var printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == true)
            {
                // Create a visual brush of the QrImage control
                var visual = new DrawingVisual();
                using (var dc = visual.RenderOpen())
                {
                    var brush = new VisualBrush(QrImage);
                    // Draw the rectangle using the full size of the QR image
                    dc.DrawRectangle(
                        brush,
                        null,
                        new Rect(
                            new System.Windows.Point(),
                            new System.Windows.Size(QrImage.ActualWidth, QrImage.ActualHeight)
                        )
                    );
                }

                // Send the composed visual to the printer with description "QR Code"
                printDialog.PrintVisual(visual, "QR Code");
            }
        }

        // Minimize icon click: minimize this window
        private void Minimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        // Maximize icon click: toggle between maximized and normal sizes
        private void Maximize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState =
                this.WindowState == WindowState.Maximized
                ? WindowState.Normal
                : WindowState.Maximized;
        }

        // Close icon click: close this window
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        // Title bar drag: allow the user to move the window when dragging the title area
        private void TitleBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove(); // Initiate window drag
            }
        }
    }
}
