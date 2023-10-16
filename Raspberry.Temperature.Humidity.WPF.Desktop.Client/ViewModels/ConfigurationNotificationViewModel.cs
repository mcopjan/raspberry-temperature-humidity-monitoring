using Raspberry.Temperature.Humidity.WPF.Desktop.Client.Stores;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Raspberry.Temperature.Humidity.WPF.Desktop.Client.Models
{
    public class ConfigurationNotificationViewModel : ViewModelBase
    {
		private string _configUrl;
        private bool _isOpened;

        private ICommand _clickCommand;

        public Action CloseAction { get; set; }

        public ICommand ClickSaveCommand
        {
            get
            {
                return _clickCommand ?? (_clickCommand = new CommandHandler(() => MyAction(), () => CanExecute));
            }
        }
        public bool CanExecute
        {
            get
            {
                // check if executing is allowed, i.e., validate, check if a process is running, etc. 
                return true;
            }
        }

        public void MyAction()
        {
            _store.Configuration = new Configuration() { ApiUrl = ConfigUrl };
        }

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

        public bool IsOpen
        {
            get
            {
                return _isOpened;
            }
            set
            {
                _isOpened = value;
                OnPropertyChanged(nameof(IsOpen));

            }
        }

        

        private ConfigurationStore _store;

        public event PropertyChangedEventHandler? PropertyChanged;

        public ConfigurationNotificationViewModel(ConfigurationStore store)
        {
            _store = store;
            ConfigUrl = "192.168.5.136";

        }

        
    }
}
