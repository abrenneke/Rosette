using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace Rosette.Fractals.Escapes
{
    class RenormalizedIterCount : IEscapeAlgorithm
    {
        public float GetNormalizedIterations(bool HasEscaped, double lastIterationReal, double lastIterationImaginary, int currentIteration, int maxIterations)
        {
            return HasEscaped ? (float)(currentIteration - (Math.Log(Math.Log(Math.Sqrt((double)((lastIterationReal * lastIterationReal) + (lastIterationImaginary * lastIterationImaginary)))) / Math.Log(4), 2))) : (float)maxIterations;
        }
    }
}
