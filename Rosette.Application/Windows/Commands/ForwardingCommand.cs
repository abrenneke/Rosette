using System.Windows.Input;

namespace Rosette.Windows.Commands
{
    /// <summary>Basic command that will always execute, made to forward its request to another class.</summary>
    public abstract class ForwardingCommand : ICommand
    {
        /// <summary>Returns true.</summary>
        public bool CanExecute(object parameter) { return true; }
        public event System.EventHandler CanExecuteChanged;
        public void Execute(object parameter) { Execute(); }
        public abstract void Execute();
    }
}
