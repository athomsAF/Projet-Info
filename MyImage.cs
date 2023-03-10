using System.IO;
using static System.Console;
using static System.BitConverter;

namespace projects
{
    public class BitMap
    {
        // Header informations
        public byte[] Header { get; set; }
        public byte[] ImageInfo { get; set; }
        public byte[] ImageByte { get; set; }

        // Final Image in pixels : R G B
        public Pixel[,] ImagePixel { get; set;}

        // Image Properties
        public int[] Dimensions {get; set; }
        public string Type {get; set;}
        public int Size {get; set;}
        public int Offset{get; set;}
        public int BitsParCouleur {get;set;}

        public BitMap(string Path)
        {
            // File content
            byte[] myfile = File.ReadAllBytes("imagesIN/"+Path+".bmp");

            // Image data in bytes
            this.Header= myfile.Take(14).ToArray();
            this.ImageInfo= myfile.Where((x, i) => i >= 14 && i < 54).ToArray();
            this.ImageByte = myfile.Skip(54).ToArray();

            // Height / Witdh of the image
            this.Dimensions = new int[2];
            this.Dimensions[1] = ToInt32(this.ImageInfo.Skip(4).Take(4).ToArray()); // Width image : bytes de 4 a 8 de ImageInfo
            this.Dimensions[0] = ToInt32(this.ImageInfo.Skip(8).Take(4).ToArray()); // Height Image : bytes de 8 a 12 de ImageInfo

            // Other images properties
            this.Type = (this.Header[0] == (byte) 66 && this.Header[1] == (byte) 77)? "BMP" : "Autre";
            this.Size = ToInt32(this.Header.Skip(2).Take(4).ToArray());
            this.Offset = ToInt32(this.Header.Skip(10).Take(4).ToArray());
            this.BitsParCouleur = ToInt16(this.ImageInfo.Skip(14).Take(2).ToArray());

            // Convert the list ImageByte to a matrix of pixels ImagePixel
            this.ImagePixel = new Pixel[Dimensions[0], Dimensions[1]];
            var var = 0;
            for (int i = 0; i < Dimensions[0]; i++)
            {
                for (int j = 0; j < Dimensions[1]; j++)
                {
                    var =  (i * Dimensions[1] + j) * 3; 
                    this.ImagePixel[Dimensions[0] - i - 1, j] = new Pixel(this.ImageByte[var], this.ImageByte[var+1], this.ImageByte[var+2]); // Chain of 3 pixels : R G B
                }
            }

            // Write down the loaded image properties
            Console.WriteLine(
                "Nouvelle image charg??e : " + Path + "\n" +
                "Donn??es Headers : Type : " + this.Type + " , Size : " + this.Size + " octets , Offset : " + this.Offset + "\n" +
                "Donn??es ImageInfo : Hauteur " + this.Dimensions[0] + " pi , Largeur : " + this.Dimensions[1] + " pi , Nbp : " + this.BitsParCouleur
            );
        }

        /// <summary>
        /// Save ImagePixel's value into a file  by updating the header with the new dimensions / file size / and all the byte values of ImagePixel
        /// Default saving folder : imagesOUT 
        /// </summary>
        /// <param name="file">Name of the file</param>
        public void FromImageToFile(string file)
        {
            // Update the header data with the final value of ImagePixel
            this.Dimensions = new int[2] { this.ImagePixel.GetLength(0), this.ImagePixel.GetLength(1) };

            this.Size = 3 * this.Dimensions[0] * this.Dimensions[1] + 54 ; // Size of the image * 3 + header size (54 bytes)

            int index = 2;
            foreach (byte size in ConvertirIntToEndian(this.Size, 4))
            {
                this.Header[index] = size;
                index++;
            }

            // Update Image Info with final values
            index = 4;
            foreach (byte InitWidth in ConvertirIntToEndian(Dimensions[1], 4))
            {
                this.ImageInfo[index] = InitWidth;
                index++;
            }

            foreach (byte InitHeigth in ConvertirIntToEndian(Dimensions[0], 4))
            {
                this.ImageInfo[index] = InitHeigth;
                index++;
            }

            // Convert ImagePixel to ImageByte
            List<byte> image = new List<byte>();
            for (int i = 0; i < Dimensions[0]; i++)
            {
                for (int j = 0; j < Dimensions[1]; j++)
                {
                    image.Add(this.ImagePixel[Dimensions[0] - i - 1, j].B);
                    image.Add(this.ImagePixel[Dimensions[0] - i - 1, j].G);
                    image.Add(this.ImagePixel[Dimensions[0] - i - 1,j].R);

                }

                // Add the right ammount of null bytes to have a line length multpiple of 4
                if (Dimensions[1] % 4 != 0 && Dimensions[1] % 4 != 2)
                {
                    for (int k = 0; k < 3 -  Dimensions[1] % 4; k++)
                    {
                        image.Add((byte)0);
                        image.Add((byte)0);
                        image.Add((byte)0);
                    }
                }

                // Parcicular case
                if (Dimensions[1] % 4 == 2)
                {
                    image.Add((byte)0);
                    image.Add((byte)0);
                }

            }
            this.ImageByte = image.ToArray();

            // Concat all data in a signle line of bytes & create the new bmp file
            byte[] data = this.Header.Concat(this.ImageInfo.Concat(this.ImageByte)).ToArray();

            File.WriteAllBytes($"imagesOUT/{file}.bmp", data);

            Console.WriteLine("New Image Saved !");
        }
        
