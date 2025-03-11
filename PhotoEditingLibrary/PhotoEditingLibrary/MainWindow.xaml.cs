using System;
using System.Windows;
using Microsoft.Win32;
using System.Drawing;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;

namespace PhotoEditingLibraryApp
{
    public partial class MainWindow : Window
    {
        private Bitmap currentImage;

        public MainWindow()
        {
            InitializeComponent();
        }

        // Open Image from File
        private void OpenImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                currentImage = new Bitmap(openFileDialog.FileName);
                ImageDisplay.Source = ConvertToBitmapImage(currentImage);
                AnimateVisibility(EditButton, true); // Show Edit Button with Animation
            }
        }

        // Open Camera (Future Implementation)
        private void OpenCamera_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Camera feature will be added!");
        }

        // Show Editing Options with Smooth Transition
        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            AnimateVisibility(EditingPanel, true);
            AnimateVisibility(EditButton, false);
            AnimateVisibility(UploadPanel, false);
        }

        // Go Back to Main Screen
        private void GoBack_Click(object sender, RoutedEventArgs e)
        {
            AnimateVisibility(EditingPanel, false);
            AnimateVisibility(EditButton, true);
            AnimateVisibility(UploadPanel, true);
        }

        // Editing Functions (To Be Implemented)
        private void ApplyFilter_Click(object sender, RoutedEventArgs e) => MessageBox.Show("Filter will be applied!");
        private void ApplyMakeup_Click(object sender, RoutedEventArgs e) => MessageBox.Show("Makeup feature coming soon!");
        private void ChangeBackground_Click(object sender, RoutedEventArgs e) => MessageBox.Show("Background Change will be implemented!");
        private void AddText_Click(object sender, RoutedEventArgs e) => MessageBox.Show("Text feature will be added!");
        private void ApplyTemplate_Click(object sender, RoutedEventArgs e) => MessageBox.Show("Template feature will be added!");

        // Smooth Animation for Visibility Toggle
        private void AnimateVisibility(UIElement element, bool show)
        {
            DoubleAnimation animation = new DoubleAnimation
            {
                To = show ? 1 : 0,
                Duration = TimeSpan.FromMilliseconds(300),
                AutoReverse = false
            };
            element.BeginAnimation(UIElement.OpacityProperty, animation);
            element.Visibility = show ? Visibility.Visible : Visibility.Collapsed;
        }

        // Convert Bitmap to BitmapImage for WPF Display
        private BitmapImage ConvertToBitmapImage(Bitmap bitmap)
        {
            using (var memoryStream = new System.IO.MemoryStream())
            {
                bitmap.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);
                memoryStream.Position = 0;
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memoryStream;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
                return bitmapImage;
            }
        }

        private void ApplyGrayscale_Click(object sender, RoutedEventArgs e)
        {
            if (currentImage != null)
            {
                // Apply grayscale filter from your custom library
                currentImage = ImageProcessingLibrary.ConvertToGrayscale(currentImage);
                ImageDisplay.Source = ConvertToBitmapImage(currentImage);  // Update the image on UI
            }
        }

        private void ApplySepia_Click(object sender, RoutedEventArgs e)
        {
            if (currentImage != null)
            {
                // Apply sepia filter from your custom library
                currentImage = ImageProcessingLibrary.ApplySepiaFilter(currentImage);
                ImageDisplay.Source = ConvertToBitmapImage(currentImage);  // Update the image on UI
            }
        }

        private void AdjustBrightness_Click(object sender, RoutedEventArgs e)
        {
            if (currentImage != null)
            {
                // Apply brightness adjustment from your custom library
                currentImage = ImageProcessingLibrary.AdjustBrightness(currentImage, 1.2f);  // Example with 1.2 brightness factor
                ImageDisplay.Source = ConvertToBitmapImage(currentImage);  // Update the image on UI
            }
        }


        private void ShowFilterOptions_Click(object sender, RoutedEventArgs e)
        {
            // Hide all editing options except for the filter panel
            EditingPanel.Visibility = Visibility.Collapsed;
            FilterPanel.Visibility = Visibility.Visible;
        }

        private void GoBackToEdit_Click(object sender, RoutedEventArgs e)
        {
            // Hide the filter panel and show the main editing options again
            FilterPanel.Visibility = Visibility.Collapsed;
            EditingPanel.Visibility = Visibility.Visible;
        }

    }
}
