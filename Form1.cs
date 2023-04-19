using System.Windows.Forms;
using static projects.Program;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace projects
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            openFileDialog1.InitialDirectory = Program.PROJECT_PATH;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.label1.Text = "CHOIX";
            this.listBox1.Items.Clear();
            var extensions = new List<string> { ".bmp" };
            string[] files = Directory.GetFiles(Program.PROJECT_PATH, "*.*", SearchOption.AllDirectories).Where(f => extensions.IndexOf(Path.GetExtension(f)) >= 0).ToArray();

            for(int i =0; i < files.Length; i++)
            {
                files[i] = new String(files[i].Skip(Program.PROJECT_PATH.Length).ToArray());
            }


            this.listBox1.Items.AddRange(files);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string path = Program.PROJECT_PATH + this.label1.Text;
            pictureBox2.ImageLocation = path;

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.label1.Text = listBox1.Text;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            BitMap image = new BitMap();

            ColorTable ct;
            if(IsGrayscale.Checked)
            {
                ct = new ColorTable(10);
            }
            else if (IsCustom.Checked)
            {
                ct = new ColorTable(new Pixel(Color1.BackColor.R, Color1.BackColor.G, Color1.BackColor.B), new Pixel(Color2.BackColor.R, Color2.BackColor.G, Color2.BackColor.B), 10);
            }
            else
            {
                Pixel[] rainbow = new Pixel[] { new Pixel(0,0,0), new Pixel(255, 0, 0), new Pixel(0, 255, 0), new Pixel(0, 0, 255) };
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
            string file = new string(this.label1.Text.Split("\\").Last().SkipLast(4).ToArray());
            Console.WriteLine(file);
            BitMap image = new BitMap(file);

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

        private void ModifRun_Click(object sender, EventArgs e)
        {
            string file = new string(this.label1.Text.Split("\\").Last().SkipLast(4).ToArray());
            Console.WriteLine(file);
            BitMap image = new BitMap(file);

            if (IsExtension.Checked)
            {
                Console.WriteLine("Running Extension");
                image.Agrandissement(ModifExtensionBar.Value);
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

        private void ConvRun_Click(object sender, EventArgs e)
        {
            string file = new string(this.label1.Text.Split("\\").Last().SkipLast(4).ToArray());
            Console.WriteLine(file);
            BitMap image = new BitMap(file);

            Console.WriteLine("Running Convolution");

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
            else if(IsEmbossing.Checked)
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
            else if(IsConvCustom.Checked)
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

        private void StegToHide_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            StegToHide.Text = openFileDialog1.InitialDirectory + openFileDialog1.FileName;
        }

        private void StegSource_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            StegSource.Text = openFileDialog1.InitialDirectory + openFileDialog1.FileName;
        }

        private void StegToUnhide_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            StegToUnhide.Text = openFileDialog1.InitialDirectory + openFileDialog1.FileName;
        }

        private void StegRun_Click(object sender, EventArgs e)
        {
            string file = new string(this.label1.Text.Split("\\").Last().SkipLast(4).ToArray());
            Console.WriteLine(file);

            BitMap image = new BitMap();

            if (IsStegEncode.Checked)
            {
                string file1 = new string(StegSource.Text.Split("\\").Last().SkipLast(4).ToArray());

                string file2 = new string(StegToHide.Text.Split("\\").Last().SkipLast(4).ToArray());

                Console.WriteLine(file1);
                Console.WriteLine(file2);


                Console.WriteLine("Running Steganography Encode");

                Pixel[,] a = BitMap.steganography(new BitMap(file1), new BitMap(file2));

                Console.WriteLine(a);

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
    }
}