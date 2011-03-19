using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Rosette.Data;
using Rosette.Fractals;
using Rosette.Fractals.Colorization;
using Rosette.Configuration;
using Rosette.Windows.Commands;

namespace Rosette.Windows
{
    public enum ColorizerContext { InnerColor, Colors }

    public class MainWindowViewModel : ViewModel
    {
        private IFractalRenderer renderer;
        private MainWindow mainWindow;

        public event Action<RenderedColoredFractal> PostRender;

        /// <summary>Gets the available fractals for the app.</summary>
        public IList<IFractal> AvailableFractals { get; private set; }
        /// <summary>Gets the available colorzations for fractals.</summary>
        public IList<ColorizerViewModel> AvailableColorizations { get; private set; }

        private IFractal selectedFractal;
        /// <summary>Gets or sets the fractal selected from the FractalStore.</summary>
        public IFractal SelectedFractal { get { return selectedFractal; } set { selectedFractal = value; OnPropertyChanged(x => SelectedFractal); } }

        private ColorizerViewModel selectedColorizer;
        /// <summary>Gets or sets the selected colorizer.</summary>
        public ColorizerViewModel SelectedColorizer 
        {
            get { return selectedColorizer; } 
            set 
            {
                selectedColorizer = value;
                ImageProvider.Colorizer = value.Colorizer;
                OnPropertyChanged(x => SelectedColorizer);
                OnPropertyChanged(x => IsColorListColorizer);
            } 
        }

        /// <summary>Gets whether the selected colorizer is a ColorListColorizer.</summary>
        public bool IsColorListColorizer { get { return SelectedColorizer is ColorListColorizerViewModel; } }

        /// <summary>Gets the image provider for the UI.</summary>
        public IImageProvider ImageProvider { get; private set; }

        /// <summary>Gets or sets the width of the image area. Set from a binding.</summary>
        public double ImageAreaWidth { get; set; }

        /// <summary>Gets or sets the height of the image area. Set from a binding.</summary>
        public double ImageAreaHeight { get; set; }

        /// <summary>Gets or sets the MainWindowCommands for the view model.</summary>
        public MainWindowCommands Commands { get; set; }

        /// <summary>Gets or sets the provider for drawing bounding boxes on the window.</summary>
        public BoundingBoxProvider BoundingBoxProvider { get; set; }

        /// <summary>Gets or sets the history provider for the window.</summary>
        public FractalHistoryProvider History { get; set; }

        /// <summary>Gets or sets the loader for rendering fractals asynchronously.</summary>
        public AsyncLoader AsyncLoader { get; set; }

        /// <summary>Gets or sets the iterations at the mouse screen coordinates.</summary>
        public float ScreenCoordinatesIterations { get; set; }

        /// <summary>Gets or sets whether the color picker is open (visible).</summary>
        public bool ColorPickerIsOpen { get; set; }

        private ColorListColorizerViewModel colorPickerColorizer;
        private ColorizerContext colorPickerColorizerContext;
        private int? colorPickerColorizerIndex;

        /// <summary>Gets or sets the maximum number of iterations for the current fractal.</summary>
        public int MaximumIterations 
        {
            get
            {
                if (SelectedFractal == null)
                    return 1;
                return SelectedFractal.MaxIterations;
            }
            set 
            {
                if (SelectedFractal != null)
                {
                    SelectedFractal.MaxIterations = value;
                    OnPropertyChanged(x => MaximumIterations);
                }
            } 
        }

        /// <summary>Creates a new MainWindowViewModel, with dependencies.</summary>
        public MainWindowViewModel(IFractalRenderer renderer, IImageProvider imageProvider, MainWindow mainWindow, IFractalStore fractals, IColorizationStore colorizers)
        {
            this.renderer = renderer;
            this.mainWindow = mainWindow;

            AvailableFractals = fractals;
            AvailableColorizations = colorizers.Select(x => GetViewModel(x)).ToList();

            ImageProvider = imageProvider;
            AsyncLoader = new AsyncLoader(renderer);

            BoundingBoxProvider = new BoundingBoxProvider();
            BoundingBoxProvider.BoxReleased += LoadFractalFromScreenBounds;

            History = new FractalHistoryProvider();
            History.OnBack += OnHistoryBack;

            Commands = new MainWindowCommands(this);

            SelectedColorizer = AvailableColorizations.FirstOrDefault();
            SelectedFractal = AvailableFractals.FirstOrDefault();

            MaximumIterations = 800;
        }

