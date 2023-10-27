using System;
using Raspberry.Temperature.Humidity.WPF.Desktop.Client.Models;
using Raspberry.Temperature.Humidity.WPF.Desktop.Client.Stores;

namespace Raspberry.Temperature.Humidity.WPF.Desktop.Client.Commands
{
    public class OnRoomNameButtonCommand : BaseCommand
    {

        private readonly ConfigurationStore _configurationStore;

        public OnRoomNameButtonCommand(ConfigurationStore configurationStore)
        {

            _configurationStore = configurationStore;
        }

        public override void Execute(object? parameter)
        {
            Console.WriteLine(((Room)parameter).Name);
        }
    }
}
