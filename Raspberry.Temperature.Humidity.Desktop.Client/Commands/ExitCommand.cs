using Raspberry.Temperature.Humidity.WPF.Desktop.Client.Commands;
using System.Windows;

namespace Raspberry.Temperature.Humidity.Desktop.Client.Commands
{
    public class ExitCommand : BaseCommand
    {
        public override void Execute(object? parameter)
        {
            Application.Current.Shutdown();
        }
    }
}
