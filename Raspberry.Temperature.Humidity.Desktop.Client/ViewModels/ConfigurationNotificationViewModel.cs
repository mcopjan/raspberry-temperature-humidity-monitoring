using Raspberry.Temperature.Humidity.WPF.Desktop.Client.Commands;
using Raspberry.Temperature.Humidity.WPF.Desktop.Client.Stores;
using System;
using System.ComponentModel;
using System.Windows.Input;

namespace Raspberry.Temperature.Humidity.WPF.Desktop.Client.Models
{
    public class ConfigurationNotificationViewModel : ViewModelBase
    {
		private string _configUrl;

        public ICommand SaveConfigurationCommand { get; }
        public ICommand TestConnectionCommand { get; }

        public event EventHandler OnRequestClose;

        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly ConfigurationStore _configurationStore;


        
        public string ConfigUrl
		{
			get
			{
				return _configUrl;
			}
			set
			{
				_configUrl = value;
				OnPropertyChanged(nameof(ConfigUrl));

            }
		}

        public bool? IsConnectionSuccessful => _configurationStore.IsConnectionSuccessful;


        public event PropertyChangedEventHandler? PropertyChanged;

        public ConfigurationNotificationViewModel(ModalNavigationStore modalNavigationStore, ConfigurationStore configurationStore)
        {
            _modalNavigationStore = modalNavigationStore;
            _configurationStore = configurationStore;
            SaveConfigurationCommand = new SaveConfigurationCommand(this, _modalNavigationStore, _configurationStore);
            TestConnectionCommand = new TestConnectionCommandAsync(this, _configurationStore);

            _configurationStore.TestConnectionResultChanged += _configurationStore_TestConnectionResultChanged;

        }

        private void _configurationStore_TestConnectionResultChanged(object? sender, bool? e)
        {
            OnPropertyChanged(nameof(IsConnectionSuccessful));
        }
    }
}
