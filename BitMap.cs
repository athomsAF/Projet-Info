using System.IO;
using static System.Console;
using static System.BitConverter;

namespace projects
{
    public class BitMap
    {
        // Header information
        public byte[] Header { get; set; }
        public byte[] ImageInfo { get; set; }
        public byte[] ImageByte { get; set; }
        public Pixel[,] ImagePixel { get; set;}
        public int[] Dimensions {get; set; }
        public string Type {get; set;}
        public int Taille {get; set;}
        public int Offset{get; set;}
        public int BitsParCouleur {get;set;}

        public BitMap(string Path)
        {
            byte[] myfile = File.ReadAllBytes("imagesIN/"+Path+".bmp");
            //
            this.Header= myfile.Take(14).ToArray();
            this.ImageInfo= myfile.Where((x, i) => i >= 14 && i < 54).ToArray();
            this.ImageByte = myfile.Skip(54).ToArray();

            // Creation of the array of the dimensions of the image
            this.Dimensions = new int[2];
            //We takes the 4 bytes of the width and height of the image
            this.Dimensions[1] = ToInt32(this.ImageInfo.Skip(4).Take(4).ToArray()); // Largeur image : bytes de 4 a 8 de ImageInfo
            this.Dimensions[0] = ToInt32(this.ImageInfo.Skip(8).Take(4).ToArray()); // Hauteur Image : bytes de 8 a 12 de ImageInfo

            // Initilaisation of the others attributes
            this.Type=(this.Header[0] == (byte) 66 && this.Header[1] == (byte) 77)? "BMP" : "Autre";

            this.Taille = ToInt32(this.Header.Skip(2).Take(4).ToArray());

            this.Offset = ToInt32(this.Header.Skip(10).Take(4).ToArray());

            this.BitsParCouleur = ToInt16(this.ImageInfo.Skip(14).Take(2).ToArray());

            // Conversion of the byte values of the image to a more exploitable image with rgb values
            
            Pixel[,] matrix = new Pixel[Dimensions[0], Dimensions[1]];
            var var = 0;
            for (int i = 0; i < Dimensions[0]; i++)
            {
                for (int j = 0; j < Dimensions[1]; j++)
                {
                    var=  i*(Dimensions[0]+3)+j*3; // ancien : this.ImageByte[i*(Dimensions[0]+3)+j*3]; je ne pense pas que ça aie un sens de chercher un byte depuis la liste
                    matrix[i, j] = new Pixel(this.ImageByte[var], this.ImageByte[var+1], this.ImageByte[var+2]);
                }
            }

            Console.WriteLine(
                "Nouvelle image chargée : " + Path + "\n" +
                "Données Headers : Type : " + this.Type + " , Taille : " + this.Taille + " octets , Offset : " + this.Offset + "\n" +
                "Données ImageInfo : Hauteur " + this.Dimensions[0] + " pi , Largeur : " + this.Dimensions[1] + " pi , Nbp : " + this.BitsParCouleur
            );

            //write all value in matrix
            WriteLine(matrix[Dimensions[0]-1,Dimensions[1]-1]);
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


