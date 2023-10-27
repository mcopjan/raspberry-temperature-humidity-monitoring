using Raspberry.Temperature.Humidity.WPF.Desktop.Client.Models;
using Raspberry.Temperature.Humidity.WPF.Desktop.Client.Repositories;
using System;

namespace Raspberry.Temperature.Humidity.WPF.Desktop.Client.Stores
{
    public class ConfigurationStore
    {
        private Configuration _config;
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

        public event EventHandler<ApiRepository> ConfigurationChanged;  
    }
}
