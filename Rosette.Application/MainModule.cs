using Rosette.Windows;
using Rosette.Fractals;
using Rosette.Fractals.Colorization;
using Rosette.Fractals.Mandelbrot;
using System.Windows.Media;
using Ninject;
using Ninject.Modules;

namespace Rosette
{
    public class MainModule : NinjectModule
    {
        public override void Load()
        {
            Bind<MainWindow>().ToSelf().InSingletonScope();
            Bind<MainWindowViewModel>().ToSelf();

            Bind<IImageProvider>().To<WriteableBitmapProvider>().InSingletonScope();
            Bind<IFractalRenderer>().To<StandardFractalRenderer>().InSingletonScope();
            Bind<IFractalImageProcessor>().To<ParallelFractalImageProcessor>();
        }
    }
}
