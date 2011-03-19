using System.ComponentModel;
using System.Windows.Media;
using Rosette.Data;
using Rosette.Fractals.Colorization;

namespace Rosette.Fractals
{
    /// <summary>A provider for an actual image of a fractal to be rendered to the screen.</summary>
    public interface IImageProvider : INotifyPropertyChanged
    {
        /// <summary>Gets or sets the dimensions of the image to be rendered.</summary>
        Dimensions Dimensions { get; set; }

        /// <summary>Gets the ImageSource which can be put in to a WPF Image control.</summary>
        ImageSource Image { get; }

        /// <summary>Gets the stride of the image - that is, the number of bytes per row of image.</summary>
        int Stride { get; }

        /// <summary>Gets the number of pixels per row of the image.</summary>
        int PixelsPerRow { get; }

        /// <summary>Gets or sets the colorizer used to colorize images.</summary>
        IFractalColorizer Colorizer { get; set; }

        /// <summary>Renders a RenderedFractal to the Image.</summary>
        RenderedColoredFractal RenderFractal(RenderedFractal fractal);

        /// <summary>Sets the image of the provider to a rendered, colored fractal.</summary>
        void SetImage(RenderedColoredFractal fractal);
    }
}
