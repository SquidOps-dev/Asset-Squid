// QRCodeView.xaml.cs
// This window generates a QR code for a given Device and prints it on a Brother PT-P710BT using a b-PAC template.

using bpac;                         // Brother b-PAC SDK interop
using QRCoder;                      // QR code generation library
using SquidOps_AssetSquid.Models;   // Device model definitions
using System;
using System.Drawing;               // For System.Drawing.Bitmap
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace SquidOps_AssetSquid.Views
{
    public partial class QRCodeView : Window
    {
        // Holds the generated QR code as a WPF BitmapImage for preview
        private readonly BitmapImage _qrPreview;

        /// <summary>
        /// Constructs the QR code view and generates an on-screen preview from the Device data.
        /// </summary>
        public QRCodeView(Device device)
        {
            InitializeComponent();  // Load XAML components

            // Build the text payload for the QR code from device properties
            var qrText =
                $"Name: {device.Name}\n" +
                $"IP: {device.IpAddress}\n" +
                $"SN: {device.SerialNumber}\n" +
                $"MAC: {device.MacAddress}\n" +
                $"Model: {device.DeviceModel}";

            // Generate a small bitmap with 8px per QR module for on-screen display
            using var qrGen = new QRCodeGenerator();
            using var qrData = qrGen.CreateQrCode(qrText, QRCodeGenerator.ECCLevel.Q);
            using var qrCode = new QRCode(qrData);
            using var bmp = qrCode.GetGraphic(8);

            // Convert the System.Drawing.Bitmap to a BitmapImage and show it
            _qrPreview = ToBitmapImage(bmp);
            QrImage.Source = _qrPreview;
        }

        /// <summary>
        /// Converts a System.Drawing.Bitmap into a WPF BitmapImage.
        /// </summary>
        private static BitmapImage ToBitmapImage(System.Drawing.Bitmap bmp)
        {
            using var ms = new MemoryStream();
            bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            ms.Seek(0, SeekOrigin.Begin);

            var bi = new BitmapImage();
            bi.BeginInit();
            bi.CacheOption = BitmapCacheOption.OnLoad;  // Load the stream immediately
            bi.StreamSource = ms;
            bi.EndInit();
            bi.Freeze();    // Make it cross-thread safe
            return bi;
        }

        /// <summary>
        /// Converts a WPF BitmapImage into a System.Drawing.Bitmap.
        /// Used to save the preview as a PNG for b-PAC.
        /// </summary>
        private static System.Drawing.Bitmap BitmapImageToDrawingBitmap(BitmapImage bmpImg)
        {
            using var ms = new MemoryStream();
            var encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bmpImg));
            encoder.Save(ms);
            ms.Seek(0, SeekOrigin.Begin);
            return new System.Drawing.Bitmap(ms);
        }

        /// <summary>
        /// Print the QR code to Brother PT-P710BT using b-PAC SDK.
        /// Requires a template file (e.g. Image1.lbx) with an image object named "Image1".
        /// </summary>
        private void Print_Click(object sender, RoutedEventArgs e)
        {
            // Generate & save QR to temp PNG
            var qrBmp = BitmapImageToDrawingBitmap(_qrPreview);
            var tmpPath = Path.Combine(Path.GetTempPath(), "qr_print.png");
            qrBmp.Save(tmpPath, System.Drawing.Imaging.ImageFormat.Png);

            // Determine the path to the LBX template (must reside alongside the EXE)
            var TEMPLATE_DIRECTORY = AppDomain.CurrentDomain.BaseDirectory;
            var TEMPLATE_NAME = "Image1.lbx";
            string templatePath = Path.Combine(TEMPLATE_DIRECTORY, TEMPLATE_NAME);

            // Attempt to open the template file using b-PAC
            var doc = new DocumentClass();
            if (!doc.Open(templatePath))
            {
                MessageBox.Show($"Could not open template: {templatePath}\nError: {doc.ErrorCode}",
                                "Print Error", MessageBoxButton.OK, MessageBoxImage.Error);

                // Abort if template load fails
                return;
            }

            // Locate the placeholder object named "QR" in the template
            var imgObj = doc.GetObject("QR");
            if (imgObj == null)
            {
                MessageBox.Show("Template missing an object named 'QR'.",   // Bail if no placeholder
                                "Print Error", MessageBoxButton.OK, MessageBoxImage.Error);
                doc.Close();
                return;
            }
            // Replace the placeholder image with our dynamic QR PNG (kind=0 for image, param=1 to maintain aspect)
            imgObj.SetData(0, tmpPath, 1);

            // Point at the PT-P710BT and autofit to media 
            doc.SetPrinter("PT-P710BT", true);

            // Print one copy with auto-cut enabled
            doc.StartPrint("", PrintOptionConstants.bpoAutoCut);
            doc.PrintOut(1, PrintOptionConstants.bpoDefault);
            doc.EndPrint();

            // 6) Close the document and release COM resources
            doc.Close();
        }

        // --- Custom Title Bar handlers: implemented for window dragging and control buttons ---

        /// <summary> Allow dragging the window by its custom title bar panel. </summary>
        private void TitleBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ButtonState == MouseButtonState.Pressed)
                DragMove();
        }

        /// <summary> Minimizes the window. </summary>
        private void Minimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }


        /// <summary> Toggles between maximized and normal window states. </summary>
        private void Maximize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = (WindowState == WindowState.Maximized)
                          ? WindowState.Normal
                          : WindowState.Maximized;
        }

        /// <summary> Closes the window. </summary>
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
