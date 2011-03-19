using System.Windows;
using Rosette.Windows;
using Rosette.Fractals.Colorization;
using Rosette.Fractals.Mandelbrot;
using Rosette.Fractals.JuliaSet;
using Rosette.Fractals.Escapes;
using Rosette.Configuration;
using Rosette.Data;
using Ninject;
using Colors = System.Windows.Media.Colors;

namespace Rosette
{
    /// <summary>Main entry point for program.</summary>
    public class Global
    {
        /// <summary>Sets the startup window that will run first.</summary>
        public Window StartupWindow { get; private set; }

        /// <summary>Initializes the application.</summary>
        public Global()
        {
            var color = Color.FromHsl(0, 0, 1);

            var escapeAlgorithm = new RenormalizedIterCount();

            var fractalStore = new FractalStore(
                    new Mandelbrot(escapeAlgorithm),
                    new JuliaSet1(escapeAlgorithm),
                    new JuliaSet2(escapeAlgorithm),
                    new JuliaSet3(escapeAlgorithm),
                    new JuliaSet4(escapeAlgorithm)
                );

            var colorizationStore = new ColorizationStore(
                    new BandedColorizer { InnerColor = Colors.Black, Colors = new Color[] { Colors.Blue, Colors.Green, Colors.Yellow } },
                    new ColorInterpolatorColorizer { InnerColor = Colors.Black, Colors = new Color[] { Colors.Chartreuse, Colors.CornflowerBlue, Colors.Red } },
                    new SimpleColorizer()
                );

            var kernel = new StandardKernel(
                    new MainModule(),
                    new StoreModule(fractalStore, colorizationStore)
                );

            var mainWindow = kernel.Get<MainWindow>();
            mainWindow.ViewModel = kernel.Get<MainWindowViewModel>();
            StartupWindow = mainWindow;
        }
    }
}
