﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Raspberry.Temperature.Humidity.WPF.Desktop.Client.Models;
using System.Threading;

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

        public async Task<List<RoomStats>> GetRoomStats(string roomName)
        {
            var response = await _client.GetStringAsync($"http://{_apiUrl}/roomstats/{roomName}");
            var result = JsonConvert.DeserializeObject<List<RoomStats>>(response);
            return result;

        }

        public async Task<bool> IsApiEndpontAvailableAsync()
        {
            try
            {
                var cts = new CancellationTokenSource();
                cts.CancelAfter(TimeSpan.FromSeconds(2));
                var response = await _client.GetAsync($"http://{_apiUrl}/hello/", cts.Token);
                return response.IsSuccessStatusCode;
            }
            catch (Exception)
            {

                return false;
            }
            
        }
    }
}
