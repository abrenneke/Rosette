using Rosette.Configuration;
using Rosette.Fractals;
using Rosette.Fractals.Colorization;
using Ninject.Modules;

namespace Rosette
{
    public class StoreModule : NinjectModule
    {
        private IFractalStore fractalStore;
        private IColorizationStore colorizerStore;

        public StoreModule(IFractalStore fractals, IColorizationStore colorizers)
        {
            fractalStore = fractals;
            colorizerStore = colorizers;
        }

        public override void Load()
        {
            Bind<IFractalStore>().ToConstant(fractalStore);
            Bind<IColorizationStore>().ToConstant(colorizerStore);
        }
    }
}
