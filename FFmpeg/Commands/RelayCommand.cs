using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace FFmpeg.Commands
{
    public class RelayCommand : ICommand
    {
        public Action<object> ExecuteCommand { get; set; }
        public Predicate<object> CanExecuteCommand { get; set; }

        public RelayCommand()
        {

        }

        public RelayCommand(Action<Object> action, Predicate<object> predicate)
        {
            ExecuteCommand = action;
            CanExecuteCommand = predicate;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter) => CanExecuteCommand(parameter);

        public void Execute(object parameter) => ExecuteCommand(parameter);
    }
}
