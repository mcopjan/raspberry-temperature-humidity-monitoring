using Newtonsoft.Json;
using RaspberryTemperatureHumidityClient.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemperatureHumidityClient
{
    
    internal class ApiRepository
    {
        private static HttpClient _client = new HttpClient();

        public static async Task<List<RoomStats>> GetRoomStats()
        {
            var response = await _client.GetAsync("http://192.168.5.136/roomstats");
            var str = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<RoomStats>>(str);

        }
    }
}
