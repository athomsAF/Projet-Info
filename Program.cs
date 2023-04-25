using static projects.Fractal;
using static projects.Form1;
using static projects.BitMap;
using static projects.Pixel;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Xml;
using System.Runtime.CompilerServices;

namespace projects
{
    public static class Program
    {
        public static string PROJECT_PATH = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\"));
        public static bool INTERFACE = !false;
        public static Dictionary<string, Dictionary<string, double[]>> ?DATA;
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            DATA = JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, double[]>>>(File.ReadAllText("../../../data.json"));

            if(DATA == null)
            {
                DATA = new Dictionary<string, Dictionary<string, double[]>>();
            }

            if(DATA.Keys.Count == 0)
            {
                DATA["Fractal"] = new Dictionary<string, double[]>();
                DATA["Convolution"] = new Dictionary<string, double[]>();
            }

            if(INTERFACE)
            {
                Console.WriteLine(PROJECT_PATH);

                ApplicationConfiguration.Initialize();
                Application.Run(new Form1());
            }
            else
            {
                Main2();
            }

            var jsonString = JsonSerializer.Serialize(DATA, new JsonSerializerOptions { WriteIndented = true, });
            File.WriteAllText("../../../data.json", jsonString);

        }
        public static void Main2()
        {
            /*
            string[] files = {"Test","lac","lena","coco"};
            // create BitMap[] IMAGES for each file in files

            
            BitMap[] IMAGES = files.Select(f => new BitMap(f)).ToArray();
            BitMap image = IMAGES[0];


            int[,]matriceConv = new int[3,3]{{1,2,1},
                                             {2,5,2},
                                             {1,2,1}};
            int[,]RBords = new int[3,3]{{0,0,0},
                                        {-1,1,0},
                                        {0,0,0}};
            int[,]DContours = new int[3,3]{{0,-1,0},
                                           {-1,4,-1},
                                           {0,-1,0}};
            int[,]DRepoussage = new int[3,3]{{-2,-1,0},
                                             {-1,1,1},
                                             {0,-1,2}};
            AfficherMatriceByteRouge(jpg(image));
            */

            // BitMap image = new BitMap("lena");

            /*

            Noeud A = new Noeud(new Color(1,1,1), 1, null, null);
            Noeud B = new Noeud(new Color(3, 3, 3), 2, null, null);
            Noeud CA = new Noeud(new Color(4, 4, 4), 3, null, null);
            Noeud CB = new Noeud(new Color(5, 5, 5), 4, null, null);
            Noeud C = new Noeud(null, 5, CA, CB);
            // Noeud D = new Noeud(null, 6, A, B);
            Noeud E = new Noeud(null, 7, A, C);

            Huffman huf = new Huffman(E);

            foreach(var elt in huf.TreeEncode(huf.root))
            {
                Console.WriteLine(elt);
            }
            */

            /*
            BitMap image = new BitMap("coco");
            BitMap image2 = new BitMap("coco");
            image2.NoirEtBlanc();
            Pixel[,] imgnoir = image.ImagePixel;
            Pixel[,] img2 = BitMap.steganography(image, image2);
            image.ImagePixel = img2;
            image.FromImageToFile("stega");
            */

        }

        public static string Spaces(char c, int n)
        {
            string res = "";
            for (int i = 0; i < n; i++) res += c;
            return res;
        }


        public static PixelJPG[,] jpg(Pixel[,]image){
            PixelJPG[,] newImage = new PixelJPG[image.GetLength(0),image.GetLength(1)];
            for (int j = 0; j < image.GetLength(0); j++){
                for (int i = 0; i < image.GetLength(1); i++){
                    newImage[i,j] = new PixelJPG(image[i,j].R,image[i,j].G,image[i,j].B);
                }
            }
            return(newImage);
        }

