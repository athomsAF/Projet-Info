using System.Windows.Forms;
using static projects.Program;
using System;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace projects
{
    public partial class Form1 : Form
    {
        private BitMap? loadedImage = null;
        private Huffman? huffman = null;

        private bool isCustomFile = false;
        private bool showHuffmanDialog = false;
        private List<bool> ?treeEncode = null;
        private List<bool> ?imageEncode = null;

        public Form1()
        {

            InitializeComponent();
            pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            openFileDialog1.InitialDirectory = Program.PROJECT_PATH;
            openFileDialog1.Filter = "Bitmap files (*.bmp)|*.bmp";
            this.textBox1.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.isCustomFile = false;

            this.listBox1.Items.Clear();
            var extensions = new List<string> { ".bmp" };
            string[] files = Directory.GetFiles(Program.PROJECT_PATH, "*.*", SearchOption.AllDirectories).Where(f => extensions.IndexOf(Path.GetExtension(f)) >= 0).ToArray();

            for (int i = 0; i < files.Length; i++)
            {
                files[i] = new String(files[i].Skip(Program.PROJECT_PATH.Length).ToArray());
            }

            this.listBox1.Items.AddRange(files);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string path;
            if (isCustomFile)
            {
                path = label1.Text;
            }
            else
            {
                path = Program.PROJECT_PATH + this.label1.Text;
            }

            if (path.EndsWith(".bmp"))
            {
                this.loadedImage = new BitMap(path);
                pictureBox2.ImageLocation = path;

                this.label27.Text = this.loadedImage.Type;
                this.label28.Text = this.loadedImage.Offset.ToString();
                this.label29.Text = this.loadedImage.Size.ToString();
                this.label30.Text = this.loadedImage.BitsParCouleur.ToString();
                this.label31.Text = this.loadedImage.Dimensions[0].ToString();
                this.label32.Text = this.loadedImage.Dimensions[1].ToString();
            }
            else
            {
                this.label27.Text = "...";
                this.label28.Text = "...";
                this.label29.Text = "...";
                this.label30.Text = "...";
                this.label31.Text = "...";
                this.label32.Text = "...";
                MessageBox.Show("Wrong file format !");
            }
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();

            this.label1.Text = openFileDialog1.FileName;
            this.isCustomFile = true;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.isCustomFile = false;
            this.label1.Text = listBox1.Text;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            BitMap image = new BitMap();

            ColorTable ct;
            if (IsGrayscale.Checked)
            {
                ct = new ColorTable(10);
            }
            else if (IsCustom.Checked)
            {
                ct = new ColorTable(new Pixel(Color1.BackColor.R, Color1.BackColor.G, Color1.BackColor.B), new Pixel(Color2.BackColor.R, Color2.BackColor.G, Color2.BackColor.B), 10);
            }
            else
            {
                Pixel[] rainbow = new Pixel[] { new Pixel(0, 0, 0), new Pixel(255, 0, 0), new Pixel(0, 255, 0), new Pixel(0, 0, 255) };
                ct = new ColorTable(rainbow, 10);
            }

            if (IsMandelbrot.Checked)
            {
                Console.WriteLine("Running Mandelbrot");
                image.ImagePixel = projects.Fractal.Mandelbrot(Convert.ToInt32(this.FractalHeigth.Text), Convert.ToInt32(this.FractalWidth.Text),
                    Convert.ToInt32(this.FractalIterations.Text), Convert.ToDouble(this.FractalZoom.Text), Convert.ToDouble(this.FractalMMoveX.Text),
                    Convert.ToDouble(this.FractalMMoveY.Text), ct);

                image.FromImageToFile("fractal");
            }
            else if (IsJulia.Checked)
            {
                image.ImagePixel = projects.Fractal.Julia(Convert.ToInt32(this.FractalHeigth.Text), Convert.ToInt32(this.FractalWidth.Text),
                    Convert.ToInt32(this.FractalIterations.Text), Convert.ToDouble(this.FractalZoom.Text), Convert.ToDouble(FractalMMoveX.Text),
                    Convert.ToDouble(FractalMMoveY.Text), Convert.ToDouble(FractalJSeedX.Text), Convert.ToDouble(FractalJSeedY.Text), ct);
                image.FromImageToFile("fractal");
            }
            else
            {
                Console.WriteLine("No fractal selected !");
            }

            string path = Program.PROJECT_PATH + "imagesOUT/fractal.bmp";
            pictureBox2.ImageLocation = path;

        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            Color1.BackColor = colorDialog1.Color;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            Color2.BackColor = colorDialog1.Color;
        }

        private void ColorRun_Click(object sender, EventArgs e)
        {
            if (this.loadedImage != null)
            {
                BitMap image = new();
                image.ImagePixel = this.loadedImage.ImagePixel;

                if (IsGray.Checked)
                {
                    Console.WriteLine("Running Grayscale");
                    image.GrayScale(GrayTrackBar.Value);
                    image.FromImageToFile("color");
                }
                else if (IsBlack.Checked)
                {
                    Console.WriteLine("Running Black And White");
                    image.BlackAndWhite(BlackTrackBar.Value);
                    image.FromImageToFile("color");
                }
                else if (IsFilter.Checked)
                {
                    Console.WriteLine("Running Color Filter");
                    image.ColorFilter(RedTrackBar.Value, GreenTrackBar.Value, BlueTrackBar.Value);
                    image.FromImageToFile("color");
                }
                else
                {
                    Console.WriteLine("No color selected !");
                }

                string path = Program.PROJECT_PATH + "imagesOUT/color.bmp";
                pictureBox2.ImageLocation = path;
            }
            else
            {
                Console.WriteLine("Warning, no image loaded !");
            }
        }

        private void ModifRun_Click(object sender, EventArgs e)
        {
            if (this.loadedImage != null)
            {
                BitMap image = new();
                image.ImagePixel = this.loadedImage.ImagePixel;

                if (IsExtension.Checked)
                {
                    Console.WriteLine("Running Extension");
                    image.Agrandissement(ModifExtensionBar.Value);
                    image.FromImageToFile("modif");
                }
                else if (IsShrink.Checked)
                {
                    Console.WriteLine("Running Shrink");
                    image.Agrandissement((double)1 / (double)ModifShrinkBar.Value);
                    image.FromImageToFile("modif");
                }
                else if (IsRotation.Checked)
                {
                    Console.WriteLine("Running Rotation");
                    image.Rotation((-1) * ModifRotationBar.Value);
                    image.FromImageToFile("modif");
                }
                else
                {
                    Console.WriteLine("No modification selected !");
                }

                string path = Program.PROJECT_PATH + "imagesOUT/modif.bmp";
                pictureBox2.ImageLocation = path;
            }
            else
            {
                Console.WriteLine("Warning, no image loaded !");
            }
        }

        private void ConvRun_Click(object sender, EventArgs e)
        {
            if (this.loadedImage != null)
            {
                BitMap image = new();
                image.ImagePixel = this.loadedImage.ImagePixel;

                if (IsBlur.Checked)
                {
                    int[,] matriceConv = new int[3, 3]{{1,2,1},
                                                 {2,5,2},
                                                 {1,2,1}};

                    image.convolution(matriceConv);
                    image.FromImageToFile("convolution");
                }
                else if (IsBorderDetection.Checked)
                {

                    int[,] DContours = new int[3, 3]{{0,-1,0},
                                               {-1,4,-1},
                                               {0,-1,0}};

                    image.convolution(DContours);
                    image.FromImageToFile("convolution");
                }
                else if (IsEmbossing.Checked)
                {
                    int[,] RBords = new int[3, 3]{{0,0,0},
                                            {-1,1,0},
                                            {0,0,0}};

                    image.convolution(RBords);
                    image.FromImageToFile("convolution");
                }
                else if (IsOther.Checked)
                {
                    int[,] DRepoussage = new int[3, 3]{{-2,-1,0},
                                                 {-1,1,1},
                                                 {0,-1,2}};

                    image.convolution(DRepoussage);
                    image.FromImageToFile("convolution");
                }
                else if (IsConvCustom.Checked)
                {
                    int[,] Custom = new int[3, 3]{{Convert.ToInt32(ConvCustom11.Text),Convert.ToInt32(ConvCustom12.Text),Convert.ToInt32(ConvCustom13.Text)},
                                                 {Convert.ToInt32(ConvCustom21.Text),Convert.ToInt32(ConvCustom22.Text),Convert.ToInt32(ConvCustom23.Text)},
                                                 {Convert.ToInt32(ConvCustom31.Text),Convert.ToInt32(ConvCustom32.Text),Convert.ToInt32(ConvCustom33.Text)}};

                    image.convolution(Custom);
                    image.FromImageToFile("convolution");
                }
                else
                {
                    Console.WriteLine("No convolution selected !");
                }

                string path = Program.PROJECT_PATH + "imagesOUT/convolution.bmp";
                pictureBox2.ImageLocation = path;
            }
            else
            {
                Console.WriteLine("Warning, no image loaded !");
            }
        }

        private void StegToHide_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            StegToHide.Text = openFileDialog1.FileName;
        }

        private void StegSource_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            StegSource.Text = openFileDialog1.FileName;
        }

        private void StegToUnhide_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            StegToUnhide.Text = openFileDialog1.FileName;
        }

        private void StegRun_Click(object sender, EventArgs e)
        {
            BitMap image = new BitMap();

            if (IsStegEncode.Checked)
            {
                Console.WriteLine("Running Steganography Encode");

                BitMap image1 = new BitMap(StegToHide.Text);
                BitMap image2 = new BitMap(StegSource.Text);
                image.ImagePixel = BitMap.steganography(image1, image2);
                image.FromImageToFile("steganography");
            }
            else if (IsStegDecode.Checked)
            {
                /*
                image.convolution(DContours);
                Console.WriteLine("Running Steganography Decode");
                image.FromImageToFile("steganography");
                */
            }
            else
            {
                Console.WriteLine("No steganography type selected !");
            }

            string path = Program.PROJECT_PATH + "imagesOUT/steganography.bmp";
            pictureBox2.ImageLocation = path;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (!showHuffmanDialog)
            {
                this.pictureBox2.Hide();
                this.textBox1.Show();
                showHuffmanDialog = true;
            }
            else
            {
                this.pictureBox2.Show();
                this.textBox1.Hide();
                showHuffmanDialog = false;
            }

        }

        private void button7_Click(object sender, EventArgs e)
        {
            textBox1.Clear();

            if (this.huffman != null)
            {
                textBox1.Text = Node.ShowTree(huffman.root);
                this.button7.BackColor = System.Drawing.Color.LightGreen;
            }
            else
            {
                Console.WriteLine("Warning, huffman not loaded !");
                this.button7.BackColor = System.Drawing.Color.OrangeRed;
            }
        }

        private void button4_Click_2(object sender, EventArgs e)
        {
            this.button7.BackColor = Color.BackColor;
            this.button8.BackColor = Color.BackColor;
            this.button9.BackColor = Color.BackColor;
            this.button10.BackColor = Color.BackColor;
            this.button11.BackColor = Color.BackColor;
            this.button12.BackColor = Color.BackColor;
            this.imageEncode = null;
            this.treeEncode = null;
            this.label34.BackColor = Color.BackColor;
            this.label36.Text = "not loaded";
            this.button10.BackColor = Color.BackColor;
            this.label33.BackColor = Color.BackColor;
            this.label35.Text = "not loaded";
            this.button9.BackColor = Color.BackColor;

            if (this.loadedImage != null)
            {
                this.huffman = new Huffman(this.loadedImage.ImagePixel);

                this.button4.BackColor = System.Drawing.Color.LightGreen;
            }
            else
            {
                Console.WriteLine("Warning, no image loaded !");
                this.button4.BackColor = System.Drawing.Color.OrangeRed;
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            textBox1.Clear();

            if (this.huffman != null)
            {
                textBox1.Text = huffman.ShowPixelEncoding();
                this.button8.BackColor = System.Drawing.Color.LightGreen;
            }
            else
            {
                Console.WriteLine("Warning, huffman not loaded !");
                this.button8.BackColor = System.Drawing.Color.OrangeRed;
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            textBox1.Clear();

            if (this.huffman != null && this.loadedImage != null)
            {
                (string matrix, List<bool> encoded) = huffman.ImageEncode(this.loadedImage.ImagePixel, true);
                textBox1.Text = matrix;

                this.label34.BackColor = System.Drawing.Color.LightGreen;
                this.label36.Text = encoded.ToArray().ToString();
                this.button10.BackColor = System.Drawing.Color.LightGreen;

                this.imageEncode = encoded;
            }
            else
            {
                Console.WriteLine("Warning, huffman not loaded !");
                this.button10.BackColor = System.Drawing.Color.OrangeRed;
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            textBox1.Clear();

            if (this.huffman != null && this.loadedImage != null)
            {
                List<bool> encoded = huffman.TreeEncode(this.huffman.root);

                string res = "";
                foreach (bool b in encoded)
                {
                    if (b) res += "1"; else res += "0";
                }

                this.textBox1.Text = res;

                this.label33.BackColor = System.Drawing.Color.LightGreen;
                this.label35.Text = encoded.ToArray().ToString();
                this.button9.BackColor = System.Drawing.Color.LightGreen;

                this.treeEncode = encoded;
            }
            else
            {
                Console.WriteLine("Warning, huffman not loaded !");
                this.button9.BackColor = System.Drawing.Color.OrangeRed;
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            textBox1.Clear();

            if (this.treeEncode != null)
            {
                this.huffman = new Huffman(Huffman.TreeDecode(this.treeEncode));

                if (this.huffman != null)
                {
                    textBox1.Text = Node.ShowTree(this.huffman.root);
                }

                this.button11.BackColor = System.Drawing.Color.LightGreen;
            }
            else
            {
                Console.WriteLine("Warning, huffman tree not encoded !");
                this.button11.BackColor = System.Drawing.Color.OrangeRed;
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            textBox1.Clear();

            if (this.huffman != null && this.treeEncode != null && this.imageEncode != null)
            {
                BitMap image = new BitMap();
                image.ImagePixel = this.huffman.ImageDecode(this.imageEncode, this.loadedImage.ImagePixel.GetLength(0), this.loadedImage.ImagePixel.GetLength(1));
                image.FromImageToFile("DecodedHuffman");

                this.button12.BackColor = System.Drawing.Color.LightGreen;

                string path = Program.PROJECT_PATH + "imagesOUT/DecodedHuffman.bmp";
                pictureBox2.ImageLocation = path;

                if (this.showHuffmanDialog)
                {
                    this.pictureBox2.Show();
                    this.textBox1.Hide();
                    showHuffmanDialog = false;
                }
            }
            else
            {
                Console.WriteLine("Warning, huffman image not encoded !");
                this.button12.BackColor = System.Drawing.Color.OrangeRed;
            }
        }
    }
}