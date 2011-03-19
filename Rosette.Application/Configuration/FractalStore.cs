using Rosette.Fractals;

namespace Rosette.Configuration
{
    public class FractalStore : Store<IFractal>, IFractalStore
    {
        public FractalStore(params IFractal[] fractals)
        {
            foreach (var fractal in fractals)
                Inner.Add(fractal);
        }
    }
}
