using static System.Console;
using static System.BitConverter;
using System.Diagnostics;


namespace projects;
public class Program
{
    static void Main(string[] args)
    {
        string[] files = {"Test","lac","lena","coco"};
        // create BitMap[] IMAGES for each file in files
        
        BitMap[] IMAGES = files.Select(f => new BitMap(f)).ToArray();

        AfficherMatriceByteRouge(IMAGES[0].ImagePixel);
        IMAGES[0].Rotation(20);
        AfficherMatriceByteRouge(IMAGES[0].ImagePixel);
        IMAGES[0].FromImageToFile("TestRot20");

        /*


        Pixel pixel = new Pixel((byte) 0,(byte) 0,(byte) 0);
        Console.WriteLine(string.Join(" ", Rotation(0).Cast<double>()));
        int[] matrice = IMAGES[0].Dimensions;
        //create a new matrice 2 by 3 with matrice[0], - matrice[1] ; matrice[0], matrice[1] ; -matrice[0], matrice[1]
        double [,] newMatrice = { {-matrice[0],matrice[0], matrice[0]}, {matrice[1],matrice[1], -matrice[1]} };

        double[,] results =  MultiplicationMatrice(Rotation(45),(double[,])newMatrice);
        Console.WriteLine(string.Join(" ", results.Cast<double>()));
        */
        

    }
    public static int[,] MultiplicationMatrice(double[,] matrice1, double[,] matrice2)
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

    static void AfficherMatriceByteRouge(Pixel[,] matrice)
    {
        for(int i = 0; i < matrice.GetLength(0); i++)
        {
            for(int j = 0; j < matrice.GetLength(1); j++)
            {
                if(matrice[i,j].R < 10)
                {
                    Console.Write($" {matrice[i,j].R} ");
                }
                else
                {
                    Console.Write(matrice[i,j].R + " ");
                }

            }

            Console.WriteLine();
        }
    }
}
