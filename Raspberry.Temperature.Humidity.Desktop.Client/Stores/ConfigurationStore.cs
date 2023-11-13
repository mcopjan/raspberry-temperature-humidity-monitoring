using Raspberry.Temperature.Humidity.WPF.Desktop.Client.Models;
using Raspberry.Temperature.Humidity.WPF.Desktop.Client.Repositories;
using System;
using System.Windows.Documents;

namespace Raspberry.Temperature.Humidity.WPF.Desktop.Client.Stores
{
    public class ConfigurationStore
    {
        private ApiRepository _apiRepository;
        private RoomStats [] _roomStats;
        private bool? _isConnectionSuccessful;
        public ApiRepository ApiRepository
        {
            get => _apiRepository;
            set
            {
                _apiRepository = value;
                ConfigurationChanged?.Invoke(this, _apiRepository);
            }
        }

        public bool? IsConnectionSuccessful
        {
            get => _isConnectionSuccessful;
            set
            {
                _isConnectionSuccessful = value;
                TestConnectionResultChanged?.Invoke(this, _isConnectionSuccessful);
            }
        }

        public RoomStats []  RoomsData
        {
            get => _roomStats;
            set
            {
                _roomStats = value;
                OnRoomStatsUpdated?.Invoke(this, _roomStats);
            }
        }

        public event EventHandler<ApiRepository> ConfigurationChanged;
        public event EventHandler<bool?> TestConnectionResultChanged;
        public event EventHandler<RoomStats[]> OnRoomStatsUpdated;
    }
}
