using static projects.BitMap;

namespace projects
{
    /// <summary>
    /// Pixel
    /// </summary>
    public class Pixel
    {
        // Attributes
        public byte R;
        public byte G;
        public byte B;

        // Easy convert byte <=> int
        public int RI { get { return ConvertirEndianToInt(new byte[] { R }); } }
        public int GI { get { return ConvertirEndianToInt(new byte[] { G }); } }
        public int BI { get { return ConvertirEndianToInt(new byte[] { B }); } }

        /// <summary>
        /// Default constructor : black pixel
        /// </summary>
        public Pixel()
        {
            R = 0;
            G = 0;
            B = 0;
        }

        /// <summary>
        /// Basic constructor : byte input
        /// </summary>
        /// <param name="r">Red</param>
        /// <param name="g">Green</param>
        /// <param name="b">Blue</param>
        public Pixel(byte r, byte g, byte b)
        {
            R = r;
            G = g;
            B = b;
        }

        /// <summary>
        /// Basic constructor : int input
        /// </summary>
        /// <param name="r">Red</param>
        /// <param name="g">Green</param>
        /// <param name="b">Blue</param>
        public Pixel(int r, int g, int b)
        {
            R = ConvertirIntToEndian(r, 1)[0];
            G = ConvertirIntToEndian(g, 1)[0];
            B = ConvertirIntToEndian(b, 1)[0];
        }

        /// <summary>
        /// String value of the pixel
        /// </summary>
        /// <returns>(R, G, B) or (null)</returns>
        public override string? ToString()
        {
            if (this.Equals(null))
            {
                return "(null)";
            }
            else
            {
                return "(" + this.R + " " + this.G + " " + this.B + ")";
            }
        }
    }

    /// <summary>
    /// Pixel value for jpg
    /// </summary>
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

    /// <summary>
    /// Generate a color gradient for the fractals
    /// </summary>
    public class ColorTable
    {
        public Pixel[] table;

        /// <summary>
        /// Default greysacle
        /// </summary>
        public ColorTable(int force = 1)
        {
            Pixel pixelList = new(0, 0, 0);
            Pixel endPixel = new(255, 255, 255);

            // Get difference of variation endPixeletween the two Pixels
            int diffR = pixelList.RI - endPixel.RI;
            int diffG = pixelList.GI - endPixel.GI;
            int diffB = pixelList.BI - endPixel.BI;

            // Get the maximum number of iterations from the maximum difference between Pixels
            int iter = Math.Max(Math.Abs(diffR), Math.Max(Math.Abs(diffG), Math.Abs(diffB)));

            List<Pixel> tab = new();

            // Create the Pixel scale
            for (int i = 0; i < iter/force; i++)
            {
                tab.Add(new Pixel(pixelList.RI - i * force * diffR / iter, pixelList.GI - i * force * diffG / iter, pixelList.BI - i * force * diffB / iter));
            }

            this.table = tab.ToArray();
        }

        /// <summary>
        /// Pixel scale between 2 Pixels
        /// </summary>
        /// <param name="pixelList">First Pixel</param>
        /// <param name="endPixel">Second Pixel</param>
        public ColorTable(Pixel pixelList, Pixel endPixel, int force = 1)
        {
            // Get difference of variation between the two Pixels
            int diffR = pixelList.RI - endPixel.RI;
            int diffG = pixelList.GI - endPixel.GI;
            int diffB = pixelList.BI - endPixel.BI;

            // Get the maximum number of iterations from the maximum difference between Pixels
            int iter = Math.Max(Math.Abs(diffR), Math.Max(Math.Abs(diffG), Math.Abs(diffB)));

            List<Pixel> tab = new();

            // Create the Pixel scale
            for (int i = 0; i < iter/force; i++)
            {
                tab.Add(new Pixel(pixelList.RI - i * force * diffR/iter, pixelList.GI - i * force * diffG/iter, pixelList.BI - i * force * diffB/iter));
            }

            this.table = tab.ToArray();
        }

        /// <summary>
        /// Pixel scale between n Pixels
        /// </summary>
        /// <param name="pixelList">Array with n Pixel</param>
        public ColorTable(Pixel[] pixelList, int force = 1)
        {
            List<Pixel> tab = new();
            
            // Repear for each couple of Pixel
            for(int pixel = 0; pixel < pixelList.Length - 1; pixel++)
            {
                // Get difference of variation between two Pixels
                int diffR = pixelList[pixel].RI - pixelList[pixel+1].RI;
                int diffG = pixelList[pixel].GI - pixelList[pixel+1].GI;
                int diffB = pixelList[pixel].BI - pixelList[pixel+1].BI;

                // Get the maximum number of iterations from the maximum difference between Pixels
                int iter = Math.Max(Math.Abs(diffR), Math.Max(Math.Abs(diffG), Math.Abs(diffB)));

                // Create the Pixel scale
                for (int i = 0; i < iter/force; i++)
                {
                    tab.Add(new Pixel(pixelList[pixel].RI - i * force *  diffR / iter, pixelList[pixel].GI - i * force * diffG / iter, pixelList[pixel].BI - i * force * diffB / iter));
                }
            }
            this.table = tab.ToArray();
        }
    }
}