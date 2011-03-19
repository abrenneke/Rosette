using Rosette.Fractals.Colorization;

namespace Rosette.Configuration
{
    public class ColorizationStore : Store<IFractalColorizer>, IColorizationStore
    {
        public ColorizationStore(params IFractalColorizer[] colorizers)
        {
            foreach (var colorizer in colorizers)
                Inner.Add(colorizer);
        }
    }
}
