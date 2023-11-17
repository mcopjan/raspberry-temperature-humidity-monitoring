using MongoDB.Driver;
using MongoDB.Bson;
using RaspberryTemperatureHumidityApi.Model;
using MongoDB.Bson.Serialization;

namespace Raspberry.Temperature.Humidity.Api.Repository
{
    public static class MongoDbRepository
    {
        private static string connectionString;
        private static string databaseName = "RoomStatsDatabase";
        private static readonly MongoClient _client;

        static MongoDbRepository()
        {
            _client = new MongoClient(connectionString);
            string mongoHost = Environment.GetEnvironmentVariable("MONGO_HOST") ?? "localhost";
            string mongoPortStr = Environment.GetEnvironmentVariable("MONGO_PORT") ?? "27017";
            Console.WriteLine($"Mongo host set to {mongoHost} and mongo port to {mongoPortStr}");
            connectionString = $"mongodb://{mongoHost}:{mongoPortStr}";
        }


        public static async Task StoreData(RoomStats data,string roomName)
        {
            var database = _client.GetDatabase(databaseName);
            var collection = database.GetCollection<BsonDocument>(roomName);
            byte[] bsonBytes = data.ToBson();
            BsonDocument bsonDocument = BsonSerializer.Deserialize<BsonDocument>(bsonBytes);
            await collection.InsertOneAsync(bsonDocument);
        }

        public static async Task<List<RoomStats>> GetData(string roomName)
        {
            var database = _client.GetDatabase(databaseName);
            var collection = database.GetCollection<BsonDocument>(roomName);
            var documents = await collection.Find("{}").ToListAsync();

            List<RoomStats> allRoomStats = documents
            .Select(bsonDocument => BsonSerializer.Deserialize<RoomStats>(bsonDocument))
            .ToList();

            return allRoomStats;
        }
    }
}
