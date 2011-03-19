namespace Rosette.Windows.Commands.MainWindow
{
    public class CloseWindowCommand : ForwardingCommand
    {
        private MainWindowViewModel viewModel;

        public CloseWindowCommand(MainWindowViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        public override void Execute()
        {
            viewModel.CloseWindow();
        }
    }
}
