using Raspberry.Temperature.Humidity.Desktop.Client.Commands;
using Raspberry.Temperature.Humidity.Desktop.Client.Misc;
using Raspberry.Temperature.Humidity.WPF.Desktop.Client.Commands;
using Raspberry.Temperature.Humidity.WPF.Desktop.Client.Repositories;
using Raspberry.Temperature.Humidity.WPF.Desktop.Client.Stores;
using Raspberry.Temperature.Humidity.WPF.Desktop.Client.ViewModels;
using System.IO;
using System.Windows.Input;

namespace Raspberry.Temperature.Humidity.WPF.Desktop.Client.Models
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly ConfigurationStore _configurationStore;

        public RoomsListViewModel RoomsListViewModel { get; }
        public ChartsViewModel ChartsViewModel { get; set; }
        public ViewModelBase CurrentModalViewModel => _modalNavigationStore.CurrentViewModel;
        public ICommand CommandAddConfiguration { get; }
        public ICommand CommandDeleteConfiguration { get; }
        public ICommand CommandExit { get; }
        public bool IsModalOpened => _modalNavigationStore.IsOpen;

        public MainWindowViewModel(ConfigurationStore configurationStore, ModalNavigationStore modalNavigationStore)
        {
            _configurationStore = configurationStore;
            _modalNavigationStore = modalNavigationStore;
            RoomsListViewModel = new RoomsListViewModel(_configurationStore);
            ChartsViewModel = new ChartsViewModel(_configurationStore);
            CommandAddConfiguration = new AddConfigurationCommand(_modalNavigationStore,_configurationStore);
            CommandDeleteConfiguration = new DeleteConfigurationCommand();
            CommandExit = new ExitCommand();
            _modalNavigationStore.CurrentViewModelChanged += _modalNavigationStore_CurrentViewModelChanged;
            CheckIfConfigurationAlreadyProvided();
        }

        private void CheckIfConfigurationAlreadyProvided()
        {
            if (File.Exists(Constants.ConfigFileName))
            {
                string url = File.ReadAllText(Constants.ConfigFileName);
                _configurationStore.ApiRepository = new ApiRepository(url);
            }
        }

        private void _modalNavigationStore_CurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentModalViewModel));
            OnPropertyChanged(nameof(IsModalOpened));
            
        }
    }
}
