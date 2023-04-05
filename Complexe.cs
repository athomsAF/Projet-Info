namespace projects
{
    public class Complexe
    {
        public double r;
        public double c;

        public Complexe(double x, double y)
        {
            this.r = x;
            this.c = y; 
        }

        // Mandellbrot
        public Complexe doCmplxSqPlusConst(Complexe c)
        {
            return new Complexe(Math.Pow(this.r, 2) - Math.Pow(this.c, 2) + c.r, 2 * this.r * this.c + c.c);
        }

        public double doMoulusSq()
        {
            return Math.Pow(this.r, 2) + Math.Pow(this.c, 2);
        }


    }
}