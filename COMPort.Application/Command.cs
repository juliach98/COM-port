using System;
using System.Windows.Input;

namespace COMPort.App
{
    internal class Command : ICommand
    {
        public Action Action { get; set; }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) => true;

        public void Execute(object parameter)
        {
            try
            {
                Action?.Invoke();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
