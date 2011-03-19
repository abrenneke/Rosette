using System.Collections.Generic;
using Rosette.Fractals.Colorization;

namespace Rosette.Configuration
{
    public interface IColorizationStore : IList<IFractalColorizer> { }
}
