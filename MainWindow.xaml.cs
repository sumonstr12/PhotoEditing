using Microsoft.Win32;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using static System.Net.Mime.MediaTypeNames;
using Color = System.Windows.Media.Color;
using System.Windows.Media;

namespace PhotoEditor_WPF_
{
    public partial class MainWindow : Window
    {


        // For dragging canvas
        private bool isImageDragging = false;
        private Point imageDragStartPoint;
        private Point imageStartPosition;

        private string currentFileName;


        // For Cropping
        private Point startPoint;
        private bool isDragging;
        private bool isSelecting = false;

        // For Undo/Redo
        private Stack<BitmapSource> undoStack = new Stack<BitmapSource>();
        private Stack<BitmapSource> redoStack = new Stack<BitmapSource>();

        private WriteableBitmap _image;
        private BitmapImage currentImage;


        public ICommand OpenCommand { get; }
        public ICommand CropCommand { get; }
        public ICommand SaveCommand { get; }
        public ICommand UndoCommand { get; }
        public ICommand RedoCommand { get; }
        public ICommand ExitCommand { get; }
        public ICommand AboutCommand { get; }

        public MainWindow()
        {
            InitializeComponent();

            // Initialize commands
            OpenCommand = new RoutedCommand();
            CropCommand = new RoutedCommand();
            SaveCommand = new RoutedCommand();
            UndoCommand = new RoutedCommand();
            RedoCommand = new RoutedCommand();
            ExitCommand = new RoutedCommand();
            AboutCommand = new RoutedCommand();

            // Bind commands to handlers
            CommandBindings.Add(new CommandBinding(OpenCommand, Open_Executed));
            CommandBindings.Add(new CommandBinding(CropCommand, Crop_Executed));
            CommandBindings.Add(new CommandBinding(SaveCommand, Save_Executed));
            CommandBindings.Add(new CommandBinding(UndoCommand, Undo_Executed));
            CommandBindings.Add(new CommandBinding(RedoCommand, Redo_Executed));
            CommandBindings.Add(new CommandBinding(ExitCommand, Exit_Executed));
            CommandBindings.Add(new CommandBinding(AboutCommand, About_Executed));


            // Upadated Crop code
            SelectionCanvas.SizeChanged += (s, e) => UpdateCanvasSize();


            // Code for Dragging canvas
            // Add mouse event handlers for dragging the image
            ImageWorkspace.MouseDown += ImageWorkspace_MouseDown;
            ImageWorkspace.MouseMove += ImageWorkspace_MouseMove;
            ImageWorkspace.MouseUp += ImageWorkspace_MouseUp;



            // Set the DataContext to this window for command bindings
            DataContext = this;
        }


        // Updated Crop code
        private void UpdateCanvasSize()
        {
            var parent = SelectionCanvas.Parent as FrameworkElement;
            if (parent != null)
            {
                SelectionCanvas.Width = parent.ActualWidth;
                SelectionCanvas.Height = parent.ActualHeight;
            }
        }

        public class ImageState
        {
            public WriteableBitmap Image { get; set; }
            public string Operation { get; set; }
        }




        // Code Dragging canvas
        private void ImageWorkspace_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // Only allow dragging if not in crop selection mode
            if (e.LeftButton == MouseButtonState.Pressed && !isSelecting)
            {
                isImageDragging = true;
                imageDragStartPoint = e.GetPosition(SelectionCanvas); // Get position relative to canvas
                imageStartPosition = new Point(Canvas.GetLeft(ImageWorkspace), Canvas.GetTop(ImageWorkspace));
                ImageWorkspace.CaptureMouse(); // Capture mouse to ensure we get move events
            }
        }

        private void ImageWorkspace_MouseMove(object sender, MouseEventArgs e)
        {
            if (isImageDragging)
            {
                Point currentPoint = e.GetPosition(SelectionCanvas);

                // Calculate the offset, accounting for zoom
                double zoomScale = zoomFactor;
                double deltaX = (currentPoint.X - imageDragStartPoint.X) / zoomScale;
                double deltaY = (currentPoint.Y - imageDragStartPoint.Y) / zoomScale;

                // Calculate new position
                double newLeft = imageStartPosition.X + deltaX;
                double newTop = imageStartPosition.Y + deltaY;

                // Get canvas and image dimensions
                double canvasWidth = SelectionCanvas.ActualWidth;
                double canvasHeight = SelectionCanvas.ActualHeight;
                double imageWidth = ImageWorkspace.ActualWidth * zoomScale;
                double imageHeight = ImageWorkspace.ActualHeight * zoomScale;

                // Restrict position to keep image within canvas bounds
                newLeft = Math.Max(0, Math.Min(newLeft, canvasWidth - imageWidth));
                newTop = Math.Max(0, Math.Min(newTop, canvasHeight - imageHeight));

                // Update the image position
                Canvas.SetLeft(ImageWorkspace, newLeft);
                Canvas.SetTop(ImageWorkspace, newTop);
            }
        }

