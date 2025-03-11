using System;
using System.Drawing;

namespace PhotoEditingLibraryApp
{
    public static class ImageProcessingLibrary
    {
        // Grayscale filter
        public static Bitmap ConvertToGrayscale(Bitmap original)
        {
            Bitmap grayscaleBitmap = new Bitmap(original.Width, original.Height);
            for (int x = 0; x < original.Width; x++)
            {
                for (int y = 0; y < original.Height; y++)
                {
                    Color pixelColor = original.GetPixel(x, y);
                    int grayValue = (int)(pixelColor.R * 0.3 + pixelColor.G * 0.59 + pixelColor.B * 0.11);
                    Color grayColor = Color.FromArgb(pixelColor.A, grayValue, grayValue, grayValue);
                    grayscaleBitmap.SetPixel(x, y, grayColor);
                }
            }
            return grayscaleBitmap;
        }

        // Sepia filter
        public static Bitmap ApplySepiaFilter(Bitmap original)
        {
            Bitmap sepiaBitmap = new Bitmap(original.Width, original.Height);
            for (int x = 0; x < original.Width; x++)
            {
                for (int y = 0; y < original.Height; y++)
                {
                    Color pixelColor = original.GetPixel(x, y);
                    int tr = (int)(0.393 * pixelColor.R + 0.769 * pixelColor.G + 0.189 * pixelColor.B);
                    int tg = (int)(0.349 * pixelColor.R + 0.686 * pixelColor.G + 0.168 * pixelColor.B);
                    int tb = (int)(0.272 * pixelColor.R + 0.534 * pixelColor.G + 0.131 * pixelColor.B);

                    // Ensure values are within the range of 0-255
                    tr = Math.Min(255, tr);
                    tg = Math.Min(255, tg);
                    tb = Math.Min(255, tb);

                    Color sepiaColor = Color.FromArgb(pixelColor.A, tr, tg, tb);
                    sepiaBitmap.SetPixel(x, y, sepiaColor);
                }
            }
            return sepiaBitmap;
        }

        // Optional: Brightness filter (adjust brightness based on a factor)
        public static Bitmap AdjustBrightness(Bitmap image, float brightnessFactor)
        {
            Bitmap adjustedBitmap = new Bitmap(image.Width, image.Height);
            for (int x = 0; x < image.Width; x++)
            {
                for (int y = 0; y < image.Height; y++)
                {
                    Color pixelColor = image.GetPixel(x, y);
                    int r = (int)(pixelColor.R * brightnessFactor);
                    int g = (int)(pixelColor.G * brightnessFactor);
                    int b = (int)(pixelColor.B * brightnessFactor);

                    // Ensure the RGB values are within the valid range
                    r = Math.Min(255, Math.Max(0, r));
                    g = Math.Min(255, Math.Max(0, g));
                    b = Math.Min(255, Math.Max(0, b));

                    Color newColor = Color.FromArgb(pixelColor.A, r, g, b);
                    adjustedBitmap.SetPixel(x, y, newColor);
                }
            }
            return adjustedBitmap;
        }
    }
}
