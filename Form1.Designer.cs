namespace projects
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.Fractal = new System.Windows.Forms.Button();
            this.FractalHeigth = new System.Windows.Forms.TextBox();
            this.FractalWidth = new System.Windows.Forms.TextBox();
            this.FractalIterations = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.FractalZoom = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.FractalMMoveX = new System.Windows.Forms.TextBox();
            this.FractalMMoveY = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label10 = new System.Windows.Forms.Label();
            this.Fractals = new System.Windows.Forms.TabPage();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.Color2 = new System.Windows.Forms.Button();
            this.Color1 = new System.Windows.Forms.Button();
            this.IsRainbow = new System.Windows.Forms.RadioButton();
            this.IsCustom = new System.Windows.Forms.RadioButton();
            this.IsGrayscale = new System.Windows.Forms.RadioButton();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.FractalJSeedY = new System.Windows.Forms.TextBox();
            this.FractalJSeedX = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.IsMandelbrot = new System.Windows.Forms.RadioButton();
            this.IsJulia = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.Convolution = new System.Windows.Forms.TabPage();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.Fractals.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1247, 11);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(73, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Fin";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(5, 5);
            this.button2.Margin = new System.Windows.Forms.Padding(2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(298, 24);
            this.button2.TabIndex = 1;
            this.button2.Text = "Update";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(245, 126);
            this.button3.Margin = new System.Windows.Forms.Padding(2);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(58, 23);
            this.button3.TabIndex = 2;
            this.button3.Text = "Load";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(79, 124);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(26, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "......";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(100, 50);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 17;
            this.listBox1.Location = new System.Drawing.Point(5, 33);
            this.listBox1.Margin = new System.Windows.Forms.Padding(2);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(298, 89);
            this.listBox1.TabIndex = 6;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // Fractal
            // 
            this.Fractal.Location = new System.Drawing.Point(76, 15);
            this.Fractal.Name = "Fractal";
            this.Fractal.Size = new System.Drawing.Size(147, 23);
            this.Fractal.TabIndex = 7;
            this.Fractal.Text = "Run and Load";
            this.Fractal.UseVisualStyleBackColor = true;
            this.Fractal.Click += new System.EventHandler(this.button4_Click);
            // 
            // FractalHeigth
            // 
            this.FractalHeigth.Location = new System.Drawing.Point(6, 29);
            this.FractalHeigth.Name = "FractalHeigth";
            this.FractalHeigth.Size = new System.Drawing.Size(100, 25);
            this.FractalHeigth.TabIndex = 8;
            this.FractalHeigth.Text = "1000";
            // 
            // FractalWidth
            // 
            this.FractalWidth.Location = new System.Drawing.Point(6, 60);
            this.FractalWidth.Name = "FractalWidth";
            this.FractalWidth.Size = new System.Drawing.Size(100, 25);
            this.FractalWidth.TabIndex = 9;
            this.FractalWidth.Text = "1000";
            // 
            // FractalIterations
            // 
            this.FractalIterations.Location = new System.Drawing.Point(6, 91);
            this.FractalIterations.Name = "FractalIterations";
            this.FractalIterations.Size = new System.Drawing.Size(100, 25);
            this.FractalIterations.TabIndex = 10;
            this.FractalIterations.Text = "1000";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(111, 32);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 17);
            this.label3.TabIndex = 11;
            this.label3.Text = "Heigth";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(111, 63);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(42, 17);
            this.label4.TabIndex = 12;
            this.label4.Text = "Width";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(111, 96);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(62, 17);
            this.label5.TabIndex = 13;
            this.label5.Text = "Iterations";
            // 
            // FractalZoom
            // 
            this.FractalZoom.Location = new System.Drawing.Point(6, 122);
            this.FractalZoom.Name = "FractalZoom";
            this.FractalZoom.Size = new System.Drawing.Size(100, 25);
            this.FractalZoom.TabIndex = 14;
            this.FractalZoom.Text = "1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(111, 125);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 17);
            this.label2.TabIndex = 15;
            this.label2.Text = "Zoom";
            // 
            // FractalMMoveX
            // 
            this.FractalMMoveX.Location = new System.Drawing.Point(6, 25);
            this.FractalMMoveX.Name = "FractalMMoveX";
            this.FractalMMoveX.Size = new System.Drawing.Size(100, 25);
            this.FractalMMoveX.TabIndex = 16;
            this.FractalMMoveX.Text = "0";
            // 
            // FractalMMoveY
            // 
            this.FractalMMoveY.Location = new System.Drawing.Point(6, 57);
            this.FractalMMoveY.Name = "FractalMMoveY";
            this.FractalMMoveY.Size = new System.Drawing.Size(100, 25);
            this.FractalMMoveY.TabIndex = 17;
            this.FractalMMoveY.Text = "0";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(111, 28);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 17);
            this.label6.TabIndex = 18;
            this.label6.Text = "Move X";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(112, 60);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(52, 17);
            this.label7.TabIndex = 19;
            this.label7.Text = "Move Y";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.Fractals);
            this.tabControl1.Controls.Add(this.Convolution);
            this.tabControl1.Location = new System.Drawing.Point(12, 11);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(316, 519);
            this.tabControl1.TabIndex = 20;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label10);
            this.tabPage1.Controls.Add(this.listBox1);
            this.tabPage1.Controls.Add(this.button3);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.button2);
            this.tabPage1.Location = new System.Drawing.Point(4, 26);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(308, 489);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Main";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(7, 124);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(68, 17);
            this.label10.TabIndex = 7;
            this.label10.Text = "Selected : ";
            // 
            // Fractals
            // 
            this.Fractals.AutoScroll = true;
            this.Fractals.AutoScrollMargin = new System.Drawing.Size(0, 70);
            this.Fractals.Controls.Add(this.groupBox5);
            this.Fractals.Controls.Add(this.groupBox4);
            this.Fractals.Controls.Add(this.groupBox3);
            this.Fractals.Controls.Add(this.groupBox2);
            this.Fractals.Controls.Add(this.groupBox1);
            this.Fractals.Controls.Add(this.Fractal);
            this.Fractals.Location = new System.Drawing.Point(4, 26);
            this.Fractals.Name = "Fractals";
            this.Fractals.Padding = new System.Windows.Forms.Padding(3);
            this.Fractals.Size = new System.Drawing.Size(308, 489);
            this.Fractals.TabIndex = 1;
            this.Fractals.Text = "Fractals";
            this.Fractals.UseVisualStyleBackColor = true;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.Color2);
            this.groupBox5.Controls.Add(this.Color1);
            this.groupBox5.Controls.Add(this.IsRainbow);
            this.groupBox5.Controls.Add(this.IsCustom);
            this.groupBox5.Controls.Add(this.IsGrayscale);
            this.groupBox5.Location = new System.Drawing.Point(6, 467);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(276, 128);
            this.groupBox5.TabIndex = 22;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Color Picker";
            // 
            // Color2
            // 
            this.Color2.BackColor = System.Drawing.Color.White;
            this.Color2.Location = new System.Drawing.Point(55, 89);
            this.Color2.Name = "Color2";
            this.Color2.Size = new System.Drawing.Size(157, 29);
            this.Color2.TabIndex = 4;
            this.Color2.Text = "Pick Color 2";
            this.Color2.UseVisualStyleBackColor = false;
            this.Color2.Click += new System.EventHandler(this.button5_Click);
            // 
            // Color1
            // 
            this.Color1.BackColor = System.Drawing.Color.White;
            this.Color1.Location = new System.Drawing.Point(55, 54);
            this.Color1.Name = "Color1";
            this.Color1.Size = new System.Drawing.Size(157, 29);
            this.Color1.TabIndex = 3;
            this.Color1.Text = "Pick Color 1";
            this.Color1.UseVisualStyleBackColor = false;
            this.Color1.Click += new System.EventHandler(this.button4_Click_1);
            // 
            // IsRainbow
            // 
            this.IsRainbow.AutoSize = true;
            this.IsRainbow.Location = new System.Drawing.Point(170, 27);
            this.IsRainbow.Name = "IsRainbow";
            this.IsRainbow.Size = new System.Drawing.Size(76, 21);
            this.IsRainbow.TabIndex = 2;
            this.IsRainbow.Text = "Rainbow";
            this.IsRainbow.UseVisualStyleBackColor = true;
            // 
            // IsCustom
            // 
            this.IsCustom.AutoSize = true;
            this.IsCustom.Location = new System.Drawing.Point(94, 27);
            this.IsCustom.Name = "IsCustom";
            this.IsCustom.Size = new System.Drawing.Size(70, 21);
            this.IsCustom.TabIndex = 1;
            this.IsCustom.Text = "Custom";
            this.IsCustom.UseVisualStyleBackColor = true;
            // 
            // IsGrayscale
            // 
            this.IsGrayscale.AutoSize = true;
            this.IsGrayscale.Checked = true;
            this.IsGrayscale.Location = new System.Drawing.Point(6, 27);
            this.IsGrayscale.Name = "IsGrayscale";
            this.IsGrayscale.Size = new System.Drawing.Size(82, 21);
            this.IsGrayscale.TabIndex = 0;
            this.IsGrayscale.TabStop = true;
            this.IsGrayscale.Text = "Greyscale";
            this.IsGrayscale.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label9);
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Controls.Add(this.FractalJSeedY);
            this.groupBox4.Controls.Add(this.FractalJSeedX);
            this.groupBox4.Location = new System.Drawing.Point(6, 373);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(276, 88);
            this.groupBox4.TabIndex = 23;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Julia Exclusive Options";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(112, 58);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(48, 17);
            this.label9.TabIndex = 21;
            this.label9.Text = "Seed Y";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(111, 27);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(49, 17);
            this.label8.TabIndex = 20;
            this.label8.Text = "Seed X";
            // 
            // FractalJSeedY
            // 
            this.FractalJSeedY.Location = new System.Drawing.Point(6, 55);
            this.FractalJSeedY.Name = "FractalJSeedY";
            this.FractalJSeedY.Size = new System.Drawing.Size(100, 25);
            this.FractalJSeedY.TabIndex = 20;
            this.FractalJSeedY.Text = "0";
            // 
            // FractalJSeedX
            // 
            this.FractalJSeedX.Location = new System.Drawing.Point(6, 24);
            this.FractalJSeedX.Name = "FractalJSeedX";
            this.FractalJSeedX.Size = new System.Drawing.Size(100, 25);
            this.FractalJSeedX.TabIndex = 20;
            this.FractalJSeedX.Text = "0";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.FractalMMoveX);
            this.groupBox3.Controls.Add(this.FractalMMoveY);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Location = new System.Drawing.Point(6, 276);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(276, 91);
            this.groupBox3.TabIndex = 22;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Mandelbrot Exclusive Options";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.IsMandelbrot);
            this.groupBox2.Controls.Add(this.IsJulia);
            this.groupBox2.Location = new System.Drawing.Point(6, 44);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(276, 61);
            this.groupBox2.TabIndex = 21;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Fractal Type";
            // 
            // IsMandelbrot
            // 
            this.IsMandelbrot.AutoSize = true;
            this.IsMandelbrot.Checked = true;
            this.IsMandelbrot.Location = new System.Drawing.Point(21, 24);
            this.IsMandelbrot.Name = "IsMandelbrot";
            this.IsMandelbrot.Size = new System.Drawing.Size(117, 21);
            this.IsMandelbrot.TabIndex = 20;
            this.IsMandelbrot.TabStop = true;
            this.IsMandelbrot.Text = "Mandelbrot Set";
            this.IsMandelbrot.UseVisualStyleBackColor = true;
            // 
            // IsJulia
            // 
            this.IsJulia.AutoSize = true;
            this.IsJulia.Location = new System.Drawing.Point(154, 24);
            this.IsJulia.Name = "IsJulia";
            this.IsJulia.Size = new System.Drawing.Size(73, 21);
            this.IsJulia.TabIndex = 21;
            this.IsJulia.Text = "Julia Set";
            this.IsJulia.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.FractalHeigth);
            this.groupBox1.Controls.Add(this.FractalWidth);
            this.groupBox1.Controls.Add(this.FractalIterations);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.FractalZoom);
            this.groupBox1.Location = new System.Drawing.Point(6, 111);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(276, 159);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "All";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Location = new System.Drawing.Point(334, 37);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(986, 493);
            this.pictureBox2.TabIndex = 21;
            this.pictureBox2.TabStop = false;
            // 
            // Convolution
            // 
            this.Convolution.Location = new System.Drawing.Point(4, 26);
            this.Convolution.Name = "Convolution";
            this.Convolution.Padding = new System.Windows.Forms.Padding(3);
            this.Convolution.Size = new System.Drawing.Size(308, 489);
            this.Convolution.TabIndex = 2;
            this.Convolution.Text = "Convolution";
            this.Convolution.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(1332, 542);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.button1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.Text = "Projet Informatique";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.Fractals.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Button button1;
        private Button button2;
        private Button button3;
        private Label label1;
        private PictureBox pictureBox1;
        private ListBox listBox1;
        private Button Fractal;
        private TextBox FractalHeigth;
        private TextBox FractalWidth;
        private TextBox FractalIterations;
        private Label label3;
        private Label label4;
        private Label label5;
        private TextBox FractalZoom;
        private Label label2;
        private TextBox FractalMMoveX;
        private TextBox FractalMMoveY;
        private Label label6;
        private Label label7;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage Fractals;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private RadioButton IsMandelbrot;
        private RadioButton IsJulia;
        private PictureBox pictureBox2;
        private GroupBox groupBox3;
        private GroupBox groupBox4;
        private GroupBox groupBox5;
        private Label label9;
        private Label label8;
        private TextBox FractalJSeedY;
        private TextBox FractalJSeedX;
        private Button Color1;
        private RadioButton radioButton3;
        private RadioButton radioButton2;
        private RadioButton radioButton1;
        private ColorDialog colorDialog1;
        private Label label10;
        private Button Color2;
        private ListBox listBox2;
        private Button button6;
        private Label label11;
        private RadioButton IsCustom;
        private RadioButton IsGrayscale;
        private RadioButton IsRainbow;
        private TabPage Convolution;
    }
}