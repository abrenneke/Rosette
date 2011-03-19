using Rosette.Data;

namespace Rosette.Fractals.Escapes
{
    public interface IEscapeAlgorithm
    {
        float GetNormalizedIterations(bool HasEscaped, double lastIterationReal, double lastIterationImaginary, int currentIteration, int maxIterations);
    }
}
