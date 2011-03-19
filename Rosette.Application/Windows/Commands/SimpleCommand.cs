using System;
using System.Windows.Input;

namespace Rosette.Windows.Commands.MainWindow
{
    public abstract class SimpleCommand : ICommand
    {
        public virtual event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object parameter)
        {
            Execute();
        }

        public bool CanExecute(object parameter)
        {
            return CanExecute();
        }

        public abstract void Execute();
        public abstract bool CanExecute();
    }
}
