using Raspberry.Temperature.Humidity.WPF.Desktop.Client.Repositories;
using Raspberry.Temperature.Humidity.WPF.Desktop.Client.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Raspberry.Temperature.Humidity.WPF.Desktop.Client.Models
{
    public class RoomsListViewModel : ViewModelBase
    {
        private ObservableCollection<Room> _availableRooms;
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
                OnPropertyChanged(nameof(AvailableRooms));
            }
        }



        public bool AnyRoomsAvailable => true;// _availableRooms.Count > 0;


   

        public RoomsListViewModel(ConfigurationStore store)
        {
            
            store.ConfigurationChanged += Store_ConfigurationChanged1;
        }

        private async void Store_ConfigurationChanged1(object? sender, Configuration e)
        {
            Console.WriteLine("Event raised");
            _apiRepository = new ApiRepository(e.ApiUrl);
            var rooms = await _apiRepository.GetRoomNames();
            AvailableRooms = new ObservableCollection<Room>(rooms.Select(s => new Room { Name = s }));
        }

    }
}
