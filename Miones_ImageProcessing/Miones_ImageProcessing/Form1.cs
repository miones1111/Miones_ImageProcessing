using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Miones_ImageProcessing
{
    public partial class Form1 : Form
    {
        Bitmap loaded, processed;
        
        public Form1()
        {
            InitializeComponent();
        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        //save image
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.InitialDirectory = "C:\\Users\\my pc\\Dropbox\\PC\\Downloads";
            saveFileDialog1.Title = "new file";
            saveFileDialog1.DefaultExt = "jpg";
            saveFileDialog1.ShowDialog();
        }

        //Open
        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            loaded = new Bitmap(openFileDialog1.FileName);
            pictureBox1.Image = loaded;
        }


        //Greyscale
        private void greyscalingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            processed = new Bitmap(loaded.Width, loaded.Height);
            for (int i = 0; i < loaded.Width; i++)
            {
                for (int j = 0; j < loaded.Height; j++)
                {
                    Color pixel = loaded.GetPixel(i, j);
                    int grey = (pixel.R + pixel.G + pixel.B) / 3;
                    Color greyscale = Color.FromArgb(grey, grey, grey);
                    processed.SetPixel(i, j, greyscale);
                }
            }
            pictureBox2.Image = processed;
        }


        //Invert
        private void invertToolStripMenuItem_Click(object sender, EventArgs e)
        {
            processed = new Bitmap(loaded.Width, loaded.Height);
            for (int i = 0; i < loaded.Width; i++)
            {
                for (int j = 0; j < loaded.Height; j++)
                {
                    Color pixel = loaded.GetPixel(i, j);
                    processed.SetPixel(i, j, Color.FromArgb(255 - pixel.R, 255 - pixel.G, 255 - pixel.B));
                }
            }
            pictureBox2.Image = processed;
        }


        //Mirror - Horizontal
        private void horizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            processed = new Bitmap(loaded.Width, loaded.Height);
            for (int i = 0; i < loaded.Width; i++)
            {
                for (int j = 0; j < loaded.Height; j++)
                {
                    Color pixel = loaded.GetPixel(i, j);
                    processed.SetPixel((loaded.Width - 1) - i, j, pixel);
                }
            }
            pictureBox2.Image = processed;
        }

        //Basic Copy
        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBox2.Image = loaded;
        }

        //Histogram
        private void histogramToolStripMenuItem_Click(object sender, EventArgs e)
        {
            processed = new Bitmap(loaded);

            //process image
            int[] histogram = new int[256];
            for (int i = 0; i < loaded.Width; i++)
            {
                for (int j = 0; j < loaded.Height; j++)
                {
                    Color c = loaded.GetPixel(i, j);
                    int avg = (c.R + c.G + c.B) / 3;
                    histogram[avg]++;
                }
            }

            //draw histogram 
            int max = histogram.Max();
            int scale = 100;
            int width = 256;
            int height = 100;

            //save histogram to the bitmap
            Bitmap histogramImage = new Bitmap(width, height);
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    if (j < (histogram[i] * scale / max))
                    {
                        histogramImage.SetPixel(i, j, Color.Black);
                    }
                    else
                    {
                        histogramImage.SetPixel(i, j, Color.White);
                    }
                }
            }
            pictureBox2.Image = histogramImage;
        }

        //Sepia
        private void sepiaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            processed = new Bitmap(loaded);

            for (int i = 0; i < loaded.Width; i++)
            {
                for (int j = 0; j < loaded.Height; j++)
                {
                    Color pixel = loaded.GetPixel(i, j);

                    int red = (int)(pixel.R * 0.393 + pixel.G * 0.769 + pixel.B * 0.189);
                    int green = (int)(pixel.R * 0.349 + pixel.G * 0.686 + pixel.B * 0.168);
                    int blue = (int)(pixel.R * 0.272 + pixel.G * 0.534 + pixel.B * 0.131);

                    if (red > 255) red = 255;
                    if (green > 255) green = 255;
                    if (blue > 255) blue = 255;
                    processed.SetPixel(i, j, Color.FromArgb(red, green, blue));
                }
            }
            pictureBox2.Image = processed;
        }

        //Open
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }
    }
}
