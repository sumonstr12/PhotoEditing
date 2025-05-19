using System;
using System.Windows;
using System.Windows.Media.Imaging;

namespace PhotoEditor_WPF_
{
    public partial class FilterWindow : Window
    {
        private WriteableBitmap originalImage;
        public WriteableBitmap SelectedImage { get; private set; }

        public FilterWindow(WriteableBitmap image)
        {
            InitializeComponent();
            originalImage = image;
            SelectedImage = new WriteableBitmap(image); // Create a copy
            ImagePreview.Source = SelectedImage; // Show initial image
        }

        private void ApplyFilter(Func<byte[], byte[]> filterAction)
        {
            WriteableBitmap newBitmap = new WriteableBitmap(originalImage);
            int width = newBitmap.PixelWidth;
            int height = newBitmap.PixelHeight;
            int stride = width * ((newBitmap.Format.BitsPerPixel + 7) / 8);
            byte[] pixelData = new byte[height * stride];
            newBitmap.CopyPixels(pixelData, stride, 0);

            pixelData = filterAction(pixelData);

            newBitmap.WritePixels(new Int32Rect(0, 0, width, height), pixelData, stride, 0);
            SelectedImage = newBitmap;
            ImagePreview.Source = SelectedImage;
        }

        private byte[] ApplyGrayscale(byte[] pixels)
        {
            for (int i = 0; i < pixels.Length; i += 4)
            {
                byte blue = pixels[i];
                byte green = pixels[i + 1];
                byte red = pixels[i + 2];
                byte gray = (byte)(0.299 * red + 0.587 * green + 0.114 * blue);
                pixels[i] = gray;     // Blue
                pixels[i + 1] = gray; // Green
                pixels[i + 2] = gray; // Red
            }
            return pixels;
        }

        private byte[] ApplySepia(byte[] pixels)
        {
            for (int i = 0; i < pixels.Length; i += 4)
            {
                byte blue = pixels[i];
                byte green = pixels[i + 1];
                byte red = pixels[i + 2];

                int newRed = (int)(red * 0.393 + green * 0.769 + blue * 0.189);
                int newGreen = (int)(red * 0.349 + green * 0.686 + blue * 0.168);
                int newBlue = (int)(red * 0.272 + green * 0.534 + blue * 0.131);

                pixels[i] = (byte)Math.Min(255, newBlue);   // Blue
                pixels[i + 1] = (byte)Math.Min(255, newGreen); // Green
                pixels[i + 2] = (byte)Math.Min(255, newRed);   // Red
            }
            return pixels;
        }

        private byte[] ApplyInvert(byte[] pixels)
        {
            for (int i = 0; i < pixels.Length; i += 4)
            {
                pixels[i] = (byte)(255 - pixels[i]);         // Blue
                pixels[i + 1] = (byte)(255 - pixels[i + 1]); // Green
                pixels[i + 2] = (byte)(255 - pixels[i + 2]); // Red
            }
            return pixels;
        }

        private byte[] ApplyCyberpunk(byte[] pixels)
        {
            for (int i = 0; i < pixels.Length; i += 4)
            {
                byte b = pixels[i];
                byte g = pixels[i + 1];
                byte r = pixels[i + 2];
                byte a = pixels[i + 3];

                // Add purple-pink tint & boost contrast
                byte newR = (byte)Math.Min(255, r + 50);
                byte newG = (byte)Math.Max(0, g - 30);
                byte newB = (byte)Math.Min(255, b + 60);

                pixels[i] = newB;
                pixels[i + 1] = newG;
                pixels[i + 2] = newR;
                pixels[i + 3] = a;
            }
            return pixels;
        }

        private byte[] ApplyRetro(byte[] pixels)
        {
            for (int i = 0; i < pixels.Length; i += 4)
            {
                byte b = pixels[i];
                byte g = pixels[i + 1];
                byte r = pixels[i + 2];
                byte a = pixels[i + 3];

                byte faded = (byte)((r + g + b) / 3);
                pixels[i] = (byte)Math.Min(255, faded - 20);  // Blue
                pixels[i + 1] = (byte)Math.Min(255, faded + 15);  // Green
                pixels[i + 2] = (byte)Math.Min(255, faded + 30);  // Red
                pixels[i + 3] = a;
            }
            return pixels;
        }

        private byte[] ApplyPolaroid(byte[] pixels)
        {
            for (int i = 0; i < pixels.Length; i += 4)
            {
                pixels[i] = (byte)Math.Min(255, pixels[i] + 10);       // Blue
                pixels[i + 1] = (byte)Math.Min(255, pixels[i + 1] + 5); // Green
                pixels[i + 2] = (byte)Math.Min(255, pixels[i + 2] + 25);// Red
            }
            return pixels;
        }

        private byte[] ApplyComic(byte[] pixels)
        {
            for (int i = 0; i < pixels.Length; i += 4)
            {
                byte gray = (byte)((pixels[i] + pixels[i + 1] + pixels[i + 2]) / 3);
                byte contrast = (gray > 128) ? (byte)255 : (byte)0;
                pixels[i] = pixels[i + 1] = pixels[i + 2] = contrast;
            }
            return pixels;
        }

        private void RestoreOriginal()
        {
            SelectedImage = new WriteableBitmap(originalImage); // Reset to original
            ImagePreview.Source = SelectedImage;
        }

        private void OriginalButton_Click(object sender, RoutedEventArgs e)
        {
            RestoreOriginal();
        }




        /// add click 

        private void GrayscaleButton_Click(object sender, RoutedEventArgs e)
        {
            ApplyFilter(ApplyGrayscale);
        }

        private void SepiaButton_Click(object sender, RoutedEventArgs e)
        {
            ApplyFilter(ApplySepia);
        }

        private void InvertButton_Click(object sender, RoutedEventArgs e)
        {
            ApplyFilter(ApplyInvert);
        }

        private void CyberpunkButton_Click(object sender, RoutedEventArgs e)
        {
            ApplyFilter(ApplyCyberpunk);
        }
        private void RetroButton_Click(object sender, RoutedEventArgs e)
        {
            ApplyFilter(ApplyRetro);
        }
        private void PolaroidButton_Click(object sender, RoutedEventArgs e)
        {
            ApplyFilter(ApplyPolaroid);
        }
        private void ComicButton_Click(object sender, RoutedEventArgs e)
        {
            ApplyFilter(ApplyComic);
        }


        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}