using System.Windows.Input;
using Rosette.Fractals;
using System;

namespace Rosette.Windows.Commands.MainWindow
{
    /// <summary>Command to load a brand new fractal. Forwards the request to the MainWindowViewModel.</summary>
    public class LoadFractalCommand : SimpleCommand
    {
        private MainWindowViewModel ViewModel;

        public override event EventHandler CanExecuteChanged;
        
        /// <summary>Creates a new LoadFractalCommand for a ViewModel.</summary>
        public LoadFractalCommand(MainWindowViewModel viewModel)
        {
            ViewModel = viewModel;
            viewModel.PostRender += x => CanExecuteChanged(viewModel, new EventArgs());
        }

        public override void Execute()
        {
            ViewModel.LoadFractal();
        }

        public override bool CanExecute()
        {
            return ViewModel.SelectedFractal != null && !ViewModel.AsyncLoader.IsLoading;
        }
    }
}
