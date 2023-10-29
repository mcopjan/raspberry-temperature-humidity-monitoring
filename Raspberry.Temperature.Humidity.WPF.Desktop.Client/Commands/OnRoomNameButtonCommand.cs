using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Documents;
using Raspberry.Temperature.Humidity.WPF.Desktop.Client.Models;
using Raspberry.Temperature.Humidity.WPF.Desktop.Client.Stores;

namespace Raspberry.Temperature.Humidity.WPF.Desktop.Client.Commands
{
    public class OnRoomNameButtonCommand : BaseCommandAsync
    {

        private readonly ConfigurationStore _configurationStore;

        public OnRoomNameButtonCommand(ConfigurationStore configurationStore)
        {

            _configurationStore = configurationStore;
        }

        public override async Task<List<RoomStats>> ExecuteAsync(object parameter)
        {
            string roomName = (((Room)parameter).Name);
            var result = await _configurationStore.ApiRepository.GetRoomStats(roomName);
            _configurationStore.RoomsData = result.ToArray();
            return result;
        }
    }
}
