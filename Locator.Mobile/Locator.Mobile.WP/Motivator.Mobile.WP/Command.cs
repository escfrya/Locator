using System;
using System.Windows.Input;

namespace Motivator.Mobile.WP
{
    public sealed class Command : ICommand
    {
        #region Fields

        private readonly Action<object> executeDelegate;
        private readonly Func<object, bool> canExecuteDelegate;

        #endregion Fields

        #region Constructors

        public Command(Action<object> executeDelegate)
            : this(executeDelegate, null)
        {
        }

        public Command(Action executeDelegate)
        {
            this.executeDelegate = o => executeDelegate();
        }

        public Command(Action<object> executeDelegate, Func<object, bool> canExecuteDelegate)
        {
            this.executeDelegate = executeDelegate;
            this.canExecuteDelegate = canExecuteDelegate;
        }

        #endregion Constructors

        #region ICommand Members

        public bool CanExecute(object parameter)
        {
            if (canExecuteDelegate == null)
                return true;

            return canExecuteDelegate(parameter);
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            if (executeDelegate != null)
                executeDelegate(parameter);

            if (CanExecuteChanged != null)
                CanExecuteChanged.Invoke(this, new EventArgs());
        }

        #endregion ICommand Members
    }
}
