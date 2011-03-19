using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Rosette.Data;

namespace Rosette.Fractals
{
    /// <summary>
    /// Class responsible for bridging the rendering calculations of the fractal with the image requirements.
    /// Can convert screen coordinates to fractal coordinates, and prevents fractal distortion.
    /// </summary>
    public class StandardFractalRenderer : IFractalRenderer
    {
        /// <summary>Gets or sets the ImageProvider used for the FractalRenderer.</summary>
        public IImageProvider ImageProvider { get; set; }

        /// <summary>Gets the bounds of the last rendered fractal by this renderer.</summary>
        public Bounds? LastRenderedBounds { get; set; }

        /// <summary>Gets or sets the last rendered fractal by the renderer.</summary>
        public IFractal LastRenderedFractal { get; set; }

        private Bounds InternalLastRenderedBounds { get { return LastRenderedBounds.HasValue ? LastRenderedBounds.Value : new Bounds(); } }

        private IFractalImageProcessor fractalProcessor;

        /// <summary>Creates a new StandardFractalRenderer using an image provider and fractal processor.</summary>
        public StandardFractalRenderer(IImageProvider imageProvider, IFractalImageProcessor fractalProcessor)
        {
            this.ImageProvider = imageProvider;
            this.fractalProcessor = fractalProcessor;
        }

        /// <summary>
        /// Gets a RenderedFractal by rendering the fractal with the area to render.
        /// 
        /// The bounds will be scaled to match the ImageProvider dimensions.
        /// </summary>
        public RenderedFractal Render(IFractal fractal, Bounds areaToRender)
        {
            var bounds = GetResizedBounds(areaToRender);
            var renderedFractal = fractalProcessor.Process(fractal, bounds, ImageProvider.Dimensions);
            return Render(renderedFractal);
        }

        public RenderedFractal Render(RenderedFractal renderedFractal)
        {
            LastRenderedBounds = renderedFractal.Bounds;
            LastRenderedFractal = renderedFractal.Fractal;
            return renderedFractal;
        }

        /// <summary>
        /// Resizes the Bounds to match the aspect ratio of the iamge to be rendered. 
        /// Prevents distorted images.
        /// </summary>
        private Bounds GetResizedBounds(Bounds areaToRender)
        {
            var realImageRatio = ImageProvider.Dimensions.Width / ImageProvider.Dimensions.Height;
            var desiredRatio = areaToRender.Width / areaToRender.Height;
            Bounds newBounds;

            if (realImageRatio < desiredRatio)
            {
                var additionalHeight = (ImageProvider.Dimensions.Height * areaToRender.Width) / ImageProvider.Dimensions.Width - areaToRender.Height;
                var amount = additionalHeight / 2;
                newBounds = new Bounds(areaToRender.X, new Bound(areaToRender.Y.MinValue - amount, areaToRender.Y.MaxValue + amount));
            }
            else
            {
                var additionalWidth = (ImageProvider.Dimensions.Width * areaToRender.Height) / ImageProvider.Dimensions.Height - areaToRender.Width;
                var amount = additionalWidth / 2;
                newBounds = new Bounds(new Bound(areaToRender.X.MinValue - amount, areaToRender.X.MaxValue + amount), areaToRender.Y);
            }

            return newBounds;
        }

        /// <summary>Transforms the Point on the screen to a Point on the surface of the fractal.</summary>
        public Point GetFractalPoint(Point screenPoint)
        {
            var percentagePoint = new Point(
                screenPoint.X / ImageProvider.Dimensions.Width,
                screenPoint.Y / ImageProvider.Dimensions.Height
                );
            return new Point(
                InternalLastRenderedBounds.X.MinValue + (InternalLastRenderedBounds.Width * percentagePoint.X),
                InternalLastRenderedBounds.Y.MinValue + (InternalLastRenderedBounds.Height * percentagePoint.Y)
                );
        }

        /// <summary>Transforms the Bounds of the screen in to Bounds on the surface of the fractal.</summary>
        public Bounds GetFractalBounds(Bounds screenBounds)
        {
            var percentageBounds = new Bounds(
                new Bound(
                    screenBounds.X.MinValue / ImageProvider.Dimensions.Width,
                    screenBounds.X.MaxValue / ImageProvider.Dimensions.Width),
                new Bound(
                    screenBounds.Y.MinValue / ImageProvider.Dimensions.Height,
                    screenBounds.Y.MaxValue / ImageProvider.Dimensions.Height)
                );

            var newBounds = new Bounds(
                new Bound(
                    InternalLastRenderedBounds.X.MinValue + (InternalLastRenderedBounds.Width * percentageBounds.X.MinValue),
                    InternalLastRenderedBounds.X.MinValue + (InternalLastRenderedBounds.Width * percentageBounds.X.MaxValue)),
                new Bound(
                    InternalLastRenderedBounds.Y.MinValue + (InternalLastRenderedBounds.Height * percentageBounds.Y.MinValue),
                    InternalLastRenderedBounds.Y.MinValue + (InternalLastRenderedBounds.Height * percentageBounds.Y.MaxValue)));

            return newBounds;
        }
    }
}
