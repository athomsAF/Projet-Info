using System.IO;
using static System.Console;
using static projects.Pixel;
using static projects.PixelJPG;

namespace projects
{
    public class BitMap
    {
        // Image in pixels : R G B
        public Pixel[,] ImagePixel { get; set;}

        // Image Properties
        public int[] Dimensions {get; set; }
        public string Type {get; set;}
        public int Size {get; set;}
        public int Offset{get; set;}
        public int BitsParCouleur {get;set;}

        /// <summary>
        /// Bitmap default contructor 200x100 canvas
        /// </summary>
        public BitMap()
        {
            // Height / Witdh of the image
            this.Dimensions = new int[2];
            this.Dimensions[1] = 200;
            this.Dimensions[0] = 100;
            // Other images properties
            this.Type = "BMP";
            this.Size = Dimensions[0] * Dimensions[1] * 3 + 54;
            this.Offset = 54;
            this.BitsParCouleur = 24;

            // Convert the list ImageByte to a matrix of pixels ImagePixel
            this.ImagePixel = new Pixel[Dimensions[0], Dimensions[1]];
        }

        /// <summary>
        /// Bitmap main constructor
        /// </summary>
        /// <param name="Path">current path of the iamge</param>
        public BitMap(string Path)
        {
            bool error = false;

            try
            {
                File.Open(Path, FileMode.Open).Close();

                if (Path.EndsWith(".bmp"))
                {
                    // File content
                    byte[] myfile = File.ReadAllBytes(Path);

                    // Image data in bytes
                    byte[] Header = myfile.Take(14).ToArray();
                    byte[] ImageInfo = myfile.Where((x, i) => i >= 14 && i < 54).ToArray();
                    byte[] ImageByte = myfile.Skip(54).ToArray();

                    // Height / Witdh of the image
                    this.Dimensions = new int[2];
                    this.Dimensions[1] = ConvertirEndianToInt(ImageInfo.Skip(4).Take(4).ToArray()); // Width image : bytes de 4 a 8 de ImageInfo
                    this.Dimensions[0] = ConvertirEndianToInt(ImageInfo.Skip(8).Take(4).ToArray()); // Height Image : bytes de 8 a 12 de ImageInfo

                    // Other images properties
                    this.Type = (Header[0] == (byte)66 && Header[1] == (byte)77) ? "BMP" : "Autre";
                    this.Size = ConvertirEndianToInt(Header.Skip(2).Take(4).ToArray());
                    this.Offset = ConvertirEndianToInt(Header.Skip(10).Take(4).ToArray());
                    this.BitsParCouleur = ConvertirEndianToInt(ImageInfo.Skip(14).Take(2).ToArray());

                    // Convert the list ImageByte to a matrix of pixels ImagePixel
                    this.ImagePixel = new Pixel[Dimensions[0], Dimensions[1]];
                    var var = 0;
                    for (int i = 0; i < Dimensions[0]; i++)
                    {
                        for (int j = 0; j < Dimensions[1]; j++)
                        {
                            var = (i * Dimensions[1] + j) * 3;
                            this.ImagePixel[Dimensions[0] - i - 1, j] = new Pixel(ImageByte[var], ImageByte[var + 1], ImageByte[var + 2]); // Chain of 3 pixels : R G B
                        }
                    }

                    // Write down the loaded image properties
                    Console.WriteLine(
                        "Nouvelle image chargée : " + Path + "\n" +
                        "Données Headers : Type : " + this.Type + " , Size : " + this.Size + " octets , Offset : " + this.Offset + "\n" +
                        "Données ImageInfo : Hauteur " + this.Dimensions[0] + " pi , Largeur : " + this.Dimensions[1] + " pi , Nbp : " + this.BitsParCouleur
                    );
                }
                else
                {
                    error = true;
                }
            }
            catch (DirectoryNotFoundException e)
            {
                error = true;
                Console.WriteLine("Folder doesn't exists !");
            }
            catch (FileNotFoundException e)
            {
                error = true;
                Console.WriteLine("File doesn't exists !");
            }
            finally
            {
                if(error)
                {
                    Console.WriteLine("Wrong file format");

                    // Height / Witdh of the image
                    this.Dimensions = new int[2];
                    this.Dimensions[1] = 200;
                    this.Dimensions[0] = 100;
                    // Other images properties
                    this.Type = "BMP";
                    this.Size = Dimensions[0] * Dimensions[1] * 3 + 54;
                    this.Offset = 54;
                    this.BitsParCouleur = 24;

                    // Convert the list ImageByte to a matrix of pixels ImagePixel
                    this.ImagePixel = new Pixel[Dimensions[0], Dimensions[1]];
                }
            }
        }

