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
        public int R;
        public int G;
        public int B;

        public int[] GetColorInt { get { return new int[] {this.R, this.G, this.B}; }}

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


    }
}