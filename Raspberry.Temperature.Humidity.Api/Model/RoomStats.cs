namespace RaspberryTemperatureHumidityApi.Model
{
    public enum TemperatureUnit { Celsius = 1, Fahrenheit }

    public class RoomStats
    {
        public string RoomName { get; set; }
        public float Temperature { get; set; }
        public float Humidity { get; set; }
        public TemperatureUnit TemperatureUnit { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}

