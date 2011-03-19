using System;
using System.Collections.Generic;
using System.Linq;
using Rosette.Data;

namespace Rosette.Fractals.Colorization
{
    class BandedColorizer : IColorListColorizer
    {
        private Color innerColor;
        public Color InnerColor 
        { 
            get { return innerColor; } 
            set { innerColor = value; } 
        }
        public IList<Color> Colors { get; set; }

        /// <summary>Creates a new ColorInterpolatorColorizer with an arbirary number of colors to use.</summary>
        public BandedColorizer()
        {
            Colors = new List<Color>();
        }

        /// <summary>
        /// Gets a color to render the point as from a point,
        /// and whether the point has reached the maximum number of iterations (to be rendered differently)
        /// </summary>
        public Color GetColor(float value, bool IsMaxIterations)
        {
            if (IsMaxIterations) return InnerColor;
            if (Colors.Count == 0) return Color.FromRgb(255, 255, 255);


            int range = Colors.Count * 10;
            float position = (Math.Abs(value) % range);
            int increment = -2;
            for (int i = 1; i < Colors.Count + 2; i++)
            {
                if (position <= (10 * i))
                {
                    increment = i - 1;
                    break;
                }
            }
            var colorBytes = new byte[6];
            for (int i = 0; i < 2; i++)
            {
                colorBytes[i * 3] = Colors[(increment * 2 + i) % Colors.Count].R;
                colorBytes[i * 3 + 1] = Colors[(increment * 2 + i) % Colors.Count].G;
                colorBytes[i * 3 + 2] = Colors[(increment * 2 + i) % Colors.Count].B;
            }
            var colorChanges = new float[3];
            for (int i = 0; i < 3; i++)
                colorChanges[i] = (colorBytes[i + 3] - colorBytes[i]);

            float percent = (position % 10) / 10;
            percent = percent == 0 ? 1 : percent;

            byte colorR = (byte)(colorBytes[0] + colorChanges[0] * percent);
            byte colorG = (byte)(colorBytes[1] + colorChanges[1] * percent);
            byte colorB = (byte)(colorBytes[2] + colorChanges[2] * percent);

            return Color.FromRgb(colorR, colorG, colorB);
        }

        /// <summary>Gets the name of the colorizer to show up in the UI.</summary>
        public string FriendlyName
        {
            get { return "Banded"; }
        }
    }
}
