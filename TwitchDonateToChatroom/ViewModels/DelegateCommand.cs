using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace TwitchDonateToChatroom.ViewModels
{
    public class DelegateCommand : ICommand
    {
        #region Fields

        private Action<object> _executeMethod;

        private Func<bool> _canExecuteMethod;

        #endregion

        #region Constructor

        public DelegateCommand(Action<object> executeMethod, Func<bool> canExecuteMethod)
        {
            _canExecuteMethod = canExecuteMethod;

            _executeMethod = executeMethod;
        }

        #endregion

        public event EventHandler CanExecuteChanged;

        bool ICommand.CanExecute(object parameter)
        {
            return CanExecute();
        }

        void ICommand.Execute(object parameter)
        {
            Execute(parameter);
        }

        private void Execute(object parameter)
        {
            _executeMethod(parameter);
        } 

        private bool CanExecute()
        {
            return _canExecuteMethod();
        }
    }
}
