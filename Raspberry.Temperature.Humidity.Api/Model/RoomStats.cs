using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace RaspberryTemperatureHumidityApi.Model
{
    public enum TemperatureUnit { Celsius = 1, Fahrenheit }

    public class RoomStats
    {
        [BsonIgnoreIfDefault]
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string RoomName { get; set; }
        [BsonRepresentation(BsonType.Double, AllowTruncation = true)]
        public double Temperature { get; set; }
        [BsonRepresentation(BsonType.Double, AllowTruncation = true)]
        public double Humidity { get; set; }
        public TemperatureUnit TemperatureUnit { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}

