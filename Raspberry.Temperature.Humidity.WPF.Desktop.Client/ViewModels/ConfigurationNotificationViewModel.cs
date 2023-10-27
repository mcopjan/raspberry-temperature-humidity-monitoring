﻿using Raspberry.Temperature.Humidity.WPF.Desktop.Client.Commands;
using Raspberry.Temperature.Humidity.WPF.Desktop.Client.Stores;
using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Input;

namespace Raspberry.Temperature.Humidity.WPF.Desktop.Client.Models
{
    public class ConfigurationNotificationViewModel : ViewModelBase
    {
		private string _configUrl;
        private bool? _isConnectionSuccessful;
        private bool _isOpened;
        private const string ConfigFileName = "config.txt";

        public ICommand SaveConfigurationCommand { get; }
        public ICommand TestConnectionCommand { get; }

        public event EventHandler OnRequestClose;

        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly ConfigurationStore _configurationStore;



        //public void OnSaveConfiguration()
        //{
        //    _store.Configuration = new Configuration() { ApiEndpointUrl = ConfigUrl };
        //    SaveConfigurationUrlInFile();
        //    OnRequestClose(this, new EventArgs());
        //}


        //private void SaveConfigurationUrlInFile()
        //{
        //    if (!File.Exists(ConfigFileName)) 
        //    {
        //        File.WriteAllText(ConfigFileName, ConfigUrl);
        //    }
        //}

        
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
        //{
        //    get
        //    {
        //        return _isConnectionSuccessful;
        //    }
        //    set
        //    {
        //        _isConnectionSuccessful = value;
        //        OnPropertyChanged(nameof(IsConnectionSuccessful));

        //    }
        //}

        public event PropertyChangedEventHandler? PropertyChanged;

        public ConfigurationNotificationViewModel(ModalNavigationStore modalNavigationStore, ConfigurationStore configurationStore)
        {
            _configUrl = "192.168.5.136";
            _modalNavigationStore = modalNavigationStore;
            _configurationStore = configurationStore;
            SaveConfigurationCommand = new SaveConfigurationCommand(this, _modalNavigationStore, _configurationStore);
            TestConnectionCommand = new TestConnectionCommandAsync(this, _configurationStore);

            _configurationStore.TestConnectionResultChanged += _configurationStore_TestConnectionResultChanged;

            //if (File.Exists(ConfigFileName))
            //{
            //    ConfigUrl = File.ReadAllText(ConfigFileName);
            //}
            //_configurationStore = configurationStore;
        }

        private void _configurationStore_TestConnectionResultChanged(object? sender, bool? e)
        {
            OnPropertyChanged(nameof(IsConnectionSuccessful));
        }
    }
}
