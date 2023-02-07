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
        public int Size {get; set;}
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

            this.Size = ToInt32(this.Header.Skip(2).Take(4).ToArray());

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
                "Données Headers : Type : " + this.Type + " , Size : " + this.Size + " octets , Offset : " + this.Offset + "\n" +
                "Données ImageInfo : Hauteur " + this.Dimensions[0] + " pi , Largeur : " + this.Dimensions[1] + " pi , Nbp : " + this.BitsParCouleur
            );

            //write all value in matrix
            WriteLine(matrix[Dimensions[0]-1,Dimensions[1]-1]);
        }


        public void FromImageToFile(string file)
        {
            byte[] data = this.Header.Concat(this.ImageInfo.Concat(this.ImageByte).ToArray()).ToArray();
            // File.WriteAllBytes("imagesOut/"+file+".bmp", );

            Console.WriteLine("Saved", data);
        }

        
        public Pixel[,] Rotation(int angle)
        {
            int width = this.ImageByte.GetLength(1);
            int height = this.ImageByte.GetLength(0);
            int[] center  = new int[2] {width/2-1, height/2-1}; // Colonne : x / Ligne : y | 0,0

            // Get corners position
            int[] CoinSupGb = ChangementBase(center[0], center[1], 0, 0, angle);
            int[] CoinSupDb = ChangementBase(center[0], center[1], width - 1, 0, angle);
            int[] CoinInfGb = ChangementBase(center[0],center[1] , 0 , height - 1, angle);
            int[] CoinInfDb = ChangementBase(center[0], center[1], width - 1, height - 1, angle);

            int MaxHeight = Enumerable.Max(new int[4] {CoinSupGb[0], CoinSupDb[0], CoinInfGb[0], CoinInfDb[0]});
            int MinHeight = Enumerable.Min(new int[4] {CoinSupGb[0], CoinSupDb[0], CoinInfGb[0], CoinInfDb[0]});
            int MaxWidth = Enumerable.Max(new int[4] {CoinSupGb[1], CoinSupDb[1], CoinInfGb[1], CoinInfDb[1]});
            int MinWidth = Enumerable.Min(new int[4] {CoinSupGb[1], CoinSupDb[1], CoinInfGb[1], CoinInfDb[1]});

            Pixel[,] rotation = new Pixel[MaxHeight - MinHeight, MaxWidth - MinWidth];
            int[] center2  = new int[2] {rotation.GetLength(1)/2-1, rotation.GetLength(0)/2-1}; // Colonne : x / Ligne : y | 0,0

            for(int i = 0; i < height; i++)
            {
                for(int j = 0; j < width; j++)
                {
                    // Coordonnées base 1 : j : x , i : y
                    // hf1 = d1*cos(angle)
                    // hf2 = d2*cos(angle)
                    int[] cb = ChangementBase(center[0], center[1], j, i, angle); 
                    cb[0] += center2[0];
                    cb[1] += center2[1];

                    rotation[cb[1], cb[0]] = this.ImagePixel[i,j];
                }
            }

            return rotation;
        }

        public int[] ChangementBase(int centrex, int centrey, int x, int y, int angle)
        {
            
            int angleRad = (int) (angle * Math.PI) / 180;
            int distFromCenter = (int) Math.Sqrt(Math.Pow((x-centrex),2) + Math.Pow((y-centrey),2));            //r = √(x2 – x1)2 + (y2 – y1)2
            int newx = (int) (Math.Cos(angle) * (x - centrex) + Math.Sin(angleRad) * (y - centrey));            //e'1 = cos(α) e1 + sin(α) e2 ;
            int newy = (int) (- Math.Sin(angle) * (x - centrex) + Math.Cos(angle) * (y - centrey));             //e'2 = –sin(α) e1 + cos(α) e2 ;
            
            return new int[2] {newx, newy};
        }
        

        public int ConvertirEndianToInt(byte[] tab)
        {
            int value = 0;

            for (int i = 0; i < tab.Length; i++)
            {
                value |= (tab[i] << i * 8);
            }

            return value;
        }

        //encode int to little endian in a function for a size of n bites
        public byte[] ConvertirIntToEndian(int val, int size)
        {
            byte[] bytes = new byte[size];
            for (int i = 0; i < size; i++)
            {
                bytes[i] = (byte)(val >> i * 8);
            }
            return bytes;
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


