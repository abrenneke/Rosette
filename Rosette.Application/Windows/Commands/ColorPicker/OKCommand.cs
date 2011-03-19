using Rosette.Windows.Controls;

namespace Rosette.Windows.Commands.ColorPicker
{
    public class OKCommand : ForwardingCommand
    {
        private ColorPickerViewModel viewModel;

        public OKCommand(ColorPickerViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        public override void Execute()
        {
            viewModel.CloseAndOK();
        }
    }
}
