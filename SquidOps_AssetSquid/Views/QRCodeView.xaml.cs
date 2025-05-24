using QRCoder;
using SquidOps_AssetSquid.Models;
using System;
using System.Printing;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Documents;

namespace SquidOps_AssetSquid.Views
{
    public partial class QRCodeView : Window
    {
        public QRCodeView(Device device)
        {
            InitializeComponent();

            string qrText = $"Name: {device.Name}\nIP: {device.IpAddress}\nSN: {device.SerialNumber}\nMAC: {device.MacAddress}\nModel: {device.DeviceModel}";

            using var qrGenerator = new QRCodeGenerator();
            using var qrData = qrGenerator.CreateQrCode(qrText, QRCodeGenerator.ECCLevel.Q);
            var qrCode = new QRCode(qrData);

            using Bitmap qrBitmap = qrCode.GetGraphic(20);
            using var stream = new MemoryStream();
            qrBitmap.Save(stream, ImageFormat.Png);
            stream.Seek(0, SeekOrigin.Begin);

            var bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.StreamSource = stream;
            bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
            bitmapImage.EndInit();

            QrImage.Source = bitmapImage;
        }

        private void Print_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog printDialog = new PrintDialog();

            if (printDialog.ShowDialog() == true)
            {
                // Create a VisualBrush of the QR code image
                DrawingVisual visual = new DrawingVisual();
                using (DrawingContext dc = visual.RenderOpen())
                {
                    VisualBrush brush = new VisualBrush(QrImage);
                    dc.DrawRectangle(brush, null, new Rect(new System.Windows.Point(), new System.Windows.Size(QrImage.ActualWidth, QrImage.ActualHeight)));
                }

                // Print the visual
                printDialog.PrintVisual(visual, "QR Code");
            }
        }

        private void Minimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Maximize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = (this.WindowState == WindowState.Maximized) ? WindowState.Normal : WindowState.Maximized;
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void TitleBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

    }
}
