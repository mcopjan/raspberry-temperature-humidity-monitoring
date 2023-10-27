using Raspberry.Temperature.Humidity.WPF.Desktop.Client.Models;
using Raspberry.Temperature.Humidity.WPF.Desktop.Client.Stores;
using System.Net.NetworkInformation;
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
            bool result = await PingAsync(_configurationNotificationViewModel.ConfigUrl);
            _configurationStore.IsConnectionSuccessful = result;
        }

        private async Task<bool> PingAsync(string hostUrl)
        {
            Ping ping = new Ping();

            PingReply result = await ping.SendPingAsync(hostUrl,1000);
            return result.Status == IPStatus.Success;
        }
    }
}
