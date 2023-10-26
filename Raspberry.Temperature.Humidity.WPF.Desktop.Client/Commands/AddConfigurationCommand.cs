using Raspberry.Temperature.Humidity.WPF.Desktop.Client.Models;
using Raspberry.Temperature.Humidity.WPF.Desktop.Client.Stores;

namespace Raspberry.Temperature.Humidity.WPF.Desktop.Client.Commands
{
    public class AddConfigurationCommand : BaseCommand
    {
        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly ConfigurationStore _configurationStore;

        public AddConfigurationCommand(ModalNavigationStore modalNavigationStore, ConfigurationStore configurationStore)
        {
            _modalNavigationStore = modalNavigationStore;
            _configurationStore= configurationStore;
        }

        public override void Execute(object? parameter)
        {
            _modalNavigationStore.CurrentViewModel = new ConfigurationNotificationViewModel(_configurationStore);
        }
    }
}
