using Raspberry.Temperature.Humidity.WPF.Desktop.Client.Services;
using Raspberry.Temperature.Humidity.WPF.Desktop.Client.Stores;
using Raspberry.Temperature.Humidity.WPF.Desktop.Client.Views;
using System;
using System.Collections.Generic;
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
        public RoomsListViewModel RoomsListViewModel { get; set; }


        private ConfigurationStore _store;
        private ICommand _clickCommand;
        public ICommand ClickCommand
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
            _dialogService.ShowDialog<ConfigurationNotificationViewModel>(result => {

                var test = result;
            });
        }

        

        public MainWindowViewModel(ConfigurationStore store)
        {
            _store = store;
            RoomsListViewModel = new RoomsListViewModel(store);
            

        }


    }
}
