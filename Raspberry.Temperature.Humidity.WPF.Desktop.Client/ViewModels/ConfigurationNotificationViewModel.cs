﻿using Raspberry.Temperature.Humidity.WPF.Desktop.Client.Stores;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
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
        private const string ConfigFileName = "config.txt";

        private ICommand _clickCommand;

        public event EventHandler OnRequestClose;

        public ICommand ClickSaveCommand
        {
            get
            {
                return _clickCommand ?? (_clickCommand = new CommandHandler(() => OnSaveConfiguration(), () => CanExecute));
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

        public void OnSaveConfiguration()
        {
            _store.Configuration = new Configuration() { ApiUrl = ConfigUrl };
            SaveConfigurationUrlInFile();
            OnRequestClose(this, new EventArgs());
        }


        private void SaveConfigurationUrlInFile()
        {
            if (!File.Exists(ConfigFileName)) 
            {
                File.WriteAllText(ConfigFileName, ConfigUrl);
            }
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

        

        private ConfigurationStore _store;

        public event PropertyChangedEventHandler? PropertyChanged;

        public ConfigurationNotificationViewModel(ConfigurationStore store)
        {
            _store = store;
            if (File.Exists(ConfigFileName))
            {
                ConfigUrl = File.ReadAllText(ConfigFileName);
            }
        }

        
    }
}
