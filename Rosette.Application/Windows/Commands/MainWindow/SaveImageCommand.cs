using System.Windows.Input;

namespace Rosette.Windows.Commands.MainWindow
{
    /// <summary>Command to save the current fractal image. Forwards request to the ViewModel.</summary>
    public class SaveImageCommand : SimpleCommand
    {
        private MainWindowViewModel viewModel;

        /// <summary>Creates a new SaveImageCommand for a ViewModel.</summary>
        public SaveImageCommand(MainWindowViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        /// <summary>Executes the save.</summary>
        public override void Execute()
        {
            viewModel.SaveImageToRandomFile();
        }

        public override bool CanExecute()
        {
            return viewModel.ImageProvider != null && viewModel.ImageProvider.Image != null;
        }
    }
}
