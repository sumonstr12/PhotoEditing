using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhotoEditor
{
    public partial class Form1: Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            DialogResult result = MessageBox.Show("Are you sure you want to exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            
            if (result == DialogResult.Yes)
            {
                Application.Exit(); 
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Create a new form for the About dialog
            Form aboutForm = new Form
            {
                Text = "About TaskMaster Pro",
                Size = new Size(300, 200),
                StartPosition = FormStartPosition.CenterParent
            };

            // Create a label to display information
            Label aboutLabel = new Label
            {
                Text = "PhotoEditor Pro\nVersion 1.0\n\nDeveloped by \nSumon Roy,\n AUDITY SAHA,\n Sohag Chandro\n© Our Project",
                AutoSize = true,
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill
            };

            // Add the label to the About form
            aboutForm.Controls.Add(aboutLabel);

            // Create an OK button to close the dialog
            Button okButton = new Button
            {
                Text = "Exit",
                DialogResult = DialogResult.OK,
                Dock = DockStyle.Bottom
            };

            // Add the button to the About form
            aboutForm.Controls.Add(okButton);

            // Show the About dialog
            aboutForm.ShowDialog(this);
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    pictureBox.Image = Image.FromFile(openFileDialog.FileName);
                }
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pictureBox.Image != null)
            {
                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "JPEG Image|*.jpg|PNG Image|*.png|Bitmap Image|*.bmp";
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        pictureBox.Image.Save(saveFileDialog.FileName);
                    }
                }
            }
        }

        private void buttonCrop_Click(object sender, EventArgs e)
        {
            if (pictureBox.Image != null)
            {
                Bitmap originalBmp = new Bitmap(pictureBox.Image); // Clone original image

                // Ensure the crop area is within bounds
                int cropWidth = Math.Min(100, originalBmp.Width);
                int cropHeight = Math.Min(100, originalBmp.Height);
                Rectangle cropArea = new Rectangle(0, 0, cropWidth, cropHeight);

                // Perform cropping
                Bitmap croppedBmp = originalBmp.Clone(cropArea, originalBmp.PixelFormat);

                ApplyChange(croppedBmp); // Store the change before applying

                pictureBox.Image = croppedBmp; // Update pictureBox

                originalBmp.Dispose(); // Free memory
            }

        }

        private void rotateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pictureBox.Image != null)
            {
                pictureBox.Image.RotateFlip(RotateFlipType.Rotate90FlipNone);
                pictureBox.Refresh();
            }
        }

        private void resizeImgeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pictureBox.Image != null)
            {
                int newWidth = 800; // Example width
                int newHeight = 600; // Example height
                Bitmap resizedBmp = new Bitmap(pictureBox.Image, new Size(newWidth, newHeight));
                pictureBox.Image = resizedBmp;
            }
        }

        private void grayscaleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pictureBox.Image != null)
            {
                Bitmap bmp = new Bitmap(pictureBox.Image);
                for (int y = 0; y < bmp.Height; y++)
                {
                    for (int x = 0; x < bmp.Width; x++)
                    {
                        Color pixel = bmp.GetPixel(x, y);
                        int gray = (int)(pixel.R * 0.3 + pixel.G * 0.59 + pixel.B * 0.11);
                        bmp.SetPixel(x, y, Color.FromArgb(gray, gray, gray));
                    }
                }
                pictureBox.Image = bmp;
            }
        }

        private void ApplySepiaFilter()
        {
            if (pictureBox.Image != null)
            {
                Bitmap bmp = new Bitmap(pictureBox.Image);

                for (int y = 0; y < bmp.Height; y++)
                {
                    for (int x = 0; x < bmp.Width; x++)
                    {
                        Color pixel = bmp.GetPixel(x, y);

                        // Calculate sepia tone
                        int r = (int)(pixel.R * 0.393 + pixel.G * 0.769 + pixel.B * 0.189);
                        int g = (int)(pixel.R * 0.349 + pixel.G * 0.686 + pixel.B * 0.168);
                        int b = (int)(pixel.R * 0.272 + pixel.G * 0.534 + pixel.B * 0.131);

                        // Clamp values to 0-255
                        r = Math.Min(255, r);
                        g = Math.Min(255, g);
                        b = Math.Min(255, b);

                        bmp.SetPixel(x, y, Color.FromArgb(r, g, b));
                    }
                }

                pictureBox.Image = bmp;
            }
        }

        private void shepiaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ApplySepiaFilter();
        }


        private void ApplyInvertColorFilter()
        {
            if (pictureBox.Image != null)
            {
                Bitmap bmp = new Bitmap(pictureBox.Image);

                for (int y = 0; y < bmp.Height; y++)
                {
                    for (int x = 0; x < bmp.Width; x++)
                    {
                        Color pixel = bmp.GetPixel(x, y);

                        // Invert the color
                        int r = 255 - pixel.R;
                        int g = 255 - pixel.G;
                        int b = 255 - pixel.B;

                        bmp.SetPixel(x, y, Color.FromArgb(r, g, b));
                    }
                }

                pictureBox.Image = bmp;
            }
        }

        // Call this method when the "Invert Color" button is clicked
     

        private void invertColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ApplyInvertColorFilter();
        }
        private void ApplyBlackAndWhiteFilter()
        {
            if (pictureBox.Image != null)
            {
                Bitmap originalBmp = new Bitmap(pictureBox.Image);
                Bitmap newBmp = new Bitmap(originalBmp.Width, originalBmp.Height);

                for (int y = 0; y < originalBmp.Height; y++)
                {
                    for (int x = 0; x < originalBmp.Width; x++)
                    {
                        Color pixel = originalBmp.GetPixel(x, y);
                        int gray = (int)(0.3 * pixel.R + 0.59 * pixel.G + 0.11 * pixel.B);
                        newBmp.SetPixel(x, y, Color.FromArgb(gray, gray, gray));
                    }
                }

                ApplyChange(newBmp); 

                pictureBox.Image = newBmp; 

                originalBmp.Dispose();
            }
        }

        private void blackWhiteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ApplyBlackAndWhiteFilter();
        }


        private void ApplyVintageFilter()
        {
            if (pictureBox.Image != null)
            {
                Bitmap originalBmp = new Bitmap(pictureBox.Image);
                Bitmap newBmp = new Bitmap(originalBmp.Width, originalBmp.Height);

                for (int y = 0; y < originalBmp.Height; y++)
                {
                    for (int x = 0; x < originalBmp.Width; x++)
                    {
                        Color pixel = originalBmp.GetPixel(x, y);

                        int r = (int)(pixel.R * 0.9 + 20);
                        int g = (int)(pixel.G * 0.7 + 20);
                        int b = (int)(pixel.B * 0.4 + 20);

                        newBmp.SetPixel(x, y, Color.FromArgb(
                            Math.Min(255, r),
                            Math.Min(255, g),
                            Math.Min(255, b)
                        ));
                    }
                }

                ApplyChange(newBmp);
                pictureBox.Image = newBmp;
                originalBmp.Dispose();

            }
        }


        private void vintageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ApplyVintageFilter();
        }

        private void ApplyOilPaintEffect()
        {
            if (pictureBox.Image != null)
            {
                Bitmap bmp = new Bitmap(pictureBox.Image);
                Bitmap newBmp = new Bitmap(bmp.Width, bmp.Height);
                Random rand = new Random();

                for (int y = 1; y < bmp.Height - 1; y++)
                {
                    for (int x = 1; x < bmp.Width - 1; x++)
                    {
                        int dx = rand.Next(-1, 2);
                        int dy = rand.Next(-1, 2);
                        newBmp.SetPixel(x, y, bmp.GetPixel(x + dx, y + dy));
                    }
                }
                ApplyChange(newBmp);
                pictureBox.Image = newBmp;
                bmp.Dispose();
            }
        }

        private void oilPaintToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ApplyOilPaintEffect();
        }

        private void ApplySketchEffect()
        {
            if (pictureBox.Image != null)
            {
                Bitmap bmp = new Bitmap(pictureBox.Image);
                Bitmap newBmp = new Bitmap(bmp.Width, bmp.Height);

                for (int y = 1; y < bmp.Height - 1; y++)
                {
                    for (int x = 1; x < bmp.Width - 1; x++)
                    {
                        Color pixel = bmp.GetPixel(x, y);
                        Color nextPixel = bmp.GetPixel(x + 1, y + 1);
                        int diff = Math.Abs(pixel.R - nextPixel.R) +
                                   Math.Abs(pixel.G - nextPixel.G) +
                                   Math.Abs(pixel.B - nextPixel.B);
                        int sketchColor = diff > 50 ? 0 : 255;
                        newBmp.SetPixel(x, y, Color.FromArgb(sketchColor, sketchColor, sketchColor));
                    }
                }
                ApplyChange(newBmp);
                pictureBox.Image = newBmp;
                bmp.Dispose();
            }
        }

        private void sketchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ApplySketchEffect();
        }

        private void ApplyGlowEffect()
        {
            if (pictureBox.Image != null)
            {
                Bitmap bmp = new Bitmap(pictureBox.Image);
                Bitmap newBmp = new Bitmap(bmp.Width, bmp.Height);
                for (int y = 0; y < bmp.Height; y++)
                {
                    for (int x = 0; x < bmp.Width; x++)
                    {
                        Color pixel = bmp.GetPixel(x, y);
                        int r = Math.Min(255, pixel.R + 50);
                        int g = Math.Min(255, pixel.G + 50);
                        int b = Math.Min(255, pixel.B + 50);
                        newBmp.SetPixel(x, y, Color.FromArgb(r, g, b));
                    }
                }
                ApplyChange(newBmp);
                pictureBox.Image = newBmp;
                bmp.Dispose();
            }
        }

        private void glowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ApplyGlowEffect();
        }

        private void ApplyCartoonEffect()
        {
            if (pictureBox.Image != null)
            {
                Bitmap bmp = new Bitmap(pictureBox.Image);
                Bitmap newBmp = new Bitmap(bmp.Width, bmp.Height);
                for (int y = 1; y < bmp.Height - 1; y++)
                {
                    for (int x = 1; x < bmp.Width - 1; x++)
                    {
                        Color pixel = bmp.GetPixel(x, y);
                        int r = (pixel.R / 64) * 64;
                        int g = (pixel.G / 64) * 64;
                        int b = (pixel.B / 64) * 64;
                        newBmp.SetPixel(x, y, Color.FromArgb(r, g, b));
                    }
                }
                ApplyChange(newBmp);
                pictureBox.Image = newBmp;
                bmp.Dispose();
            }
        }

        private void cartoonEffectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ApplyCartoonEffect();
        }



        private void ZoomIn()
        {
            if (pictureBox.Image != null)
            {
                int newWidth = (int)(pictureBox.Image.Width * 1.2);
                int newHeight = (int)(pictureBox.Image.Height * 1.2);

                pictureBox.Image = ResizeImage(pictureBox.Image, newWidth, newHeight);
            }
        }

        private void ZoomOut()
        {
            if (pictureBox.Image != null)
            {
                int newWidth = (int)(pictureBox.Image.Width / 1.2);
                int newHeight = (int)(pictureBox.Image.Height / 1.2);

                pictureBox.Image = ResizeImage(pictureBox.Image, newWidth, newHeight);
            }
        }

        private Image ResizeImage(Image img, int width, int height)
        {
            Bitmap bmp = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.DrawImage(img, 0, 0, width, height);
            }
            return bmp;
        }

        private void buttonZoomIn_Click(object sender, EventArgs e)
        {
            ZoomIn();
        }

        private void buttonZoomOut_Click(object sender, EventArgs e)
        {
            ZoomOut();
        }





        private void buttonZoomIn2_Click(object sender, EventArgs e)
        {
            ZoomIn();
        }

        private Stack<Image> undoStack = new Stack<Image>();
        private Stack<Image> redoStack = new Stack<Image>();

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (undoStack.Count > 0)
            {
                redoStack.Push(CloneImage(pictureBox.Image)); // Save current image
                pictureBox.Image = undoStack.Pop(); // Restore last image
            }
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (redoStack.Count > 0)
            {
                undoStack.Push(CloneImage(pictureBox.Image)); // Save current image
                pictureBox.Image = redoStack.Pop(); // Restore next image
            }
        }

        private void ApplyChange(Image newImage)
        {
            if (pictureBox.Image != null) // Prevent pushing null
            {
                undoStack.Push(CloneImage(pictureBox.Image)); // Save a copy
            }
            pictureBox.Image = newImage;
            redoStack.Clear(); // Clear redo stack as new change is made
        }

        // Helper function to create a deep copy of the image
        private Image CloneImage(Image img)
        {
            if (img == null) return null;
            return (Image)img.Clone();
        }


        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void buttonImageInfo_Click(object sender, EventArgs e)
        {

        }
    }
}
