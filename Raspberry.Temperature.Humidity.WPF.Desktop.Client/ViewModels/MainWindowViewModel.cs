using Raspberry.Temperature.Humidity.WPF.Desktop.Client.Services;
using Raspberry.Temperature.Humidity.WPF.Desktop.Client.Stores;
using Raspberry.Temperature.Humidity.WPF.Desktop.Client.ViewModels;
using Raspberry.Temperature.Humidity.WPF.Desktop.Client.Views;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Raspberry.Temperature.Humidity.WPF.Desktop.Client.Models
{
    public class MainWindowViewModel : ViewModelBase
    {
        IDialogService _dialogService =  new DialogService();
       // public AddConfigurationViewModel AddConfigurationModel { get; set; }

        private RoomsListViewModel _roomsListViewModel;
        public RoomsListViewModel RoomsListViewModel
        {
            get
            {
                return _roomsListViewModel;
            }
            set
            {
                _roomsListViewModel = value;
                OnPropertyChanged(nameof(RoomsListViewModel));
            }
        }

        public ChartsViewModel ChartsViewModel { get; set; }

        private const string ConfigFileName = "config.txt";


        private ConfigurationStore _store;
        private ICommand _addConfigCommand;
        private ICommand _deleteConfigCommand;
        public ICommand CommandAddConfiguration
        {
            get
            {
                return _addConfigCommand ?? (_addConfigCommand = new CommandHandler(() => OpenConfigNotification(), () => CanExecute));
            }
        }

        public ICommand CommandDeleteConfiguration
        {
            get
            {
                return _deleteConfigCommand ?? (_deleteConfigCommand = new CommandHandler(() => DeleteConfigurationFile(), () => CanExecute));
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

        public void OpenConfigNotification()
        {
            _dialogService.ShowDialog<ConfigurationNotificationViewModel>(result => {

                var test = result;
            });
        }

        public void DeleteConfigurationFile()
        {
            File.Delete(ConfigFileName);
        }



        public MainWindowViewModel(ConfigurationStore store)
        {
            _store = store;
            RoomsListViewModel = new RoomsListViewModel(store);
            ChartsViewModel = new ChartsViewModel(store);



        }


    }
}