        /// <summary>
        /// Save ImagePixel's value into a file  by updating the header with the new dimensions / file size / and all the byte values of ImagePixel
        /// Default saving folder : imagesOUT 
        /// </summary>
        /// <param name="file">Name of the file</param>
        public void FromImageToFile(string file)
        {
            List<byte> data = new(0);

            // Add bmp file code
            data.Add(66);
            data.Add(77);

            // Update the header data with the final value of ImagePixel
            this.Dimensions = new int[2] { this.ImagePixel.GetLength(0), this.ImagePixel.GetLength(1) };

            this.Size = 3 * this.Dimensions[0] * this.Dimensions[1] + 54 ; // Size of the image * 3 + header size (54 bytes)

            // File total size
            data.AddRange(ConvertirIntToEndian(this.Size, 4).ToList());

            // Add reserved field (4 bytes)
            data.AddRange(ConvertirIntToEndian(0, 4).ToList());

            // Add Offset (4 bytes)
            data.AddRange(ConvertirIntToEndian(this.Offset, 4).ToList());

            // Add Hearder size : 40 (4 bytes)
            data.AddRange(ConvertirIntToEndian(40, 4).ToList());

            // Add Image Width & Height
            data.AddRange(ConvertirIntToEndian(Dimensions[1], 4).ToList());
            data.AddRange(ConvertirIntToEndian(Dimensions[0], 4).ToList());

            // Add number of plans
            data.AddRange(ConvertirIntToEndian(1, 2).ToList());

            // Add bits per color
            data.AddRange(ConvertirIntToEndian(this.BitsParCouleur, 2).ToList());

            // Add copression type (4 bytes)
            data.AddRange(ConvertirIntToEndian(0, 4).ToList());

            // Add image size (4 bytes)
            data.AddRange(ConvertirIntToEndian(this.Size-54, 4).ToList());

            // Add image info (16 bytes)
            data.AddRange(ConvertirIntToEndian(0, 16).ToList());

            // Convert ImagePixel to ImageByte
            for (int row = 0; row < Dimensions[0]; row++)
            {
                for (int col = 0; col < Dimensions[1]; col++)
                {

                    data.Add(this.ImagePixel[Dimensions[0] - row - 1, col].R);
                    data.Add(this.ImagePixel[Dimensions[0] - row - 1, col].G);
                    data.Add(this.ImagePixel[Dimensions[0] - row - 1, col].B);

                }

                // Add the right ammount of null bytes to have a line length multpiple of 4
                if (Dimensions[1] % 4 != 0)
                {
                    for (int k = 0; k < Dimensions[1] % 4; k++)
                    {
                        data.Add(0);
                    }
                }

            }

            File.WriteAllBytes(Program.PROJECT_PATH + $"imagesOUT/{file}.bmp", data.ToArray());

            Console.WriteLine(
                "Nouvelle image sauvegradée " +
                "Données Headers : Type : " + this.Type + " , Size : " + this.Size + " octets , Offset : " + this.Offset + "\n" +
                "Données ImageInfo : Hauteur " + this.Dimensions[0] + " pi , Largeur : " + this.Dimensions[1] + " pi , Nbp : " + this.BitsParCouleur
            );

            Console.WriteLine("New Image Saved !");
        }
        
        /// <summary>
        /// Transform an image to a grey scale
        /// </summary>
        /// <param name="force">Intensity of the transformy</param>
        public void GrayScale(int force = 255)
        {
            Pixel[,] transformed = new Pixel[this.ImagePixel.GetLength(0), this.ImagePixel.GetLength(1)];

            // Loop through each pixel
            for(int row = 0; row < this.ImagePixel.GetLength(0); row++)
            {
                for(int col = 0; col <  this.ImagePixel.GetLength(1); col++)
                {
                    // Adjust the pixel values by computing the average of the 3 components of a pixel
                    int avgValue = force * (this.ImagePixel[row,col].RI + this.ImagePixel[row,col].GI + this.ImagePixel[row,col].BI) / (3 * 255);

                    if(avgValue > 255)
                    {
                        avgValue = 255;
                    }

                    transformed[row,col] = new Pixel(avgValue, avgValue, avgValue);
                }
            }

            this.ImagePixel = transformed;
        }

