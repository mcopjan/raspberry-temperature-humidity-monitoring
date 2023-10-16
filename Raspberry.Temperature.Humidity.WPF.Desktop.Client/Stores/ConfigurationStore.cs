using Raspberry.Temperature.Humidity.WPF.Desktop.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                ConfigurationChanged?.Invoke(this,_config);
            }
        }

        public event EventHandler<Configuration> ConfigurationChanged;  
    }
}
