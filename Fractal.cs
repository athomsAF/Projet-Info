using static projects.Pixel;
using static projects.Complexe;

namespace projects
{
    public class Factal
    {

        public Pixel[,] Mandelbrot(int xMin, int yMin, int xMax, int yMax, int ystep, int xstep, int xyPixelStep, int kMax, int kLast)
        {
            Pixel[,] mandelbrot = new Pixel[xMax - xMin, yMax - yMin];
            double modulusSquared = 0;

            for (double y = yMin; y < yMax; y += ystep) {
                int xPix = 0, yPix = 0;
                for (double x = xMin; x < xMax; x += xstep) {
                    Complexe c = new Complexe(x, y);
                    Complexe zk = new Complexe(0, 0);
                    int k = 0;
                    
                    do {
                        zk = zk.doCmplxSqPlusConst(c);
                        modulusSquared = zk.doMoulusSq();
                        k++;
                    } while ((modulusSquared <= 4.0) && (k < kMax));
                    
                    /*
                    if (k < kMax) {
                        if (k == kLast) {
                            color = colorLast;
                        } else {
                            color = colourTable.GetColour(k);
                            colorLast = color;
                        }

                        if (xyPixelStep == 1) {
                            if ((xPix < mandelbrot.GetLength(0)) && (yPix >= 0)) {
                                mandelbrot[xPix, yPix] = new Pixel(color);
                            }
                        } else {
                            for (int pX = 0; pX < xyPixelStep; pX++) {
                                for (int pY = 0; pY < xyPixelStep; pY++) {
                                    if (((xPix + pX) < mandelbrot.GetLength(0)) && ((yPix - pY) >= 0)) {
                                        mandelbrot[xPix + pX, yPix - pY] = new Pixel(color);
                                    }
                                }
                            }
                        }
                    }
                    xPix += xyPixelStep;
                    */
                }
                yPix -= xyPixelStep;
            }

            return new Pixel[2,2];
        }
        

    }
}