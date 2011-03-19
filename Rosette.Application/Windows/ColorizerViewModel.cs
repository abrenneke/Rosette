using Rosette.Fractals.Colorization;

namespace Rosette.Windows
{
    /// <summary>View model wrapper for a Colorizer.</summary>
    public class ColorizerViewModel : ViewModel
    {
        /// <summary>Gets the inner colorizer of the view model.</summary>
        public IFractalColorizer Colorizer { get; protected set; }

        /// <summary>Gets the friendly name of the colorizer.</summary>
        public string FriendlyName { get { return Colorizer.FriendlyName; } }

        /// <summary>Creates a new ColorizerViewModel around a colorizer.</summary>
        public ColorizerViewModel(IFractalColorizer colorizer)
        {
            Colorizer = colorizer;
        }
    }
}
