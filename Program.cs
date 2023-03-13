﻿using static System.Console;
using static System.BitConverter;
using System.Diagnostics;


namespace projects{
    public class Program
    {
        static void Main(string[] args)
        {
            string[] files = {"Test","lac","lena","coco"};
            // create BitMap[] IMAGES for each file in files
            
            BitMap[] IMAGES = files.Select(f => new BitMap(f)).ToArray();


            Pixel[,] image = IMAGES[0].ImagePixel;
            AfficherMatriceByteRouge(image);
            int[,]matrice = new int[3,3]{{1,2,1},{2,5,2},{1,2,1}};

            AfficherMatriceByteRouge(convolution(image,matrice));

        }

        public static Pixel[,] convolution(Pixel[,]image, int[,] matrice){
            Pixel[,] newImage = new Pixel[image.GetLength(0),image.GetLength(1)];
            for (int j = 0; j < image.GetLength(0); j++){
                Console.WriteLine("test");
                for (int i = 0; i < image.GetLength(1); i++){
                    Console.Write("n");
                    newImage[i,j] = convolution1Pixel(image, matrice,i,j);
                }
            }
            return(newImage);
        }

        public static Pixel convolution1Pixel(Pixel[,]image, int[,] matrice, int x, int y){
            Pixel newPixel = new Pixel(0,0,0);
            int value=0;
            for (int i=-1;i<=1;i++){
                for (int j=-1;j<=1;j++){
                    Console.WriteLine("test2");
                    if (j+y>=0 && j+y<image.GetLength(0) && i+x>=0 && i+x<image.GetLength(1)){
                        newPixel.R += (byte)(image[i+x,j+y].R * matrice[i+1,j+1]);
                        newPixel.G += (byte)(image[i+x,j+y].G * matrice[i+1,j+1]);
                        newPixel.B += (byte)(image[i+x,j+y].B * matrice[i+1,j+1]);
                        value+=matrice[i+1,j+1];
                    }
                }
            }
            //divide the value of newPixel by value
            newPixel.R = (byte)(newPixel.R/value);
            newPixel.G = (byte)(newPixel.G/value);
            newPixel.B = (byte)(newPixel.B/value);

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
}