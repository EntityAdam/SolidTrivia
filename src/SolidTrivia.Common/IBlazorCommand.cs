using System;

namespace SolidTrivia.Common
{
    public interface IBlazorCommand
    {
        event EventHandler CanExecuteChanged;

        bool CanExecute(object parameter);
        void Execute(object parameter);
        void RaiseCanExecuteChanged();
    }
}