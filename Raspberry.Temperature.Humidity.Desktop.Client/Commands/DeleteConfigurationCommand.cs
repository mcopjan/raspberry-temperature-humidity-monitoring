using Raspberry.Temperature.Humidity.Desktop.Client.Misc;
using Raspberry.Temperature.Humidity.WPF.Desktop.Client.Commands;
using System.IO;

namespace Raspberry.Temperature.Humidity.Desktop.Client.Commands
{
    public class DeleteConfigurationCommand : BaseCommand
    {
        public override void Execute(object? parameter)
        {
            if (File.Exists(Constants.ConfigFileName))
            {
                File.Delete(Constants.ConfigFileName);
            }
        }
    }
}
