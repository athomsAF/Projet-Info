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
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
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
                ct = new ColorTable(new Color(Color1.BackColor.R, Color1.BackColor.G, Color1.BackColor.B), new Color(Color2.BackColor.R, Color2.BackColor.G, Color2.BackColor.B), 10);
            }
            else
            {
                Color[] rainbow = new Color[] { new Color(0,0,0), new Color(255, 0, 0), new Color(0, 255, 0), new Color(0, 0, 255) };
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
    }
}