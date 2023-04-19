using static projects.Pixel;
using static projects.Complexe;

namespace projects
{
    /// <summary>
    /// Fractal
    /// </summary>
    public class Fractal
    {
        /// <summary>
        /// Mandelbrot fractal
        /// </summary>
        /// <param name="height">Height of the fractal</param>
        /// <param name="width">Width of the fractal</param>
        /// <param name="maxiter">Maximum iteration before exiting the loop</param>
        /// <param name="zoom">Zooming value</param>
        /// <param name="decalx">X shift</param>
        /// <param name="decaly">Y shift</param>
        /// <param name="colorTable">Color gradient used for the fractal</param>
        /// <returns>Mandelbrot fractal generated from the given parameters</returns>
        public static Pixel[,] Mandelbrot(int height, int width, int maxiter = 1000, double zoom = 1, double decalx = 0, double decaly = 0, ColorTable ?colorTable = null)
        {
            // Initialisation of all parameters
            double realzoom = 4.0 / (zoom * Math.Pow(10, zoom - 1));

            colorTable ??= new ColorTable();

            Pixel[,] mandelbrot = new Pixel[height, width];

            // Loop through the canvas and compute the level of the pixel in the mandelbrot fractal through their iteration count
            for(int row = 0; row < height; row++)
            {
                for(int col = 0; col < width; col++)
                {
                    double c_re = (col - width / 2.0) * realzoom / width + decalx;
                    double c_im = (row - height / 2.0) * realzoom / height + decaly;

                    int iter = 0;
                    double x = 0, y = 0;

                    // Repeat until the modulus of the complex number exceed the circle radius of 4
                    while (iter < maxiter && ((x*x) + (y*y)) <= 4)
                    {
                        double x_temp = (x * x) - (y * y) + c_re;
                        y = 2 * y * x + c_im;
                        x = x_temp;
                        iter++;
                    }

                    // Set the color using the iterations required to exit the loot
                    Pixel c = colorTable.table.First();
                    mandelbrot[row, col] = new Pixel(c.R, c.G, c.B);

                    if (iter < maxiter)
                    {
                        c = colorTable.table[colorTable.table.Length - 1 - iter % colorTable.table.Length];
                        mandelbrot[row, col] = new Pixel(c.R, c.G, c.B);

                    }
                }
            }
            return mandelbrot;
        }

        /// <summary>
        /// Julia fractal
        /// </summary>
        /// <param name="height">Height of the fractal</param>
        /// <param name="width">Width of the fractal</param>
        /// <param name="maxiter">Maximum iteration before exiting the loop</param>
        /// <param name="zoom">Zooming value</param>
        /// <param name="decalx">X shift</param>
        /// <param name="decaly">Y shift</param>
        /// <param name="seedX">X increment</param>
        /// <param name="seedY">Y increment</param>
        /// <param name="colorTable">Color gradient used for the fractal</param>
        /// <returns>Julia fractal generated from the given parameters</returns>
        public static Pixel[,] Julia (int height, int width, int maxiter = 1000, double zoom = 1, double decalx = 0, double decaly = 0, double seedX = -0.7, double seedY = 0.27015, ColorTable colorTable = null)
        {
            // Initialisation of all parameters
            var julia = new Pixel[height, width];
            double zx, zy, tmp;
            int i;

            if (colorTable == null)
            {
                colorTable = new ColorTable();
            }

            // Loop through the canvas and compute the level of the pixel in the mandelbrot fractal through their iteration count
            for (int row = 0; row < height; row++)
            {
                for (int col = 0; col < width; col++)
                {
                    zx = 1.5 * (row - height / 2) / (0.5 * zoom * height) + decalx;
                    zy = 1.0 * (col - width / 2) / (0.5 * zoom * width) + decaly;
                    i = 0;

                    // Repeat until the modulus of the complex number exceed the circle radius of 4 
                    while (zx * zx + zy * zy < 4 && i < maxiter)
                    {
                        tmp = zx * zx - zy * zy + seedX;
                        zy = 2.0 * zx * zy + seedY;
                        zx = tmp;
                        i += 1;
                    }

                    // Set the color using the iterations required to exit the loot

                    Pixel c = colorTable.table.Last();
                    julia[row, col] = new Pixel(c.R, c.G, c.B);

                    if (i < maxiter)
                    {
                        c = colorTable.table[i % colorTable.table.Length];
                        julia[row, col] = new Pixel(c.R, c.G, c.B);
                    }
                }
            }

            return julia;
        }

        /* Unfinished Newton's fractal
        public static Pixel[,] Newton(int height, int width, int maxiter = 1000, double zoom = 1)
        {
            double zx, zy, tmp;
            int i;
            int a = 1;
            ColorTable colorTable= new ColorTable(new Pixel[] { new Pixel(0, 0, 0), new Pixel(255, 0, 0), new Pixel(255, 255, 0), new Pixel(0, 255, 255), new Pixel(0, 255, 0) });

            var newton = new Pixel[height, width];
            for (int lig = 0; lig < height; lig++)
            {
                for (int col = 0; col < width; col++)
                {
                    zx = 1.5 * (lig - height / 2) / (0.5 * zoom * height);
                    zy = 1.0 * (col - width / 2) / (0.5 * zoom * width);
                    i = 0;
                    Complexe z = new Complexe(zx, zy);
                    while (z.Mod() < 4 && i < maxiter)
                    {
                        z = Complexe.Add(z, Complexe.Mult(new Complexe(-1 * a, 0), Complexe.Add(Complexe.Div(z, new Complexe(10, 0)), Complexe.Div(new Complexe(-1, 0), Complexe.Mult(new Complexe(10, 0), Complexe.Pow(z, 9))))));
                        i += 1;
                    }

                    if (i < maxiter)
                    {
                        Pixel c = colorTable.table[i % colorTable.table.Length];
                        newton[lig, col] = new Pixel(c.RB, c.GB, c.BB);
                    }
                    else
                    {
                        Pixel c = new Pixel(0, 0, 0);
                        newton[lig, col] = new Pixel(c.RB, c.GB, c.BB);
                    }
                }
            }

            return newton;
        }
        */
    }
}