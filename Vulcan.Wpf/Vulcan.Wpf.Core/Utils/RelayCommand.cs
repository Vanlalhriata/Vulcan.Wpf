using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Vulcan.Wpf.Core
{
    public class RelayCommand : ICommand
    {
        private Action<object> executeMethod;
        private Func<object, bool> canExecuteMethod;

        public event EventHandler CanExecuteChanged;

        public RelayCommand(Action<object> executeMethod, Func<object, bool> canExecuteMethod = null)
        {
            this.executeMethod = executeMethod;
            this.canExecuteMethod = canExecuteMethod;
        }

        public bool CanExecute(object parameter)
        {
            if (null == canExecuteMethod)
                return true;
                
            if (null == parameter)
                return true;

            return canExecuteMethod(parameter);
        }

        public void Execute(object parameter)
        {
            executeMethod(parameter);
        }
    }
    
    public class RelayCommand<T> : ICommand
    {
        private Action<T> executeMethod;
        private Func<T, bool> canExecuteMethod;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public RelayCommand(Action<T> executeMethod, Func<T, bool> canExecuteMethod = null)
        {
            this.executeMethod = executeMethod;
            this.canExecuteMethod = canExecuteMethod;
        }

        public bool CanExecute(object parameter)
        {
            if (null == canExecuteMethod)
                return true;
                
            if (null == parameter)
                return true;

            return canExecuteMethod((T)Convert.ChangeType(parameter, typeof(T)));
        }

        public void Execute(object parameter)
        {
            executeMethod((T)Convert.ChangeType(parameter, typeof(T)));
        }
    }
}
