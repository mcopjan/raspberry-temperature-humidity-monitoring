using Raspberry.Temperature.Humidity.WPF.Desktop.Client.Models;
using Raspberry.Temperature.Humidity.WPF.Desktop.Client.Repositories;
using System;

namespace Raspberry.Temperature.Humidity.WPF.Desktop.Client.Stores
{
    public class ConfigurationStore
    {
        private Configuration _config;
        private bool? _isConnectionSuccessful;
        public Configuration Configuration
        {
            get => _config;
            set
            {
                _config = value;
                ApiRepository apiRepository = new ApiRepository(_config.ApiEndpointUrl);
                ConfigurationChanged?.Invoke(this, apiRepository);
            }
        }

        public bool? IsConnectionSuccessful
        {
            get => _isConnectionSuccessful;
            set
            {
                _isConnectionSuccessful = value;
                TestConnectionResultChanged?.Invoke(this, _isConnectionSuccessful);
            }
        }

        public event EventHandler<ApiRepository> ConfigurationChanged;
        public event EventHandler<bool?> TestConnectionResultChanged;
    }
}
