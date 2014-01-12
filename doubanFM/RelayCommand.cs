using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace doubanFM
{
    class RelayCommand : ICommand
    {
        Action _execute;
        Func<bool> _CanExecute;

        public RelayCommand(Action action, Func<bool> canExecute)
        {
            this._execute = action;
            this._CanExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return _CanExecute();
        }

        public void RaiseCanExecuteChanged()
        {
            if(CanExecuteChanged != null)
            {
                CanExecuteChanged(this, new EventArgs());
            }
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            _execute();
        }
    }
}
