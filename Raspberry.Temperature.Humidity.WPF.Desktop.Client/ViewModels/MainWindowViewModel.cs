using Raspberry.Temperature.Humidity.WPF.Desktop.Client.Commands;
using Raspberry.Temperature.Humidity.WPF.Desktop.Client.Stores;
using Raspberry.Temperature.Humidity.WPF.Desktop.Client.ViewModels;
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
        public bool IsModalOpened => _modalNavigationStore.IsOpen;

        public MainWindowViewModel(ConfigurationStore configurationStore, ModalNavigationStore modalNavigationStore)
        {
            _configurationStore = configurationStore;
            _modalNavigationStore = modalNavigationStore;
            RoomsListViewModel = new RoomsListViewModel(_configurationStore);
            ChartsViewModel = new ChartsViewModel(_configurationStore);
            CommandAddConfiguration = new AddConfigurationCommand(_modalNavigationStore,_configurationStore);
            //
            //._modalNavigationStore.CurrentViewModel = new ConfigurationNotificationViewModel(_configurationStore);
            _modalNavigationStore.CurrentViewModelChanged += _modalNavigationStore_CurrentViewModelChanged;

        }

        private void _modalNavigationStore_CurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentModalViewModel));
            OnPropertyChanged(nameof(IsModalOpened));
            
        }
    }
}
