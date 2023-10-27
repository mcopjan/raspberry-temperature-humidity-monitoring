using Raspberry.Temperature.Humidity.WPF.Desktop.Client.Models;
using Raspberry.Temperature.Humidity.WPF.Desktop.Client.Stores;

namespace Raspberry.Temperature.Humidity.WPF.Desktop.Client.Commands
{
    public class SaveConfigurationCommand : BaseCommand
    {
        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly ConfigurationStore _configurationStore;
        private ConfigurationNotificationViewModel _configurationNotificationViewModel;
        private ModalNavigationStore modalNavigationStore;
        private ConfigurationStore configurationStore;

        public SaveConfigurationCommand(ModalNavigationStore modalNavigationStore, ConfigurationStore configurationStore)
        {
            _modalNavigationStore = modalNavigationStore;
            _configurationStore= configurationStore;
        }

        public SaveConfigurationCommand(ConfigurationNotificationViewModel configurationNotificationViewModel, ModalNavigationStore modalNavigationStore, ConfigurationStore configurationStore)
        {
            _configurationNotificationViewModel = configurationNotificationViewModel;
            _modalNavigationStore = modalNavigationStore;
            _configurationStore = configurationStore;
        }

        public override void Execute(object? parameter)
        {
            _modalNavigationStore.CurrentViewModel = null;
            _configurationStore.Configuration = new Configuration() { ApiEndpointUrl = _configurationNotificationViewModel.ConfigUrl };
        }
    }
}
