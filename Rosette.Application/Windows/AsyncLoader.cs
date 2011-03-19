using System;
using System.ComponentModel;
using Rosette.Fractals;
using Rosette.Data;

namespace Rosette.Windows
{
    /// <summary>Asynchronous loader for fractals.</summary>
    public class AsyncLoader : INotifyPropertyChanged
    {
        private class AsyncLoadArgs
        {
            public IFractal Fractal { get; set; }
            public IFractalRenderer Renderer { get; set; }
            public Bounds Bounds { get; set; }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private bool isLoading;
        /// <summary>Gets whether the loading is currently loading a fractal.</summary>
        public bool IsLoading
        {
            get { return isLoading; }
            private set { isLoading = value; OnPropertyChanged("IsLoading"); }
        }

        /// <summary>Event to be fired when the loading is completed.</summary>
        public event Action<RenderedColoredFractal> OnFinished;

        public event Action<RenderedColoredFractal> PostFinish;

        private IFractalRenderer renderer;

        public AsyncLoader(IFractalRenderer renderer)
        {
            this.renderer = renderer;
        }

        public void LoadAsync(IFractal fractal, Bounds bounds)
        {
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += RunFractalLoad;
            IsLoading = true;
            worker.RunWorkerAsync(new AsyncLoadArgs { Bounds = bounds, Fractal = fractal, Renderer = renderer });
        }

        public void RunFractalLoad(object sender, DoWorkEventArgs e)
        {
            var args = e.Argument as AsyncLoadArgs;
            var renderedFractal = args.Renderer.Render(args.Fractal, args.Bounds);
            var renderedColoredFractal = args.Renderer.ImageProvider.RenderFractal(renderedFractal);
            if (OnFinished != null)
            {
                OnFinished(renderedColoredFractal);
                OnFinished = null;
            }
            IsLoading = false;
        }

        private void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }
    }
}
