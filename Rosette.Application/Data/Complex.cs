namespace Rosette.Data
{
    public struct Complex
    {
        public double Real { get; set; }
        public double Imaginary { get; set; }

        public static Complex FromReal(double real)
        {
            return new Complex { Real = real, Imaginary = 0 };
        }

        public static Complex FromReal(double real, double complex)
        {
            return new Complex { Real = real, Imaginary = complex };
        }

        public static Complex operator +(Complex a, Complex b)
        {
            return new Complex { Real = a.Real + b.Real, Imaginary = a.Imaginary + b.Imaginary };
        }

        public static Complex operator -(Complex a, Complex b)
        {
            return new Complex { Real = a.Real - b.Real, Imaginary = a.Imaginary - b.Imaginary };
        }

        public static readonly Complex Zero = Complex.FromReal(0);

        public Complex Sqr()
        {
            return new Complex 
            { 
                Real = (Real * Real) + (Imaginary * -Imaginary), 
                Imaginary = 2 * Real * Imaginary 
            };
        }
    }
}
