using System.IO;

namespace projects
{
    public class BitMap
    {
        // Header information
        public byte[] Header { get; set; }
        public byte[] ImageInfo { get; set; }
        public byte[] Image { get; set; }

        public byte[][] Dimensions {get; set; }

        public string Type {get; set;}
        public int Taille {get; set;}
        public int Offset{get; set;}
        public int BitsParCouleur {get;set;}

        public BitMap(string Path){
            byte[] myfile = File.ReadAllBytes("imagesIN/"+Path+".bmp");
            //
            this.Header= myfile.Take(14).ToArray();
            this.ImageInfo= myfile.Where((x, i) => i >= 14 && i < 54).ToArray();
            this.Image= myfile.Skip(54).ToArray();

            // Creation of the array of the dimensions of the image
            this.Dimensions = new byte[2][];
            //We takes the 4 bytes of the width and height of the image
            this.Dimensions[1] = this.ImageInfo.Skip(4).Take(4).ToArray(); // Largeur image : bytes de 4 a 8 de ImageInfo
            this.Dimensions[0] = this.ImageInfo.Skip(8).Take(4).ToArray(); // Hauteur Image : bytes de 8 a 12 de ImageInfo

            Console.WriteLine(String.Join(" ", this.Header) + "\n" );
            // Initilaisation of the others attributes
            if(true) // this.ImageInfo.Take(2) == {}
            {
                //abc
            }

        }
        

    }
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

}