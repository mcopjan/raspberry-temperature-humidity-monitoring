using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Raspberry.Temperature.Humidity.WPF.Desktop.Client.Repositories
{
    
    public class ApiRepository
    {
        private readonly string _apiUrl;
        private static HttpClient _client = new HttpClient();

        public ApiRepository(string apiUrl)
        {
            _apiUrl = apiUrl;
        }

        public async Task<List<string>> GetRoomNames()
        {
            var response = await _client.GetStringAsync($"http://{_apiUrl}/roomnames");
            var result = JsonConvert.DeserializeObject<List<string>>(response);
            return result.ToList();

        }
    }
}
