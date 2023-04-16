using static projects.Pixel;
using static projects.Complexe;

namespace projects
{
    public class Fractal
    {

        public static Pixel[,] Mandelbrot(int h, int w, int maxiter = 1000, double zoom = 1, double decalx = 0, double decaly = 0, ColorTable ct = null)
        {
            double realzoom = 4.0 / (zoom * Math.Pow(10, zoom - 1));

            if(ct == null)
            {
                ct = new ColorTable();
            }

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
                        Color c = ct.table[0];
                        mandelbrot[lig, col] = new Pixel(c.RB, c.GB, c.BB);
                    }
                }
            }
            return mandelbrot;
        }

        public static Pixel[,] Julia (int h, int w, int maxiter = 1000, double zoom = 1, double decalx = 0, double decaly = 0, double seedX = 0, double seedY = 0, ColorTable ct = null)
        {
            // -0.7 / 0.27015;
            double zx, zy, tmp;
            int i;

            if (ct == null)
            {
                ct = new ColorTable();
            }

            var julia = new Pixel[h, w];
            for (int lig = 0; lig < h; lig++)
            {
                for (int col = 0; col < w; col++)
                {
                    zx = 1.5 * (lig - h / 2) / (0.5 * zoom * h) + decalx;
                    zy = 1.0 * (col - w / 2) / (0.5 * zoom * w) + decaly;
                    i = 0;
                    while (zx * zx + zy * zy < 4 && i < maxiter)
                    {
                        tmp = zx * zx - zy * zy + seedX;
                        zy = 2.0 * zx * zy + seedY;
                        zx = tmp;
                        i += 1;
                    }

                    if(i < maxiter)
                    {
                        Color c = ct.table[i % ct.table.Length];
                        julia[lig, col] = new Pixel(c.RB, c.GB, c.BB);
                    }
                    else
                    {
                        Color c = new Color(0,0,0);
                        julia[lig, col] = new Pixel(c.RB, c.GB, c.BB);
                    }
                }
            }

            return julia;
        }
        /*
        public static Pixel[,] Newton(int h, int w, int maxiter = 1000, double zoom = 1)
        {
            double zx, zy, tmp;
            int i;
            int a = 1;
            ColorTable ct= new ColorTable(new Color[] { new Color(0, 0, 0), new Color(255, 0, 0), new Color(255, 255, 0), new Color(0, 255, 255), new Color(0, 255, 0) });

            var newton = new Pixel[h, w];
            for (int lig = 0; lig < h; lig++)
            {
                for (int col = 0; col < w; col++)
                {
                    zx = 1.5 * (lig - h / 2) / (0.5 * zoom * h);
                    zy = 1.0 * (col - w / 2) / (0.5 * zoom * w);
                    i = 0;
                    Complexe z = new Complexe(zx, zy);
                    while (z.Mod() < 4 && i < maxiter)
                    {
                        z = Complexe.Add(z, Complexe.Mult(new Complexe(-1 * a, 0), Complexe.Add(Complexe.Div(z, new Complexe(10, 0)), Complexe.Div(new Complexe(-1, 0), Complexe.Mult(new Complexe(10, 0), Complexe.Pow(z, 9))))));
                        i += 1;
                    }

                    if (i < maxiter)
                    {
                        Color c = ct.table[i % ct.table.Length];
                        newton[lig, col] = new Pixel(c.RB, c.GB, c.BB);
                    }
                    else
                    {
                        Color c = new Color(0, 0, 0);
                        newton[lig, col] = new Pixel(c.RB, c.GB, c.BB);
                    }
                }
            }

            return newton;
        }
        */
    }
}