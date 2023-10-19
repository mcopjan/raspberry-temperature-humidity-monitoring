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
using System.Windows.Input;

namespace Raspberry.Temperature.Humidity.WPF.Desktop.Client.Models
{
    public class RoomsListViewModel : ViewModelBase
    {
        private ObservableCollection<Room> _availableRooms = new ObservableCollection<Room>();
        private ApiRepository _apiRepository;
        private ICommand _getRoomStatsCommand;

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

        


        public ICommand GetRoomStatsCommand
        {
            get
            {
                return _getRoomStatsCommand ?? (_getRoomStatsCommand = new CommandHandler(() => FetchRoomStats(), () => CanExecute));
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

        private void FetchRoomStats()
        {
            throw new NotImplementedException();
        }






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
