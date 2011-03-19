using System;
using Rosette.Extensions;

namespace Rosette.Data
{
    public class Color
    {
        public byte R { get; private set; }
        public byte G { get; private set; }
        public byte B { get; private set; }
        public byte A { get; private set; }

        /// <summary>Gets a Color from an RGB value.</summary>
        public static Color FromRgb(byte r, byte g, byte b)
        {
            return new Color { R = r, G = g, B = b, A = 255 };
        }

        /// <summary>Gets a Color from an ARGB value.</summary>
        public static Color FromArgb(byte a, byte r, byte g, byte b)
        {
            return new Color { R = r, G = g, B = b, A = a };
        }

        /// <summary>Gets a Color from an HSV value.</summary>
        public static Color FromHsv(int hue, float saturation, float value)
        {
            hue.MustBeWithinRange(0, 360);
            saturation.MustBeWithinRange(0, 1);
            value.MustBeWithinRange(0, 1);

            var chroma = value * saturation;
            var hPrime = hue / 60.0f;
            var x = chroma * (1 - Math.Abs((hPrime % 2) - 1));

            float r, g, b;
            if (hPrime >= 0 && hPrime < 1)
            {
                r = chroma;
                g = x;
                b = 0;
            }
            else if (hPrime >= 1 && hPrime < 2)
            {
                r = x;
                g = chroma;
                b = 0;
            }
            else if (hPrime >= 2 && hPrime < 3)
            {
                r = 0;
                g = chroma;
                b = x;
            }
            else if (hPrime >= 3 && hPrime < 4)
            {
                r = 0;
                g = x;
                b = chroma;
            }
            else if (hPrime >= 4 && hPrime < 5)
            {
                r = x;
                g = 0;
                b = chroma;
            }
            else if (hPrime >= 5 && hPrime < 6)
            {
                r = chroma;
                g = 0;
                b = x;
            }
            else
            {
                r = g = b = 0;
            }

            var m = value - chroma;

            return FromRgb((byte)Math.Round(((r + m) * 255)), (byte)Math.Round(((g + m) * 255)), (byte)Math.Round(((b + m) * 255)));
        }

        public static Color FromHsl(int hue, float saturation, float lightness)
        {
            var chroma = (1 - Math.Abs((lightness * 2) - 1)) * saturation;
            var hPrime = hue / 60f;
            var x = chroma * (1 - Math.Abs((hPrime % 2) - 1));

            float r, g, b;
            if (hPrime >= 0 && hPrime < 1)
            {
                r = chroma;
                g = x;
                b = 0;
            }
            else if (hPrime >= 1 && hPrime < 2)
            {
                r = x;
                g = chroma;
                b = 0;
            }
            else if (hPrime >= 2 && hPrime < 3)
            {
                r = 0;
                g = chroma;
                b = x;
            }
            else if (hPrime >= 3 && hPrime < 4)
            {
                r = 0;
                g = x;
                b = chroma;
            }
            else if (hPrime >= 4 && hPrime < 5)
            {
                r = x;
                g = 0;
                b = chroma;
            }
            else if (hPrime >= 5 && hPrime < 6)
            {
                r = chroma;
                g = 0;
                b = x;
            }
            else
            {
                r = g = b = 0;
            }

            var m = lightness - (chroma / 2f);
            return FromRgb((byte)Math.Round(((r + m) * 255)), (byte)Math.Round(((g + m) * 255)), (byte)Math.Round(((b + m) * 255)));
        }

        /// <summary>Converts a Color to a System.Windows.Media.Color.</summary>
        public static implicit operator System.Windows.Media.Color(Color thisColor)
        {
            return System.Windows.Media.Color.FromArgb(thisColor.A, thisColor.R, thisColor.G, thisColor.B);
        }

        /// <summary>Converts a System.Windows.Media.Color to a Color.</summary>
        public static implicit operator Color(System.Windows.Media.Color otherColor)
        {
            return FromArgb(otherColor.A, otherColor.R, otherColor.G, otherColor.B);
        }

        public override string ToString()
        {
            return "R:{0}, G:{1}, B:{2}, A:{3}".With(R, G, B, A);
        }
    }
}
