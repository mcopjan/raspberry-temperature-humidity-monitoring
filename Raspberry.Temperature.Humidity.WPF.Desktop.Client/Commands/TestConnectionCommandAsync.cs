using Raspberry.Temperature.Humidity.WPF.Desktop.Client.Models;
using Raspberry.Temperature.Humidity.WPF.Desktop.Client.Repositories;
using Raspberry.Temperature.Humidity.WPF.Desktop.Client.Stores;
using System.Threading.Tasks;

namespace Raspberry.Temperature.Humidity.WPF.Desktop.Client.Commands
{
    public class TestConnectionCommandAsync : BaseCommandAsync
    {
        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly ConfigurationStore _configurationStore;
        private ConfigurationNotificationViewModel _configurationNotificationViewModel;


        public TestConnectionCommandAsync(ConfigurationNotificationViewModel configurationNotificationViewModel, ConfigurationStore configurationStore)
        {
            _configurationNotificationViewModel = configurationNotificationViewModel;
            _configurationStore = configurationStore;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            var apiRepository = new ApiRepository(_configurationNotificationViewModel.ConfigUrl);
            bool result = await apiRepository.IsApiEndpontAvailableAsync();
            _configurationStore.IsConnectionSuccessful = result;
        }
    }
}
