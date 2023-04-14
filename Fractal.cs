using static projects.Pixel;
using static projects.Complexe;

namespace projects
{
    public class Fractal
    {

        public static Pixel[,] Mandelbrot(int h, int w, int maxiter, double zoom = 1, double decalx = 0, double decaly = 0)
        {
            double realzoom = 4.0 / (zoom * Math.Pow(10, zoom));
            ColorTable ct = new ColorTable(new Color[] { new Color(0, 0, 0), new Color(255, 0, 0), new Color(255, 255, 0), new Color(0, 255, 255), new Color(0, 255, 0) });

            Pixel[,] mandelbrot = new Pixel[h, w];

            for(int lig = 0; lig < h; lig++)
            {
                for(int col = 0; col < w; col++)
                {
                    double c_re = (col - w / 2.0) * realzoom / w + decalx;
                    double c_im = (lig - h / 2.0) * realzoom / h + decaly;

                    int iter = 0;
                    double x = 0, y = 0;

                    while (iter < maxiter && ((x*x) + (y*y)) <= 4)
                    {
                        double x_temp = (x * x) - (y * y) + c_re;
                        y = 2 * y * x + c_im;
                        x = x_temp;
                        iter++;
                    }

                    if(iter < maxiter)
                    {
                        Color c = ct.table[iter % ct.table.Length];
                        mandelbrot[lig, col] = new Pixel(c.RB, c.GB, c.BB);

                    }
                    else
                    {
                        Color c = new Color(0, 0, 0);
                        mandelbrot[lig, col] = new Pixel(c.RB, c.GB, c.BB);
                    }
                }
            }
            return mandelbrot;
        }
    }
}