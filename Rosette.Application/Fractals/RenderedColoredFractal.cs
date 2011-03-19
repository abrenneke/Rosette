namespace Rosette.Fractals
{
    /// <summary>A fractal which has been rendered and colored. Gives access to the imageProcessor used to color the fractal, since coloring is image-specific.</summary>
    public class RenderedColoredFractal
    {
        /// <summary>Gets the underlying RenderedFractal used to make the RenderedColoredFractal.</summary>
        public RenderedFractal RenderedFractal { get; private set; }

        /// <summary>Gets the raw colored data of the fractal.</summary>
        public byte[] RawData { get; private set; }
        
        /// <summary>Gets or sets the image processor used to create this RenderedColoredFractal.</summary>
        public IImageProvider ImageProvider { get; set; }

        /// <summary>Creates a new RenderedColoredFractal with the underlying rendered fractal, and the colorized byte data.</summary>
        public RenderedColoredFractal(RenderedFractal rendered, byte[] rawColoredData)
        {
            this.RenderedFractal = rendered;
            this.RawData = rawColoredData;
        }
    }
}
