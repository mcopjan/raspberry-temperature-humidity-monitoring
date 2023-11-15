using System.Text.Json;
using RaspberryTemperatureHumidityApi.Model;

namespace Raspberry.Temperature.Humidity.Api.Repository
{
    public static class FileRepository
    {
        static string filePath = "rooms-stats.json";
        static object fileLock = new object();

        static FileRepository()
        {
            if(!File.Exists(filePath))
                File.Create(filePath);
        }
        

        public static List<RoomStats> ReadRoomsStatsFromFile()
        {
            lock (fileLock)
            {
                string jsonContent = File.ReadAllText(filePath);

                if (!string.IsNullOrEmpty(jsonContent))
                {
                    List<RoomStats> stats = JsonSerializer.Deserialize<List<RoomStats>>(jsonContent);
                    return stats;
                }
                return null;
            }
            
        }

        public static void WriteRoomStatsIntoFile(RoomStats data)
        {
            lock (fileLock)
            {
                List<RoomStats> stats;

                string jsonContent = File.ReadAllText(filePath);
                stats = !string.IsNullOrEmpty(jsonContent) ? JsonSerializer.Deserialize<List<RoomStats>>(jsonContent) : new List<RoomStats>();
                stats.Add(data);
                string updatedJsonContent = JsonSerializer.Serialize(stats);
                File.WriteAllText(filePath, updatedJsonContent);
            }
            
        }
    }
}
