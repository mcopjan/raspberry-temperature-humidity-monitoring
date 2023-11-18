using Raspberry.Temperature.Humidity.WPF.Desktop.Client.Models;
using Raspberry.Temperature.Humidity.WPF.Desktop.Client.Repositories;
using Raspberry.Temperature.Humidity.WPF.Desktop.Client.Stores;
using System.IO;

namespace Raspberry.Temperature.Humidity.WPF.Desktop.Client.Commands
{
    public class SaveConfigurationCommand : BaseCommand
    {
        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly ConfigurationStore _configurationStore;
        private ConfigurationNotificationViewModel _configurationNotificationViewModel;
        private const string ConfigFileName = "config.json";

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

            _configurationStore.ApiRepository = new ApiRepository(_configurationNotificationViewModel.ConfigUrl);

            //save config into a file
            File.WriteAllText(ConfigFileName, _configurationNotificationViewModel.ConfigUrl);
        }
    }
}
