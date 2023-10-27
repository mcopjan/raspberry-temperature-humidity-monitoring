using System.Threading.Tasks;

namespace Raspberry.Temperature.Humidity.WPF.Desktop.Client.Commands
{
    public abstract class BaseCommandAsync : BaseCommand
    {
        public override async void Execute(object? parameter)
        {
            try
            {
                await ExecuteAsync(parameter);
            }
            catch (System.Exception) { };
           
        }

        public abstract Task ExecuteAsync(object parameter);
    }
}
