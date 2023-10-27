using Raspberry.Temperature.Humidity.WPF.Desktop.Client.Commands;
using Raspberry.Temperature.Humidity.WPF.Desktop.Client.Repositories;
using Raspberry.Temperature.Humidity.WPF.Desktop.Client.Stores;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace Raspberry.Temperature.Humidity.WPF.Desktop.Client.Models
{
    public class RoomsListViewModel : ViewModelBase
    {
        private ObservableCollection<Room> _availableRooms = new ObservableCollection<Room>();

        public ICommand OnRoomNameButtonCommand { get;}

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
            OnRoomNameButtonCommand = new OnRoomNameButtonCommand(store);
            store.ConfigurationChanged += Store_ConfigurationChanged;
        }

        private async void Store_ConfigurationChanged(object? sender, ApiRepository e)
        {
            var rooms = await e.GetRoomNames();
            AvailableRooms = new ObservableCollection<Room>(rooms.Select(s => new Room { Name = s }));
        }

    }
}