        /// <summary>
        /// Transform an image to black and white
        /// </summary>
        /// <param name="force">Intensity of the transform</param>
        public void BlackAndWhite(int force = 125)
        {
            Pixel[,] transformed = new Pixel[this.ImagePixel.GetLength(0), this.ImagePixel.GetLength(1)];

            // Loop through each pixel
            for (int row = 0; row < this.ImagePixel.GetLength(0); row++)
            {
                for(int col = 0; col < this.ImagePixel.GetLength(1); col++)
                {
                    // Replace by White if the avg value of the 3 components are under of the limit, replace by black otherwise
                    int avgValue = (this.ImagePixel[row,col].RI + this.ImagePixel[row,col].GI + this.ImagePixel[row,col].BI)/3;

                    if(avgValue < Math.Abs(force))
                    {
                        transformed[row,col] = new Pixel(0, 0, 0);
                    }
                    else
                    {
                        transformed[row,col] = new Pixel(255, 255, 255);
                    }
                }
            }
            this.ImagePixel = transformed;
        }

        /// <summary>
        /// Transform an image to it's transform through a color filter
        /// </summary>
        /// <param name="red">Red intensity</param>
        /// <param name="green">Green intensity</param>
        /// <param name="blue">Blue intensity</param>
        public void ColorFilter(int red = 255, int green = 255, int blue = 255)
        {
            Pixel[,] transformed = new Pixel[this.ImagePixel.GetLength(0), this.ImagePixel.GetLength(1)];

            for(int row = 0; row < this.ImagePixel.GetLength(0); row++)
            {
                for(int col = 0; col < this.ImagePixel.GetLength(1); col++)
                {   
                    transformed[row,col] = new Pixel((int) (((double) this.ImagePixel[row,col].BI * blue)/255.0), (int) (((double) this.ImagePixel[row,col].GI * green)/255.0), (int) (((double) this.ImagePixel[row,col].RI * red)/255.0));
                }
            }

            this.ImagePixel = transformed;
        }

