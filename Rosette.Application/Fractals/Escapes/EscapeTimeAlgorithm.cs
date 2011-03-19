using Rosette.Data;

namespace Rosette.Fractals.Escapes
{
    public class EscapeTimeAlgorithm : IEscapeAlgorithm
    {
        public float GetNormalizedIterations(bool HasEscaped, double lastIterationReal, double lastIterationImaginary, int currentIteration, int MaxIterations)
        {
            return currentIteration;
        }
    }
}
