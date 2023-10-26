using Raspberry.Temperature.Humidity.WPF.Desktop.Client.Repositories;
using Raspberry.Temperature.Humidity.WPF.Desktop.Client.Stores;
using System.Collections.ObjectModel;
using System.Linq;

namespace Raspberry.Temperature.Humidity.WPF.Desktop.Client.Models
{
    public class RoomsListViewModel : ViewModelBase
    {
        private ObservableCollection<Room> _availableRooms = new ObservableCollection<Room>();
        private ApiRepository _apiRepository;

        public ObservableCollection<Room> AvailableRooms
        {
            get
            {
                return _availableRooms;
            }
            set
            {
                _availableRooms = value;
                AnyRoomsAvailable = true;
                OnPropertyChanged(nameof(AvailableRooms));
            }
        }


        private bool _anyRoomsAvailable;
        public bool  AnyRoomsAvailable
        {
            get
            {
                return _anyRoomsAvailable;
            }
            set
            {
                _anyRoomsAvailable = value;
                OnPropertyChanged(nameof(AnyRoomsAvailable));
            }
        }


        public RoomsListViewModel(ConfigurationStore store)
        {
            
            store.ConfigurationChanged += Store_ConfigurationChanged1;
        }

        private async void Store_ConfigurationChanged1(object? sender, Configuration e)
        {
            _apiRepository = new ApiRepository(e.ApiEndpointUrl);
            var rooms = await _apiRepository.GetRoomNames();
            AvailableRooms = new ObservableCollection<Room>(rooms.Select(s => new Room { Name = s }));
        }

    }
}