        public static Pixel convolution1Pixel(Pixel[,]image, int[,] matrice, int x, int y){
            Pixel newPixel = new Pixel(0,0,0);
            int value=0;
            int R=0, G=0, B=0;
            for (int i=-1;i<=1;i++){
                for (int j=-1;j<=1;j++){
                    if (j+y>=0 && i+x<image.GetLength(0) && i+x>=0 && j+y<image.GetLength(1)){
                        R += image[i+x,j+y].RI * matrice[i+1,j+1];
                        G += image[i+x,j+y].GI * matrice[i+1,j+1];
                        B += image[i+x,j+y].BI * matrice[i+1,j+1];
                        value+=matrice[i+1,j+1];
                    }
                }
            }

            //divide the value of newPixel by value
            value=value==0?1:value;
            R = R>=0?R/value: 0;
            G = G>=0?G/value: 0;
            B = B>=0?B/value: 0;
            newPixel = new Pixel(R, G, B);

            return(newPixel);
        }

        public static int[,] MultiplicationMatriceint(double[,] matrice1, double[,] matrice2)
        {
            int[,] matrice = new int[matrice1.GetLength(0), matrice2.GetLength(1)];
            for(int i = 0; i < matrice1.GetLength(0); i++)
            {
                for(int j = 0; j < matrice2.GetLength(1); j++)
                {
                    for(int k = 0; k < matrice1.GetLength(1); k++)
                    {
                        matrice[i,j] += (int) (matrice1[i,k] * matrice2[k,j]);
                    }
                }
            }
            return matrice;
        }

        static double[,] MultiplicationMatrice(double[,] matrice1, double[,] matrice2)
        {
            double[,] matrice = new double[matrice1.GetLength(0), matrice2.GetLength(1)];
            for(int i = 0; i < matrice1.GetLength(0); i++)
            {
                for(int j = 0; j < matrice2.GetLength(1); j++)
                {
                    for(int k = 0; k < matrice1.GetLength(1); k++)
                    {
                        matrice[i,j] += matrice1[i,k] * matrice2[k,j];
                    }
                }
            }
            return matrice;
        }
        public static double[,] Rotation(float angle)
        {
            double[,] rotation = new double[2,2];
            double angleRad = (angle * Math.PI) / 180;
            rotation[0,0] = (double) Math.Cos(angleRad);
            rotation[0,1] = (double) Math.Sin(angleRad);
            rotation[1,0] = (double) (-1) * Math.Sin(angleRad);
            rotation[1,1] = (double) Math.Cos(angleRad);
            return rotation;
        }

        public static mainBinary[,] mainBinaryMatrix(Pixel[,] image)
        {
            //for all element in image add 1
            mainBinary[,] fimage = new mainBinary[image.GetLength(0), image.GetLength(1)];
            for (int i = 0; i < image.GetLength(0); i++)
            {
                for (int j = 0; j < image.GetLength(1); j++)
                {
                    fimage[i, j] = new mainBinary(image[i, j]);
                }
            }
            return fimage;
        }


        public static string getFourBinary(string str)
        {
            int len = str.Length;
            for (int i = 0; i <= 7 - len; i++)
            {
                str = "0" + str;
            }
            return (str.Substring(0, 4));
        }


        public struct mainBinary
        {
            public string R;
            public string G;
            public string B;
            public mainBinary(Pixel pixel)
            {
                B = Convert.ToString(pixel.R, 2);
                B = getFourBinary(B);
                G = Convert.ToString(pixel.G, 2);
                G = getFourBinary(G);
                R = Convert.ToString(pixel.B, 2);
                R = getFourBinary(R);
            }
        }

        public static string getFourLastBinary(string str)
        {
            int len = str.Length;
            for (int i = 0; i <= 7 - len; i++)
            {
                str = "0" + str;
            }
            return (new string( str.Skip(4).Take(4).ToArray())) ;
        }

        public struct LastBinary
        {
            public string R;
            public string G;
            public string B;
            public LastBinary(Pixel pixel)
            {
                B = Convert.ToString(pixel.R, 2);
                B = getFourLastBinary(B);
                G = Convert.ToString(pixel.G, 2);
                G = getFourLastBinary(G);
                R = Convert.ToString(pixel.B, 2);
                R = getFourLastBinary(R);
            }
        }

        public static LastBinary[,] LastBinaryMatrix(Pixel[,] image)
        { 
            //for all element in image add 1
            LastBinary[,] fimage = new LastBinary[image.GetLength(0), image.GetLength(1)];
            for (int i = 0; i < image.GetLength(0); i++)
            {
                for (int j = 0; j < image.GetLength(1); j++)
                {
                    fimage[i, j] = new LastBinary(image[i, j]);
                }
            }
            return fimage;
        }
    }
}