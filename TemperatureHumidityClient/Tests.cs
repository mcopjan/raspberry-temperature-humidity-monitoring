using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemperatureHumidityClient
{
    internal class Tests
    {
        [Test]
        public async Task Test()
        {
            var data = await ApiRepository.GetRoomStats();
        }
    }
}
