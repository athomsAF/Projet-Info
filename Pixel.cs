using static projects.BitMap;

namespace projects
{
    public struct Pixel
    {
        public byte R;
        public byte G;
        public byte B;

        public Pixel(byte r, byte g, byte b)
        {
            R = r;
            G = g;
            B = b;
        }
    }

    public struct PixelJPG
    {
        public byte Y;
        public byte Cb;
        public byte Cr;
        public PixelJPG(byte r, byte g, byte b)
            {
                Y = Convert.ToByte(0.299*r+0.087*g+0.114*b);
                Cb = Convert.ToByte(-0.1687*r-0.3313*g+0.5*b+128);
                Cr = Convert.ToByte(0.5*r-0.4187*g-0.0813*b+128);
            }
    }

    public class Color
    {
        // Int values
        public int R;
        public int G;
        public int B;

        // Byte values
        public byte RB { get { return (byte) this.R;  } }
        public byte GB { get { return (byte) this.G; } }
        public byte BB { get { return (byte) this.B; } }

        public Color(byte r, byte g, byte b)
        {
            this.R = BitMap.ConvertirEndianToInt(new byte[] {r});
            this.G = BitMap.ConvertirEndianToInt(new byte[] {g});
            this.B = BitMap.ConvertirEndianToInt(new byte[] {b});
        }

        public Color(int r, int g, int b)
        {
            this.R = r;
            this.G = g;
            this.B = b;
        }
    }

    public class ColorTable
    {
        public Color[] table;

        /// <summary>
        /// Default greysacle
        /// </summary>
        public ColorTable()
        {
            Color a = new Color(0, 0, 0);
            Color b = new Color(255, 255, 255);

            // Get difference of variation between the two colors
            int diffR = a.R - b.R;
            int diffG = a.G - b.G;
            int diffB = a.B - b.B;

            // Get the maximum number of iterations from the maximum difference between colors
            int iter = Math.Max(Math.Abs(diffR), Math.Max(Math.Abs(diffG), Math.Abs(diffB)));

            List<Color> tab = new List<Color>();

            // Create the color scale
            for (int i = 0; i < iter; i++)
            {
                tab.Add(new Color(a.R - i * diffR / iter, a.G - i * diffG / iter, a.B - i * diffB / iter));
            }

            this.table = tab.ToArray();
        }

        /// <summary>
        /// Color scale between 2 colors
        /// </summary>
        /// <param name="a">First color</param>
        /// <param name="b">Second color</param>
        public ColorTable(Color a, Color b)
        {
            // Get difference of variation between the two colors
            int diffR = a.R - b.R;
            int diffG = a.G - b.G;
            int diffB = a.B - b.B;

            // Get the maximum number of iterations from the maximum difference between colors
            int iter = Math.Max(Math.Abs(diffR), Math.Max(Math.Abs(diffG), Math.Abs(diffB)));

            List<Color> tab = new List<Color>();

            // Create the color scale
            for (int i = 0; i < iter; i++)
            {
                tab.Add(new Color(a.R - i * diffR/iter, a.G - i * diffG/iter, a.B - i * diffB/iter));
            }

            this.table = tab.ToArray();
        }

        /// <summary>
        /// Color scale between n colors
        /// </summary>
        /// <param name="a">Array with n color</param>
        public ColorTable(Color[] a)
        {
            List<Color> tab = new List<Color>();
            
            // Repear for each couple of color
            for(int j = 0; j < a.Length - 1; j++)
            {
                // Get difference of variation between two colors
                int diffR = a[j].R - a[j+1].R;
                int diffG = a[j].G - a[j+1].G;
                int diffB = a[j].B - a[j+1].B;

                // Get the maximum number of iterations from the maximum difference between colors
                int iter = Math.Max(Math.Abs(diffR), Math.Max(Math.Abs(diffG), Math.Abs(diffB)));

                // Create the color scale
                for (int i = 0; i < iter; i++)
                {
                    tab.Add(new Color(a[j].R - i * diffR / iter, a[j].G - i * diffG / iter, a[j].B - i * diffB / iter));
                }
            }
            this.table = tab.ToArray();
        }
    }
}