namespace Rosette.Windows.Commands.MainWindow
{
    public class MinimizeWindowCommand : ForwardingCommand
    {
        private MainWindowViewModel viewModel;

        public MinimizeWindowCommand(MainWindowViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        public override void Execute()
        {
            viewModel.MinimizeWindow();
        }
    }
}
