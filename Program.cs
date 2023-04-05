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

        public static Pixel[,] convolution(Pixel[,]image, int[,] matrice){
            Pixel[,] newImage = new Pixel[image.GetLength(0),image.GetLength(1)];
            for (int j = 0; j < image.GetLength(0); j++){
                for (int i = 0; i < image.GetLength(1); i++){
                    newImage[i,j] = convolution1Pixel(image, matrice,i,j);
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
                    if (j+y>=0 && j+y<image.GetLength(0) && i+x>=0 && i+x<image.GetLength(1)){
                        R += (byte)(image[i+x,j+y].R * matrice[i+1,j+1]);
                        G += (byte)(image[i+x,j+y].G * matrice[i+1,j+1]);
                        B += (byte)(image[i+x,j+y].B * matrice[i+1,j+1]);
                        value+=matrice[i+1,j+1];
                    }
                }
            }

            //divide the value of newPixel by value
            value=value==0?1:value;
            newPixel.R = R>=0?Convert.ToByte(R/value): (byte) 0;
            newPixel.G = G>=0?Convert.ToByte(G/value): (byte) 0;
            newPixel.B = B>=0?Convert.ToByte(B/value): (byte) 0;

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

        static void AfficherMatriceByteRouge(PixelJPG[,] matrice)
        {
            for(int i = 0; i < matrice.GetLength(0); i++)
            {
                for(int j = 0; j < matrice.GetLength(1); j++)
                {
                    if(matrice[i,j].Y < 10)
                    {
                        Console.Write($" {matrice[i,j].Y} ");
                    }
                    else
                    {
                        Console.Write(matrice[i,j].Y + " ");
                    }
                }
                Console.WriteLine();
            }
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