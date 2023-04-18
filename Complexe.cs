using System.CodeDom;

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

        // Exponential
        /// <summary>
        /// give the exponential of a complex number
        /// </summary>
        /// <param name="fact"></param>
        /// <returns></returns>
        public Complexe IExponential(int fact = 1)
        {
            Complexe exp = new Complexe(Math.Exp(-1 * fact * this.c) * Math.Cos(fact * this.r), Math.Exp(-1 * fact * this.c) * Math.Sin(fact * this.r));

            return exp;
        }


        /// <summary>
        /// give the cosine of a complex number
        /// </summary>
        /// <returns></returns>
        public Complexe Cosine()
        {
            Complexe cos = new Complexe(this.IExponential().r + this.IExponential(-1).r / 2, this.IExponential().c + this.IExponential(-1).c / 2);

            return cos;
        }


        /// <summary>
        /// give the sinus of a complex number
        /// </summary>
        /// <returns></returns>
        public Complexe Sine()
        {
            Complexe sin = new Complexe(((-1 * Math.Exp(this.c) * Math.Sin(this.r)) + Math.Exp(this.c) * Math.Sin(-1 * this.r))/ 2, 
                (Math.Exp(-1 * this.c) * Math.Cos(this.r) - Math.Exp(this.c) * Math.Cos(-1 * this.r))/2);

            return sin;
        }

        /// <summary>
        /// give the addition of two complex numbers
        /// </summary>
        /// <param name="a">first complex number</param>
        /// <param name="b">second complex number</param>
        /// <returns></returns>
        public static Complexe Add(Complexe a, Complexe b)
        {
            return new Complexe(a.r + b.r, a.c + b.c);
        }


        /// <summary>
        /// give the multiplication of two complex numbers
        /// </summary>
        /// <param name="a">first complex number</param>
        /// <param name="b">second complex number</param>
        /// <returns></returns>
        public static Complexe Mult(Complexe a, Complexe b)
        {
            return new Complexe(a.r * b.r - a.c * b.c, a.r * b.c + b.r * a.c);
        }


        /// <summary>
        /// give the division of two complex numbers
        /// </summary>
        /// <param name="a">firs complex number</param>
        /// <param name="b">second complex number</param>
        /// <returns></returns>
        public static Complexe Div(Complexe a, Complexe b)
        {
            Complexe div = new Complexe((a.r * b.r + a.c * b.c) / (b.r * b.r + b.c * b.c), (a.c * b.r - a.r * b.c) / (b.r * b.r + b.c * b.c));
            return div;
        }

        /// <summary>
        /// give the power of a complex number
        /// </summary>
        /// <param name="a">complex number</param>
        /// <param name="pow">the int power of the complex</param>
        /// <returns>a^pow</returns>
        public static Complexe Pow(Complexe a, int pow)
        {
            Complexe res = new Complexe(a.r, a.c);
            for(int i = 0; i < pow; i++)
            {
                res = Mult(res, a);
            }
            return res;
        }

        /// <summary>
        /// return the module of a complex number
        /// </summary>
        /// <returns>moduls</returns>
        public double Mod()
        {
            return this.r * this.r + this.c * this.c;
        }
    }
}