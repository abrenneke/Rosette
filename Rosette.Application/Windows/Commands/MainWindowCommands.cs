using System.Windows.Input;
using Rosette.Windows.Commands.MainWindow;

namespace Rosette.Windows.Commands
{
    public class MainWindowCommands
    {
        private MainWindowViewModel viewModel;

        /// <summary>Gets or sets the command to load a brand new fractal.</summary>
        public ICommand LoadFractalCommand { get; set; }
        /// <summary>Gets or sets the command to save the current fractal to an image.</summary>
        public ICommand SaveImageCommand { get; set; }
        /// <summary>Gets or sets the command to close the main window.</summary>
        public ICommand CloseWindowCommand { get; set; }
        /// <summary>Gets or sets the command to minimize the main window.</summary>
        public ICommand MinimizeWindowCommand { get; set; }

        public MainWindowCommands(MainWindowViewModel viewModel)
        {
            this.viewModel = viewModel;

            LoadFractalCommand = new LoadFractalCommand(viewModel);
            SaveImageCommand = new SaveImageCommand(viewModel);
            CloseWindowCommand = new CloseWindowCommand(viewModel);
            MinimizeWindowCommand = new MinimizeWindowCommand(viewModel);
        }
    }
}
