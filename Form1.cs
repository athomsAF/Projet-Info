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
            pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.Height = 1800;
            this.Width = 1000;
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
            pictureBox1.ImageLocation = path;

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.label1.Text = listBox1.Text;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string[] files = { "Test", "lac", "lena", "coco" };
            // create BitMap[] IMAGES for each file in files

            BitMap image = new BitMap(files[0]);

            image.ImagePixel = Fractal.Mandelbrot(Convert.ToInt32(this.textBox1.Text), Convert.ToInt32(this.textBox2.Text), Convert.ToInt32(this.textBox3.Text), Convert.ToDouble(this.textBox4.Text)
                , Convert.ToDouble(this.textBox5.Text), Convert.ToDouble(this.textBox6.Text)); ;
            image.FromImageToFile("test");
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_Load_1(object sender, EventArgs e)
        {

        }

    }
}