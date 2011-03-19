using System.Collections.Generic;
using System.Collections.Specialized;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Rosette.Data;
using Rosette.Fractals.Colorization;

namespace Rosette.Windows
{
    public class AddColorToListCommand : Rosette.Windows.Commands.ForwardingCommand
    {
        private ColorListColorizerViewModel viewModel;

        public AddColorToListCommand(ColorListColorizerViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        public override void Execute()
        {
            viewModel.AddNewColor();
        }
    }

    /// <summary>View model wrapper for a ColorListColorizer.</summary>
    public class ColorListColorizerViewModel : ColorizerViewModel
    {
        public IColorListColorizer ListColorizer { get { return (IColorListColorizer)Colorizer; } }

        /// <summary>Gets or sets the inner color of the colorizer.</summary>
        public Color InnerColor { get { return ListColorizer.InnerColor; } set { ListColorizer.InnerColor = value; OnPropertyChanged(x => InnerColor); } }

        private ObservableCollection<Color> colors;
        /// <summary>Gets or sets the list of colors to be used in the colorizer.</summary>
        public ObservableCollection<Color> Colors { get { return colors; } set { colors = value; OnPropertyChanged(x => Colors); } }

        /// <summary>Gets or sets the command to add a new color to the list of colors.</summary>
        public ICommand AddColorToListCommand { get; set; }

        public ColorListColorizerViewModel(IColorListColorizer colorizer) : base(colorizer)
        {
            AddColorToListCommand = new AddColorToListCommand(this);

            Colors = new ObservableCollection<Color>(colorizer.Colors);
            Colors.CollectionChanged += Colors_CollectionChanged;
        }

        public void AddNewColor()
        {
            Colors.Add(Color.FromArgb(255, 0, 0, 0));
        }

        private void Colors_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            ListColorizer.Colors = new List<Color>(Colors);
        }
    }
}