        private void ImageWorkspace_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (isImageDragging)
            {
                isImageDragging = false;
                ImageWorkspace.ReleaseMouseCapture(); // Release mouse capture
            }
        }







        private void Open_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                // Store the file name (without path or extension) for use in Save_Executed
                currentFileName = System.IO.Path.GetFileNameWithoutExtension(openFileDialog.FileName);

                using (FileStream stream = new FileStream(openFileDialog.FileName, FileMode.Open, FileAccess.Read))
                {
                    BitmapDecoder decoder = BitmapDecoder.Create(stream, BitmapCreateOptions.None, BitmapCacheOption.OnLoad);
                    BitmapSource bitmapSource = decoder.Frames[0];

                    originalBitmap = new WriteableBitmap(bitmapSource); // Save the original
                    ImageWorkspace.Source = new WriteableBitmap(originalBitmap); // Show a copy

                    // Get canvas and image dimensions
                    double canvasWidth = SelectionCanvas.ActualWidth;
                    double canvasHeight = SelectionCanvas.ActualHeight;
                    double imageWidth = bitmapSource.PixelWidth;
                    double imageHeight = bitmapSource.PixelHeight;

                    // Calculate scaling factor to fit image within canvas
                    double scaleX = canvasWidth / imageWidth;
                    double scaleY = canvasHeight / imageHeight;
                    zoomFactor = Math.Min(scaleX, scaleY); // Use the smaller scale to preserve aspect ratio

                    // If image is smaller than canvas, use original size (zoomFactor = 1)
                    if (imageWidth <= canvasWidth && imageHeight <= canvasHeight)
                    {
                        zoomFactor = 1.0;
                    }

                    // Apply zoom to fit within canvas
                    ApplyZoom();

                    // Center the scaled image in the canvas
                    double scaledWidth = imageWidth * zoomFactor;
                    double scaledHeight = imageHeight * zoomFactor;
                    Canvas.SetLeft(ImageWorkspace, (canvasWidth - scaledWidth) / 2);
                    Canvas.SetTop(ImageWorkspace, (canvasHeight - scaledHeight) / 2);
                }

                // Push the newly opened image to undoStack
                undoStack.Clear(); // New image loaded, so clear any previous undo history
                redoStack.Clear(); // Clear redo history too
                undoStack.Push(CloneImage(originalBitmap)); // Save current image to undo stack
            }
        }

        private BitmapSource CloneImage(BitmapSource source)
        {
            if (source == null) return null;

            return new WriteableBitmap(source); // Deep copy
        }


        private void Crop_Executed(object sender, RoutedEventArgs e)
        {
            if (!isSelecting)
            {
                // Start selection mode
                isSelecting = true;
                SelectionRectangle.Visibility = Visibility.Visible;
                SelectionCanvas.MouseDown += Canvas_MouseLeftButtonDown;
                SelectionCanvas.MouseMove += Canvas_MouseMove;
                SelectionCanvas.MouseUp += Canvas_MouseLeftButtonUp;
            }
            else
            {
                // Apply crop
                isSelecting = false;
                if (ImageWorkspace.Source is BitmapSource bitmapSource && SelectionRectangle.Visibility == Visibility.Visible)
                {
                    // Get zoom scale
                    double zoomScale = 1.0;
                    if (ImageWorkspace.RenderTransform is ScaleTransform scaleTransform)
                    {
                        zoomScale = scaleTransform.ScaleX; // Assume uniform scaling
                    }

                    // Calculate image-to-canvas scaling (pixels to control size)
                    double scaleX = bitmapSource.PixelWidth / ImageWorkspace.ActualWidth;
                    double scaleY = bitmapSource.PixelHeight / ImageWorkspace.ActualHeight;

                    // Get selection rectangle coordinates (canvas space)
                    double canvasLeft = Canvas.GetLeft(SelectionRectangle);
                    double canvasTop = Canvas.GetTop(SelectionRectangle);
                    double canvasWidth = SelectionRectangle.Width;
                    double canvasHeight = SelectionRectangle.Height;

                    // Convert to image pixel coordinates, accounting for zoom
                    int x = (int)((canvasLeft / zoomScale) * scaleX);
                    int y = (int)((canvasTop / zoomScale) * scaleY);
                    int width = (int)((canvasWidth / zoomScale) * scaleX);
                    int height = (int)((canvasHeight / zoomScale) * scaleY);

                    // Boundary checks
                    x = Math.Max(0, Math.Min(x, bitmapSource.PixelWidth - 1));
                    y = Math.Max(0, Math.Min(y, bitmapSource.PixelHeight - 1));
                    width = Math.Min(width, bitmapSource.PixelWidth - x);
                    height = Math.Min(height, bitmapSource.PixelHeight - y);

                    // Debug output to verify coordinates
                    Console.WriteLine($"Image: {bitmapSource.PixelWidth}x{bitmapSource.PixelHeight}");
                    Console.WriteLine($"Control: {ImageWorkspace.ActualWidth}x{ImageWorkspace.ActualHeight}");
                    Console.WriteLine($"Zoom: {zoomScale}");
                    Console.WriteLine($"Selection (Canvas): Left={canvasLeft}, Top={canvasTop}, Width={canvasWidth}, Height={canvasHeight}");
                    Console.WriteLine($"Crop (Pixels): X={x}, Y={y}, Width={width}, Height={height}");

                    if (width > 0 && height > 0)
                    {
                        undoStack.Push(bitmapSource.Clone());
                        redoStack.Clear();

                        try
                        {
                            CroppedBitmap croppedBitmap = new CroppedBitmap(bitmapSource, new Int32Rect(x, y, width, height));
                            ImageWorkspace.Source = croppedBitmap;

                            // Reset zoom and selection
                            ImageWorkspace.RenderTransform = new ScaleTransform(1, 1);
                            SelectionRectangle.Width = 0;
                            SelectionRectangle.Height = 0;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Crop failed: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Invalid crop area. Please select a valid region.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }

                    SelectionRectangle.Visibility = Visibility.Collapsed;
                }
                else
                {
                    MessageBox.Show("No image loaded or no selection made.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                }

                // Remove event handlers
                SelectionCanvas.MouseDown -= Canvas_MouseLeftButtonDown;
                SelectionCanvas.MouseMove -= Canvas_MouseMove;
                SelectionCanvas.MouseUp -= Canvas_MouseLeftButtonUp;
            }
        }

        private void Canvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            startPoint = e.GetPosition(SelectionCanvas);
            Canvas.SetLeft(SelectionRectangle, startPoint.X);
            Canvas.SetTop(SelectionRectangle, startPoint.Y);
            SelectionRectangle.Width = 0;
            SelectionRectangle.Height = 0;
            SelectionRectangle.Visibility = Visibility.Visible;
        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Point currentPoint = e.GetPosition(SelectionCanvas);
                double width = currentPoint.X - startPoint.X;
                double height = currentPoint.Y - startPoint.Y;

                // Constrain to image bounds
                double maxWidth = ImageWorkspace.ActualWidth;
                double maxHeight = ImageWorkspace.ActualHeight;

                if (width >= 0)
                {
                    Canvas.SetLeft(SelectionRectangle, startPoint.X);
                    SelectionRectangle.Width = Math.Min(width, maxWidth - startPoint.X);
                }
                else
                {
                    Canvas.SetLeft(SelectionRectangle, Math.Max(currentPoint.X, 0));
                    SelectionRectangle.Width = Math.Min(-width, startPoint.X);
                }

                if (height >= 0)
                {
                    Canvas.SetTop(SelectionRectangle, startPoint.Y);
                    SelectionRectangle.Height = Math.Min(height, maxHeight - startPoint.Y);
                }
                else
                {
                    Canvas.SetTop(SelectionRectangle, Math.Max(currentPoint.Y, 0));
                    SelectionRectangle.Height = Math.Min(-height, startPoint.Y);
                }
            }
        }

        private void Canvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            // No action needed; selection is complete

        }



        private void Save_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (ImageWorkspace.Source is BitmapSource bitmapSource)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "PNG Image|*.png|JPEG Image|*.jpg;*.jpeg|Bitmap Image|*.bmp",
                    // Use currentFileName if available, otherwise fallback to "EditedImage"
                    FileName = string.IsNullOrEmpty(currentFileName) ? "EditedImage" : currentFileName
                };

                if (saveFileDialog.ShowDialog() == true)
                {
                    BitmapEncoder encoder;

                    string extension = System.IO.Path.GetExtension(saveFileDialog.FileName).ToLower();
                    switch (extension)
                    {
                        case ".jpg":
                        case ".jpeg":
                            encoder = new JpegBitmapEncoder();
                            break;
                        case ".bmp":
                            encoder = new BmpBitmapEncoder();
                            break;
                        default:
                            encoder = new PngBitmapEncoder();
                            break;
                    }

                    encoder.Frames.Add(BitmapFrame.Create(bitmapSource));

                    using (var fileStream = new System.IO.FileStream(saveFileDialog.FileName, System.IO.FileMode.Create))
                    {
                        encoder.Save(fileStream);
                    }

                    MessageBox.Show("Image saved successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                MessageBox.Show("No image to save!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }


        private void Undo_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (undoStack.Count > 1)
            {
                BitmapSource current = (BitmapSource)ImageWorkspace.Source;
                redoStack.Push(current); // Save current in redo
                undoStack.Pop(); // Remove current
                ImageWorkspace.Source = undoStack.Peek(); // Restore previous
            }
            else
            {
                MessageBox.Show("Nothing to undo.", "Info");
            }
        }


        private void Redo_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (redoStack.Count > 0)
            {
                BitmapSource next = redoStack.Pop();
                undoStack.Push(next);
                ImageWorkspace.Source = next;
            }
            else
            {
                MessageBox.Show("Nothing to redo.", "Info");
            }
        }





        // Event handler for the Brightness Slider
        private WriteableBitmap originalBitmap;
        private object bitmapSource;


        private void LoadImage(BitmapSource bitmapSource)
        {
            originalBitmap = new WriteableBitmap(bitmapSource);
            ImageWorkspace.Source = originalBitmap;
        }

        private void BrightnessToggleButton_Click(object sender, RoutedEventArgs e)
        {
            if (BrightnessSlider.Visibility == Visibility.Visible)
            {
                BrightnessSlider.Visibility = Visibility.Collapsed;
            }
            else
            {
                BrightnessSlider.Visibility = Visibility.Visible;
            }
        }



        private void BrightnessSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (originalBitmap != null)
            {
                WriteableBitmap adjusted = new WriteableBitmap(originalBitmap);
                ApplyBrightness(adjusted, BrightnessSlider.Value);
                ImageWorkspace.Source = adjusted;
            }
        }


        private void ApplyBrightness(WriteableBitmap bitmap, double brightnessFactor)
        {
            int width = bitmap.PixelWidth;
            int height = bitmap.PixelHeight;
            int stride = width * ((bitmap.Format.BitsPerPixel + 7) / 8);
            byte[] pixelData = new byte[height * stride];
            bitmap.CopyPixels(pixelData, stride, 0);

            int brightnessOffset = (int)((brightnessFactor - 1.0) * 100);

            for (int i = 0; i < pixelData.Length; i += 4)
            {
                pixelData[i] = ClampToByte(pixelData[i] + brightnessOffset); // Blue
                pixelData[i + 1] = ClampToByte(pixelData[i + 1] + brightnessOffset); // Green
                pixelData[i + 2] = ClampToByte(pixelData[i + 2] + brightnessOffset); // Red
            }

            bitmap.WritePixels(new Int32Rect(0, 0, width, height), pixelData, stride, 0);
        }

        private byte ClampToByte(int value)
        {
            return (byte)Math.Max(0, Math.Min(255, value));
        }


        // code for FilterButton
        private void FilterButton_Click(object sender, RoutedEventArgs e)
        {
            if (ImageWorkspace.Source is BitmapSource bitmapSource)
            {
                WriteableBitmap writable = new WriteableBitmap(bitmapSource);
                FilterWindow filterWindow = new FilterWindow(writable);
                if (filterWindow.ShowDialog() == true && filterWindow.SelectedImage != null)
                {
                    undoStack.Push(CloneImage(bitmapSource));
                    redoStack.Clear();
                    ImageWorkspace.Source = filterWindow.SelectedImage;
                }
            }
            else
            {
                MessageBox.Show("No image loaded in the workspace!", "Error");
            }
        }


        //zoom in | zoom out


        private double zoomFactor = 1.0; // Initial zoom factor (100% zoom)

        private void ZoomIn()
        {
            zoomFactor *= 1.1; // Increase the zoom factor by 10%
            ApplyZoom();
        }

        private void ZoomOut()
        {
            zoomFactor /= 1.1; // Decrease the zoom factor by 10%
            ApplyZoom();
        }

        private void ApplyZoom()
        {
            // Apply the zoom factor using a ScaleTransform
            ScaleTransform scale = new ScaleTransform(zoomFactor, zoomFactor);
            ImageWorkspace.RenderTransform = scale;
        }


        private void ZoomInButton_Click(object sender, RoutedEventArgs e)
        {
            ZoomIn();
        }

        private void ZoomOutButton_Click(object sender, RoutedEventArgs e)
        {
            ZoomOut();
        }

        private void SelectionCanvas_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (e.Delta > 0)
            {
                ZoomIn(); // Zoom in
            }
            else if (e.Delta < 0)
            {
                ZoomOut(); // Zoom out
            }
        }

        private void ResetZoom()
        {
            zoomFactor = 1.0; // Reset the zoom factor to 100%
            ApplyZoom();
        }

        private void ResetZoomButton_Click(object sender, RoutedEventArgs e)
        {
            ResetZoom();
        }







        private void Exit_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            // Close the application
            //Application.Current.Shutdown();
        }

        private void About_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            // Open the AboutWindow as a modal dialog
            AboutWindow aboutWindow = new AboutWindow();
            aboutWindow.Owner = this; // Set the owner to the main window
            aboutWindow.ShowDialog(); // Show as a modal dialog
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

        }


        // Code for Adjust color
        // Code for Adjust Colors
        private void AdjustColorsToggleButton_Click(object sender, RoutedEventArgs e)
        {
            if (ColorSlidersPanel.Visibility == Visibility.Visible)
            {
                ColorSlidersPanel.Visibility = Visibility.Collapsed;
            }
            else
            {
                ColorSlidersPanel.Visibility = Visibility.Visible;
            }
        }

        private void ColorSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (originalBitmap != null)
            {
                // Undo স্ট্যাক-এ বর্তমান ছবি সংরক্ষণ করুন
                undoStack.Push(CloneImage((BitmapSource)ImageWorkspace.Source));
                redoStack.Clear();

                WriteableBitmap adjusted = new WriteableBitmap(originalBitmap);
                ApplyColorAdjustments(adjusted, RedSlider.Value, GreenSlider.Value, BlueSlider.Value);
                ImageWorkspace.Source = adjusted;
            }
        }

        private void ApplyColorAdjustments(WriteableBitmap bitmap, double redFactor, double greenFactor, double blueFactor)
        {
            int width = bitmap.PixelWidth;
            int height = bitmap.PixelHeight;
            int stride = width * ((bitmap.Format.BitsPerPixel + 7) / 8);
            byte[] pixelData = new byte[height * stride];
            bitmap.CopyPixels(pixelData, stride, 0);

            for (int i = 0; i < pixelData.Length; i += 4)
            {
                pixelData[i] = ClampToByte((int)(pixelData[i] * blueFactor)); // Blue
                pixelData[i + 1] = ClampToByte((int)(pixelData[i + 1] * greenFactor)); // Green
                pixelData[i + 2] = ClampToByte((int)(pixelData[i + 2] * redFactor)); // Red
            }

            bitmap.WritePixels(new Int32Rect(0, 0, width, height), pixelData, stride, 0);
        }

        // Code for Constract 
        private void ContrastToggleButton_Click(object sender, RoutedEventArgs e)
        {
            if (ContrastSlider.Visibility == Visibility.Visible)
            {
                ContrastSlider.Visibility = Visibility.Collapsed;
            }
            else
            {
                ContrastSlider.Visibility = Visibility.Visible;
            }
        }

        private void ContrastSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (originalBitmap != null)
            {
                // Undo স্ট্যাক-এ বর্তমান ছবি সংরক্ষণ করুন
                undoStack.Push(CloneImage((BitmapSource)ImageWorkspace.Source));
                redoStack.Clear();

                WriteableBitmap adjusted = new WriteableBitmap(originalBitmap);
                ApplyContrast(adjusted, ContrastSlider.Value);
                ImageWorkspace.Source = adjusted;
            }
        }

        private void ApplyContrast(WriteableBitmap bitmap, double contrastFactor)
        {
            int width = bitmap.PixelWidth;
            int height = bitmap.PixelHeight;
            int stride = width * ((bitmap.Format.BitsPerPixel + 7) / 8);
            byte[] pixelData = new byte[height * stride];
            bitmap.CopyPixels(pixelData, stride, 0);

            // কনট্রাস্ট ফ্যাক্টর প্রয়োগ করা
            double factor = contrastFactor;
            double correction = (1.0 - factor) / 2.0;

            for (int i = 0; i < pixelData.Length; i += 4)
            {
                // RGB মানগুলোর জন্য কনট্রাস্ট প্রয়োগ
                pixelData[i] = ClampToByte((int)((pixelData[i] * factor) + (255 * correction))); // Blue
                pixelData[i + 1] = ClampToByte((int)((pixelData[i + 1] * factor) + (255 * correction))); // Green
                pixelData[i + 2] = ClampToByte((int)((pixelData[i + 2] * factor) + (255 * correction))); // Red
            }

            bitmap.WritePixels(new Int32Rect(0, 0, width, height), pixelData, stride, 0);
        }


        // Frame add code 

        private void ToggleFrameOptions_Click(object sender, RoutedEventArgs e)
        {
            if (FrameOptionsPanel.Visibility == Visibility.Visible)
                FrameOptionsPanel.Visibility = Visibility.Collapsed;
            else
                FrameOptionsPanel.Visibility = Visibility.Visible;
        }

        private void ApplyLeafyFrame_Click(object sender, RoutedEventArgs e)
        {
            ApplyImageFrameFromFile(@"C:\Users\pc\Desktop\PhotoEditor(WPF)\Resources\Frames\ful.png");
        }


        private void ApplyFloralFrame_Click(object sender, RoutedEventArgs e)
        {
            ApplyImageFrameFromFile(@"C:\Users\pc\Desktop\PhotoEditor(WPF)\Resources\Frames\lotapata.png");
        }

        private void ApplySkyFrame_Click(object sender, RoutedEventArgs e)
        {
            ApplyImageFrameFromFile(@"C:\Users\pc\Desktop\PhotoEditor(WPF)\Resources\Frames\sky.png");
        }

        private void ApplyLoveFrame_Click(object sender, RoutedEventArgs e)
        {
            ApplyImageFrameFromFile(@"C:\Users\Hp\Desktop\PhotoEditor(WPF)\Resources\Frames\loveframe.png");
        }
        private void ApplyDesignFrame_Click(object sender, RoutedEventArgs e)
        {
            ApplyImageFrameFromFile(@"C:\Users\Hp\Desktop\PhotoEditor(WPF)\Resources\Frames\design.png");
        }




        private void ApplyImageFrameFromFile(string filePath)
        {
            if (ImageWorkspace.Source is BitmapSource baseImage && File.Exists(filePath))
            {
                undoStack.Push(baseImage.Clone());
                redoStack.Clear();

                BitmapImage frameImage = new BitmapImage(new Uri(filePath, UriKind.Absolute));

                int width = baseImage.PixelWidth;
                int height = baseImage.PixelHeight;

                DrawingVisual visual = new DrawingVisual();
                using (DrawingContext context = visual.RenderOpen())
                {
                    context.DrawImage(baseImage, new Rect(0, 0, width, height));
                    context.DrawImage(frameImage, new Rect(0, 0, width, height));
                }

                RenderTargetBitmap result = new RenderTargetBitmap(width, height, baseImage.DpiX, baseImage.DpiY, PixelFormats.Pbgra32);
                result.Render(visual);

                ImageWorkspace.Source = result;

                undoStack.Push(result.Clone());
                redoStack.Clear();
            }
        }


        // New 15.05 code
        //rotate code

        private void RotateButton_Click(object sender, RoutedEventArgs e)
        {
            if (ImageWorkspace.Source is BitmapSource bitmapSource)
            {
                // Save current image to undo stack before rotation
                undoStack.Push(CloneImage(bitmapSource));
                redoStack.Clear();

                // Create a new transformed bitmap by rotating 90 degrees
                TransformedBitmap rotatedBitmap = new TransformedBitmap(
                    bitmapSource,
                    new RotateTransform(90)
                );

                // Update the image workspace with the rotated image
                ImageWorkspace.Source = rotatedBitmap;
            }
            else
            {
                MessageBox.Show("No image to rotate.", "Info");
            }
        }




        private CustomImage RotateImage(CustomImage original)
        {
            int newWidth = original.Height;
            int newHeight = original.Width;
            CustomImage rotated = new CustomImage(newWidth, newHeight);

            for (int y = 0; y < original.Height; y++)
            {
                for (int x = 0; x < original.Width; x++)
                {
                    Pixel pixel = original.GetPixel(x, y);
                    rotated.SetPixel(original.Height - y - 1, x, pixel);
                }
            }

            return rotated;
        }



        public class Pixel
        {
            public byte R, G, B;

            public Pixel(byte r, byte g, byte b)
            {
                R = r;
                G = g;
                B = b;
            }
        }

        public class CustomImage
        {
            private Pixel[,] pixels;

            public int Width { get; private set; }
            public int Height { get; private set; }

            public CustomImage(int width, int height)
            {
                Width = width;
                Height = height;
                pixels = new Pixel[width, height];

                // Initialize all pixels with black
                for (int x = 0; x < width; x++)
                    for (int y = 0; y < height; y++)
                        pixels[x, y] = new Pixel(0, 0, 0);
            }

            public Pixel GetPixel(int x, int y)
            {
                return pixels[x, y];
            }

            public void SetPixel(int x, int y, Pixel pixel)
            {
                pixels[x, y] = pixel;
            }
        }


        private double originalAspectRatio = 1.0;
        private bool isUpdatingDimensions = false;


        private void InitializeResizeDimensions()
        {
            if (ImageWorkspace.Source is BitmapSource source)
            {
                isUpdatingDimensions = true;
                WidthTextBox.Text = source.PixelWidth.ToString();
                HeightTextBox.Text = source.PixelHeight.ToString();
                originalAspectRatio = (double)source.PixelWidth / source.PixelHeight;
                isUpdatingDimensions = false;
            }
        }








        //resize code

        private void ResizeButton_Click(object sender, RoutedEventArgs e)
        {
            ResizeGroupBox.Visibility = ResizeGroupBox.Visibility == Visibility.Visible
                ? Visibility.Collapsed
                : Visibility.Visible;
        }



        private void WidthTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!isUpdatingDimensions && MaintainAspectRatioCheckBox.IsChecked == true)
            {
                if (int.TryParse(WidthTextBox.Text, out int width) && width > 0)
                {
                    isUpdatingDimensions = true;
                    // Calculate height based on aspect ratio
                    int height = (int)(width / originalAspectRatio);
                    HeightTextBox.Text = height.ToString();
                    isUpdatingDimensions = false;
                }
            }
        }

        private void HeightTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!isUpdatingDimensions && MaintainAspectRatioCheckBox.IsChecked == true)
            {
                if (int.TryParse(HeightTextBox.Text, out int height) && height > 0)
                {
                    isUpdatingDimensions = true;
                    // Calculate width based on aspect ratio
                    int width = (int)(height * originalAspectRatio);
                    WidthTextBox.Text = width.ToString();
                    isUpdatingDimensions = false;
                }
            }
        }

        private void MaintainAspectRatio_CheckedChanged(object sender, RoutedEventArgs e)
        {
            if (MaintainAspectRatioCheckBox.IsChecked == true &&
                int.TryParse(WidthTextBox.Text, out int width) && width > 0)
            {
                isUpdatingDimensions = true;
                // Reset height based on width
                int height = (int)(width / originalAspectRatio);
                HeightTextBox.Text = height.ToString();
                isUpdatingDimensions = false;
            }
        }

        private void SizePreset_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SizePresetsComboBox.SelectedItem is ComboBoxItem selectedItem &&
                selectedItem.Tag is string dimensions)
            {
                isUpdatingDimensions = true;

                // Parse dimensions from tag (format: "width,height")
                string[] parts = dimensions.Split(',');
                if (parts.Length == 2)
                {
                    WidthTextBox.Text = parts[0];
                    HeightTextBox.Text = parts[1];

                    // When selecting a preset, we should uncheck maintain aspect ratio
                    MaintainAspectRatioCheckBox.IsChecked = false;
                }

                isUpdatingDimensions = false;
            }
        }

        private void ApplyResizeButton_Click(object sender, RoutedEventArgs e)
        {
            if (ImageWorkspace.Source is BitmapSource bitmapSource)
            {
                if (int.TryParse(WidthTextBox.Text, out int newWidth) &&
                    int.TryParse(HeightTextBox.Text, out int newHeight))
                {
                    if (newWidth > 0 && newHeight > 0)
                    {
                        // Save to undo stack
                        undoStack.Push(CloneImage(bitmapSource));
                        redoStack.Clear();

                        // Apply resize
                        BitmapSource resizedBitmap = ResizeImage(bitmapSource, newWidth, newHeight);
                        ImageWorkspace.Source = resizedBitmap;

                        // Update original aspect ratio for future resize operations
                        originalAspectRatio = (double)newWidth / newHeight;

                        // Reset preset selection
                        SizePresetsComboBox.SelectedIndex = 0;
                        // Select "Custom"
                    }
                    else
                    {
                        MessageBox.Show("Width and Height must be greater than zero.",
                            "Invalid input", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Please enter valid numbers for width and height.",
                        "Invalid input", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                MessageBox.Show("No image loaded to resize.",
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // আপনার আগের ResizeImage মেথড ব্যবহার করুন, যা আপনি ইতিমধ্যে আপনার প্রোগ্রামে যোগ করেছেন
        private BitmapSource ResizeImage(BitmapSource source, int width, int height)
        {
            TransformedBitmap tb = new TransformedBitmap(source, new ScaleTransform(
                scaleX: (double)width / source.PixelWidth,
                scaleY: (double)height / source.PixelHeight));
            return tb;
        }



        // Code for text
        // ✅ Declare Missing Variables
        //private Point imageStartPosition;
        //private Point imageDragStartPoint;
        //private bool isImageDragging = false;
        private TextBlock selectedTextBlock;
        private bool isDraggingText = false;
        private Point textDragStartPoint;

        // ✅ Toggle Text Panel Visibility
        private void ToggleTextToolPanel(object sender, RoutedEventArgs e)
        {
            TextToolPanel.Visibility = TextToolPanel.Visibility == Visibility.Visible
                ? Visibility.Collapsed
                : Visibility.Visible;
        }

        // ✅ Apply Text to Canvas
        private void ApplyTextToImage(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(TextInputBox.Text))
            {
                selectedTextBlock = new TextBlock
                {
                    Text = TextInputBox.Text,
                    FontSize = FontSizeSlider.Value,
                    Foreground = Brushes.White
                };

                Canvas.SetLeft(selectedTextBlock, 50); // Default Position
                Canvas.SetTop(selectedTextBlock, 50);
                SelectionCanvas.Children.Add(selectedTextBlock);

                selectedTextBlock.MouseDown += StartDragging;
                selectedTextBlock.MouseMove += DragTextBlock;
                selectedTextBlock.MouseUp += StopDragging;
            }
        }

        // ✅ Change Font Size Dynamically
        private void FontSizeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (selectedTextBlock != null)
            {
                selectedTextBlock.FontSize = FontSizeSlider.Value;
            }
        }

        // ✅ Dragging Mechanism for Text
        private void StartDragging(object sender, MouseButtonEventArgs e)
        {
            isDraggingText = true;
            textDragStartPoint = e.GetPosition(SelectionCanvas);
        }

        private void DragTextBlock(object sender, MouseEventArgs e)
        {
            if (isDraggingText && sender is TextBlock textBlock)
            {
                Point newPoint = e.GetPosition(SelectionCanvas);
                double deltaX = newPoint.X - textDragStartPoint.X;
                double deltaY = newPoint.Y - textDragStartPoint.Y;

                Canvas.SetLeft(textBlock, Canvas.GetLeft(textBlock) + deltaX);
                Canvas.SetTop(textBlock, Canvas.GetTop(textBlock) + deltaY);

                textDragStartPoint = newPoint;
            }
        }

        private void StopDragging(object sender, MouseButtonEventArgs e)
        {
            isDraggingText = false;
        }

        // ✅ Remove Selected Text
        private void RemoveTextFromImage(object sender, RoutedEventArgs e)
        {
            if (selectedTextBlock != null)
            {
                SelectionCanvas.Children.Remove(selectedTextBlock);
                selectedTextBlock = null;
            }
        }
    }
}