        /// <summary>Loads a brand new fractal, refreshing the image.</summary>
        public void LoadFractal()
        {
            ImageProvider.Dimensions = new Dimensions((int)ImageAreaWidth, (int)ImageAreaHeight);

            Bounds bounds;
            if (renderer.LastRenderedFractal != SelectedFractal || !renderer.LastRenderedBounds.HasValue)
            {
                bounds = SelectedFractal.Bounds;
            }
            else
            {
                bounds = renderer.LastRenderedBounds.Value;
            }

            RenderNew(bounds);
        }

        /// <summary>Loads a fractal to the specified bounds, converting from bounds obtained from screen units.</summary>
        public void LoadFractalFromScreenBounds(Bounds screenBounds)
        {
            var fractalBounds = renderer.GetFractalBounds(screenBounds);
            RenderNew(fractalBounds);
        }

        /// <summary>Renders the fractal with the specified bounds, pushing the render on to the history stack.</summary>
        private void RenderNew(Bounds bounds) { RenderNew(bounds, false); }

        /// <summary>Renders the fractal with the specified bounds, optionally ignoring pushing the bounds on to the history stack.</summary>
        private void RenderNew(Bounds bounds, bool ignoreHistory)
        {
            AsyncLoader.OnFinished += fractal => {
                if(!ignoreHistory)
                    History.Push(fractal);
                SetImageDispatch(fractal);
            };
            AsyncLoader.PostFinish += fractal => PostRender(fractal);
            AsyncLoader.LoadAsync(SelectedFractal, bounds);
        }

        private void RenderNew(RenderedColoredFractal renderedFractal, bool ignoreHistory)
        {
            ImageProvider.SetImage(renderedFractal);
            if(!ignoreHistory)
                History.Push(renderedFractal);
        }

        private void SetImageDispatch(RenderedColoredFractal fractal)
        {
            mainWindow.Dispatcher.Invoke(new Action<RenderedColoredFractal>(SetImage), fractal);
        }

        private void SetImage(RenderedColoredFractal fractal)
        {
            ImageProvider.SetImage(fractal);
        }

        private void OnHistoryBack(RenderedColoredFractal fractal)
        {
            renderer.LastRenderedBounds = fractal.RenderedFractal.Bounds;
            RenderNew(fractal, true);
            MaximumIterations = fractal.RenderedFractal.MaxIterations;
        }

        /// <summary>Saves the current rendered image to the directory of the executable, with a random filename.</summary>
        public void SaveImageToRandomFile()
        {
            var image = ImageProvider.Image as BitmapSource;

            if (image == null)
                return;

            var encoder = new System.Windows.Media.Imaging.PngBitmapEncoder();
            var frame = BitmapFrame.Create(image);
            encoder.Frames.Add(frame);

            var fileName = System.IO.Path.GetRandomFileName() + ".png";

            System.IO.Directory.SetCurrentDirectory(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location));
            using (var file = System.IO.File.Create(fileName))
            {
                encoder.Save(file);
            }
        }

        public void UpdateScreenCoordiatesIterations(Point mousePosition)
        {
            if (SelectedFractal == null)
                return;

            var fractalPoint = renderer.GetFractalPoint(mousePosition);
            this.ScreenCoordinatesIterations = SelectedFractal.TestPoint(fractalPoint.X, fractalPoint.Y);
            OnPropertyChanged(x => ScreenCoordinatesIterations);
        }

        public void OpenColorPickerFor(ColorListColorizerViewModel colorizer, ColorizerContext context, int? index)
        {
            colorPickerColorizer = colorizer;
            colorPickerColorizerContext = context;
            colorPickerColorizerIndex = index;

            ColorPickerIsOpen = true;
            OnPropertyChanged(x => ColorPickerIsOpen);
        }

        public void CloseColorPickerAndOK()
        {
            var selectedColor = mainWindow.ColorPicker.SelectedColor;
            if (colorPickerColorizerContext == ColorizerContext.InnerColor)
            {
                colorPickerColorizer.InnerColor = selectedColor;
            }
            else if(colorPickerColorizerIndex.HasValue && colorPickerColorizer != null)
            {
                if(colorPickerColorizer.Colors.Count >= colorPickerColorizerIndex + 1)
                    colorPickerColorizer.Colors[colorPickerColorizerIndex.Value] = selectedColor;
            }

            

            ColorPickerIsOpen = false;
            OnPropertyChanged(x => ColorPickerIsOpen);
        }

        public void CloseWindow()
        {
            mainWindow.Close();
        }

        public void MinimizeWindow()
        {
            mainWindow.WindowState = WindowState.Minimized;
        }

        private ColorizerViewModel GetViewModel(IFractalColorizer colorizer)
        {
            return colorizer is IColorListColorizer ? new ColorListColorizerViewModel((IColorListColorizer)colorizer) : new ColorizerViewModel(colorizer);
        }
    }
}