        public void NuanceGris(int force = 255)
        {
            Pixel[,] imageFinale = new Pixel[Dimensions[0], Dimensions[1]];

            for(int i = 0; i < Dimensions[0]; i++)
            {
                for(int j = 0; j < Dimensions[1]; j++)
                {
                    int moyennePixel = (int) ((int) this.ImagePixel[i,j].R + (int) this.ImagePixel[i,j].G + (int) this.ImagePixel[i,j].B)/3;
                    moyennePixel *= force;
                    moyennePixel /= 255;

                    if(moyennePixel > 255)
                    {
                        moyennePixel = 255;
                    }

                    imageFinale[i,j] = new Pixel((byte) moyennePixel,(byte) moyennePixel, (byte) moyennePixel);
                }
            }

            this.ImagePixel = imageFinale;
        }

        public void NoirEtBlanc(int force = 125)
        {
            Pixel[,] imageFinale = new Pixel[Dimensions[0], Dimensions[1]];

            for(int i = 0; i < Dimensions[0]; i++)
            {
                for(int j = 0; j < Dimensions[1]; j++)
                {   
                    int moyennePixel = (int) ((int) this.ImagePixel[i,j].R + (int) this.ImagePixel[i,j].G + (int) this.ImagePixel[i,j].B)/3;
                    if(moyennePixel < Math.Abs(force))
                    {
                        imageFinale[i,j] = new Pixel((byte) 0,(byte) 0, (byte) 0);
                    }
                    else
                    {
                        imageFinale[i,j] = new Pixel((byte) 255, (byte) 255, (byte) 255);
                    }
                }
            }
            this.ImagePixel = imageFinale;
        }

        public void ChoixChromatique(int rouge = 255, int vert = 255, int bleu = 255)
        {
            Pixel[,] imageFinale = new Pixel[Dimensions[0], Dimensions[1]];

            for(int i = 0; i < Dimensions[0]; i++)
            {
                for(int j = 0; j < Dimensions[1]; j++)
                {   
                    imageFinale[i,j] = new Pixel((byte) (int)(((int) this.ImagePixel[i,j].R) * (rouge/255)), (byte) (int) (((int) this.ImagePixel[i,j].G) * (vert/255)), (byte) (int) (((int) this.ImagePixel[i,j].B) * (bleu/255)));
                }
            }
            this.ImagePixel = imageFinale;
        }