        /// <summary>
        /// Rotate the image by a certain angle counter clockwise and save the resulting matrix in ImagePixel
        /// </summary>
        /// <param name="angle">Angle of rotation in deg</param>
        public void Rotation(int angle)
        {
            double[,] MatriceChangementBaseHorraire = Program.Rotation(angle-90);
            double[,] MatriceChangementBaseAntiHorraire = Program.Rotation((-1) * (angle-90));

            int InitHeigth = this.ImagePixel.GetLength(0);
            int InitWidth = this.ImagePixel.GetLength(1);

            int[] InitCenter = new int[2] { InitHeigth / 2 - 1, InitWidth / 2 - 1 }; // column : x / line : y

            // Get opposite corners position
            int[] CoinSupGb = ChangementBase(InitCenter[0], InitCenter[1], 0, 0, MatriceChangementBaseHorraire);
            int[] CoinSupDb = ChangementBase(InitCenter[0], InitCenter[1], 0, InitWidth - 1, MatriceChangementBaseHorraire);

            // Determine the new dimensions of the image using the previous result
            int MaxHeight = Math.Max(Math.Abs(CoinSupGb[0]) + 1, Math.Abs(CoinSupDb[0]) + 1);
            int MaxWidth = Math.Max(Math.Abs(CoinSupGb[1]) + 1, Math.Abs(CoinSupDb[1]) + 1);

            // Create the new matrix of pixels
            Pixel[,] Rotation = new Pixel[2 * MaxHeight, 2 * MaxWidth];
            int[] RotationCenter = new int[2] { Rotation.GetLength(0) / 2 - 1, Rotation.GetLength(1) / 2 - 1 }; // column : x / line : y

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
                        Rotation[newLine, newColumn] = this.ImagePixel[RotationCoordinates[0], this.ImagePixel.GetLength(1) - RotationCoordinates[1] - 1];
                    }
                    else
                    {
                        Rotation[newLine, newColumn] = new Pixel(0, 0, 0);
                    }
                }
            }

            // Save the result in ImagePixel
            this.ImagePixel = Rotation;
        }

        ///<summary> agrandissement de l'image </summary>
        public void Agrandissement(double coeffAG){
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
                line' = cos(α) line + sin(α) column 
                column' = –sin(α) line + cos(α) column
                |row'| = |cos (α)  sin (α)| * |line + centerline    |
                |col'| = |-sin(α)  cos (α)|   |column + centercolumn|
            */
            int[,] coordinates = Program.MultiplicationMatriceint(BaseChangmentMatrix, new double[2,1] {{column - centerColumn},{line - centerLine}});
            return new int[2] {coordinates[0,0], coordinates[1,0]};
        }
        
        /// <summary>
        /// Convert a byte array to an int
        /// </summary>
        /// <param name="tab">Byte array to be converted</param>
        /// <returns>Resulting integer</returns>
        public static int ConvertirEndianToInt(byte[] tab)
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
        public static byte[] ConvertirIntToEndian(int val, int size)
        {
            byte[] bytes = new byte[size];
            for (int i = 0; i < size; i++)
            {
                bytes[i] = (byte)(val >> i * 8);
            }
            return bytes;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="image1">Root Image</param>
        /// <param name="image2">Image to hide</param>
        /// <returns></returns>
        public static Pixel[,] steganographyEncode(BitMap image1, BitMap image2)
        {
            if (image1.ImagePixel.GetLength(0) != image2.ImagePixel.GetLength(0) || image1.ImagePixel.GetLength(1) != image2.ImagePixel.GetLength(1))
            {
                Console.WriteLine("Error : image1 and image2 are not at the same size");
                return null;
            }
            else
            {
                Program.mainBinary[,] image1Binary = Program.mainBinaryMatrix(image1.ImagePixel);
                Program.mainBinary[,] image2Binary = Program.mainBinaryMatrix(image2.ImagePixel);
                Pixel[,] steganographyImage = new Pixel[image1Binary.GetLength(0), image1Binary.GetLength(1)];
                for (int i = 0; i < image1Binary.GetLength(0); i++)
                {
                    for (int j = 0; j < image1Binary.GetLength(1); j++)
                    {
                        steganographyImage[i, j] = new Pixel();

                        steganographyImage[i, j].B = (byte)Convert.ToInt32((image1Binary[i, j].R + image2Binary[i, j].R).ToString(), 2);
                        steganographyImage[i, j].G = (byte)Convert.ToInt32((image1Binary[i, j].G + image2Binary[i, j].G).ToString(), 2);
                        steganographyImage[i, j].R = (byte)Convert.ToInt32((image1Binary[i, j].B + image2Binary[i, j].B).ToString(), 2);
                    }
                }
                return steganographyImage;
            }
        }

        public static (Pixel[,], Pixel[,]) steganographyDecode(Pixel[,] image)
        {

            Program.LastBinary[,] imageBinaryLast = Program.LastBinaryMatrix(image);
            Program.mainBinary[,] image1Binary = Program.mainBinaryMatrix(image);

            Pixel[,] steganographyImage1 = new Pixel[image1Binary.GetLength(0), image1Binary.GetLength(1)];
            Pixel[,] steganographyImage2 = new Pixel[image1Binary.GetLength(0), image1Binary.GetLength(1)];

            for (int i = 0; i < image1Binary.GetLength(0); i++)
            {
                for (int j = 0; j < image1Binary.GetLength(1); j++)
                {
                    (string, string) separationPixelR = (new string(image1Binary[i, j].R.ToArray()), new string(imageBinaryLast[i, j].R.ToArray()));
                    (string, string) separationPixelG = (new string(image1Binary[i, j].G.ToArray()), new string(imageBinaryLast[i, j].G.ToArray()));
                    (string, string) separationPixelB = (new string(image1Binary[i, j].B.ToArray()), new string(imageBinaryLast[i, j].B.ToArray()));

                    steganographyImage1[i, j] = new Pixel();
                    steganographyImage2[i, j] = new Pixel();

                    steganographyImage1[i, j].R = (byte)Convert.ToInt32(separationPixelR.Item1, 2);
                    steganographyImage1[i, j].G = (byte)Convert.ToInt32(separationPixelG.Item1, 2);
                    steganographyImage1[i, j].B = (byte)Convert.ToInt32(separationPixelB.Item1, 2);

                    steganographyImage2[i, j].R = (byte)Convert.ToInt32(separationPixelR.Item2, 2);
                    steganographyImage2[i, j].G = (byte)Convert.ToInt32(separationPixelG.Item2, 2);
                    steganographyImage2[i, j].B = (byte)Convert.ToInt32(separationPixelB.Item2, 2);
                }
            }

            return (steganographyImage1, steganographyImage2);

        }
        public void convolution(int[,] matrice)
        {
            Pixel[,] newImage = new Pixel[this.ImagePixel.GetLength(0), this.ImagePixel.GetLength(1)];
            for (int j = 0; j < this.ImagePixel.GetLength(1); j++)
            {
                for (int i = 0; i < this.ImagePixel.GetLength(0); i++)
                {
                    newImage[i, j] = Program.convolution1Pixel(this.ImagePixel, matrice, i, j);
                }
            }
            this.ImagePixel = newImage;
        }
    }

}
