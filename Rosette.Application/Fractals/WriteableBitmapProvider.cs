using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Rosette.Data;
using Rosette.Fractals.Colorization;

namespace Rosette.Fractals
{
    /// <summary>Image provider that uses a WriteableBitmap for its backing.</summary>
    public class WriteableBitmapProvider : IImageProvider
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string property) { if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(property)); }

        /// <summary>The DPI of a typical monitor - 96.</summary>
        private const int MonitorDPI = 96;

        private Dimensions dimensions;
        /// <summary>Gets or sets the dimensions of the image to be rendered.</summary>
        public Dimensions Dimensions
        {
            get { return dimensions; }
            set 
            {
                //If the dimensions get changed, the image needs to be recreated.
                if (dimensions != value)
                {
                    bitmap = null;
                }
                dimensions = value;
                OnPropertyChanged("Dimensions");
            }
        }

        /// <summary>Gets or sets the pixel format of the image to be rendered. Different pixel formats will take different amounts of space.</summary>
        public PixelFormat PixelFormat { get; set; }

        private WriteableBitmap bitmap;

        /// <summary>Gets the actual bitmap to be rendered. If the bitmap has not been created, it will be created.</summary>
        private WriteableBitmap Bitmap
        {
            get 
            {
                if (bitmap == null && Dimensions.Width > 0 && Dimensions.Height > 0)
                {
                    bitmap = new WriteableBitmap(Dimensions.Width, Dimensions.Height, MonitorDPI, MonitorDPI, PixelFormat, null);
                    OnPropertyChanged("Bitmap");
                }
                return bitmap;
            }
        }

        /// <summary>Gets the ImageSource to display the image.</summary>
        public ImageSource Image { get { return Bitmap; } }

        /// <summary>Gets the stride of the image to be rendered. The stride is the number of bytes for one line of the image.</summary>
        public int Stride
        {
            get { return (PixelFormat.BitsPerPixel / 8) * Dimensions.Width; }
        }

        /// <summary>Gets the number of pixels per row of the image.</summary>
        public int PixelsPerRow
        {
            get { return Dimensions.Width; }
        }

        /// <summary>Gets or sets the colorizer used to rendering images.</summary>
        public IFractalColorizer Colorizer { get; set; }

        /// <summary>Creates a new WriteableBitmapProvider with the specified dimensions for the image.</summary>
        /// <param name="dimensions">The dimensions of the image to be rendered.</param>
        public WriteableBitmapProvider()
        {
            PixelFormat = PixelFormats.Rgb24;
        }

        /// <summary>Renders a RenderedFractal to the Image.</summary>
        public RenderedColoredFractal RenderFractal(RenderedFractal fractal)
        {
            if (Colorizer == null)
                throw new InvalidOperationException("A Colorizer must be set to render a fractal.");

            var imageConvertedFractal = fractal.RawData.SelectMany(x =>
                {
                    var color = Colorizer.GetColor(x, x == fractal.MaxIterations);
                    return new byte[] { color.R, color.G, color.B };
                }).ToArray();
            return new RenderedColoredFractal(fractal, imageConvertedFractal) { ImageProvider = this };
        }

        /// <summary>Sets the image of the provider to a rendered, colored fractal.</summary>
        public void SetImage(RenderedColoredFractal fractal)
        {
            Bitmap.WritePixels(new Int32Rect(0, 0, Dimensions.Width, Dimensions.Height), fractal.RawData, Stride, 0);
            OnPropertyChanged("Image");
        }
    }
}