        /// <summary>
        /// Rotate ImagePixel by a certain angle counter clockwise and save the resulting matrix in ImagePixel
        /// </summary>
        /// <param name="angle">Angle of rotation in deg</param>
        public void Rotation(int angle)
        {
            double[,] MatriceChangementBaseHorraire = Program.Rotation(angle-90);
            double[,] MatriceChangementBaseAntiHorraire = Program.Rotation((-1) * (angle-90));

            int InitHeigth = this.ImagePixel.GetLength(0);
            int InitWidth = this.ImagePixel.GetLength(1);

            int[] InitCenter = new int[2] { InitHeigth / 2 - 1, InitWidth / 2 - 1 }; // Colonne : x / Ligne : y | 0,0

            // Get opposite corners position
            int[] CoinSupGb = ChangementBase(InitCenter[0], InitCenter[1], 0, 0, MatriceChangementBaseHorraire);
            int[] CoinSupDb = ChangementBase(InitCenter[0], InitCenter[1], 0, InitWidth - 1, MatriceChangementBaseHorraire);

            // Determine the new dimensions of the image using the previous result
            int MaxHeight = Math.Max(Math.Abs(CoinSupGb[0]) + 1, Math.Abs(CoinSupDb[0]) + 1);
            int MaxWidth = Math.Max(Math.Abs(CoinSupGb[1]) + 1, Math.Abs(CoinSupDb[1]) + 1);

            // Create the new matrix of pixels
            Pixel[,] Rotation = new Pixel[2 * MaxHeight, 2 * MaxWidth];
            int[] RotationCenter = new int[2] { Rotation.GetLength(0) / 2 - 1, Rotation.GetLength(1) / 2 - 1 }; // Colonne : x / Ligne : y | 0,0

            // For each pixel in the new matrix, calulate the corresponding pixel in the inital matrix by using a base changment
            for (int newLine = 0; newLine < Rotation.GetLength(0); newLine++)
            {
                for (int newColumn = 0; newColumn < Rotation.GetLength(1); newColumn++)
                {
                    int[] RotationCoordinates = ChangementBase(RotationCenter[0], RotationCenter[1], newLine, newColumn, MatriceChangementBaseAntiHorraire);
                    RotationCoordinates[0] += InitCenter[0]; // Line
                    RotationCoordinates[1] += InitCenter[1]; // Column

                    if (!(RotationCoordinates[0] < 0 || RotationCoordinates[1] < 0 || RotationCoordinates[0] >= this.ImagePixel.GetLength(0) || RotationCoordinates[1] >= this.ImagePixel.GetLength(1)))
                    {
                        Rotation[newLine, newColumn] = this.ImagePixel[RotationCoordinates[0], Dimensions[1] - RotationCoordinates[1] - 1]; //this.ImagePixel[RotationCoordinates[0], RotationCoordinates[1]]; 
                    }
                    else
                    {
                        Rotation[newLine, newColumn] = new Pixel((byte)0, (byte)0, (byte)0);
                    }

                }
            }

            // Save the result in ImagePixel
            this.ImagePixel = Rotation;
        }

        ///<summary> agrandissement de l'image </summary>
        /// 
        /// 
        public void Agrandissement(int coeffAG){
            int height = Convert.ToUInt16( this.ImagePixel.GetLength(0)* coeffAG);
            int width = Convert.ToUInt16( this.ImagePixel.GetLength(1)* coeffAG);
            Pixel[,] AGImage = new Pixel[height,width];
            for (int newLine = 0; newLine < height; newLine++)
            {
                for (int newColumn = 0; newColumn < width; newColumn++)
                {
                    uint oldx = (uint)(newLine / coeffAG);
                    uint oldy = (uint)(newColumn / coeffAG);
                    AGImage[newLine, newColumn] = this.ImagePixel[oldx,oldy]; //this.ImagePixel[RotationCoordinates[0], RotationCoordinates[1]]; 
                }
            }
            this.ImagePixel = AGImage;
        }

        /// <summary>
        /// Calculate the new coordinates of a point by using a base changment
        /// </summary>
        /// <param name="centerLine">Center row coordinate of the initial matrix</param>
        /// <param name="centerColumn">Center column of the initial matrix</param>
        /// <param name="line">Row coordinate of the point to be calculated</param>
        /// <param name="column">Column of the point to be calculated</param>
        /// <param name="BaseChangmentMatrix">Matrix used to change the base</param>
        /// <returns>Projected values of the row / column in a new base (!!! You sould add the new matrix center's coordinates to get the correct values)</returns>
        public int[] ChangementBase(int centerLine, int centerColumn, double line, double column, double[,] BaseChangmentMatrix)
        {
            /* Base changment :
                line' = cos(??) line + sin(??) column 
                column' = ???sin(??) line + cos(??) column
                |row'| = |cos (??)  sin (??)| * |line + centerline    |
                |col'| = |-sin(??)  cos (??)|   |column + centercolumn|
            */
            int[,] coordinates = Program.MultiplicationMatriceint(BaseChangmentMatrix, new double[2,1] {{column - centerColumn},{line - centerLine}});
            return new int[2] {coordinates[0,0], coordinates[1,0]};
        }
        
        /// <summary>
        /// Convert a byte array to an int
        /// </summary>
        /// <param name="tab">Byte array to be converted</param>
        /// <returns>Resulting integer</returns>
        public int ConvertirEndianToInt(byte[] tab)
        {
            int value = 0;

            for (int i = 0; i < tab.Length; i++)
            {
                value |= (tab[i] << i * 8);
            }

            return value;
        }

        /// <summary>
        /// Convert an int to a byte array
        /// </summary>
        /// <param name="val">Integer to be converted</param>
        /// <param name="size">Size of the resulting array</param>
        /// <returns>Resulting array</returns>
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
