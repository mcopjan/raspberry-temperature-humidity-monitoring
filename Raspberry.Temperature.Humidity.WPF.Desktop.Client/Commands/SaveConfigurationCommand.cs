using Raspberry.Temperature.Humidity.WPF.Desktop.Client.Models;
using Raspberry.Temperature.Humidity.WPF.Desktop.Client.Stores;

namespace Raspberry.Temperature.Humidity.WPF.Desktop.Client.Commands
{
    public class SaveConfigurationCommand : BaseCommand
    {
        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly ConfigurationStore _configurationStore;
        private ConfigurationNotificationViewModel _configurationNotificationViewModel;

        public SaveConfigurationCommand(ConfigurationNotificationViewModel configurationNotificationViewModel, ModalNavigationStore modalNavigationStore, ConfigurationStore configurationStore)
        {
            _configurationNotificationViewModel = configurationNotificationViewModel;
            _modalNavigationStore = modalNavigationStore;
            _configurationStore = configurationStore;
        }

        public override void Execute(object? parameter)
        {
            //this will close the modal
            _modalNavigationStore.CurrentViewModel = null;

            _configurationStore.Configuration = new Configuration() { ApiEndpointUrl = _configurationNotificationViewModel.ConfigUrl };
        }
    }
}
