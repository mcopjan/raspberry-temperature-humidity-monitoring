using MongoDB.Driver;
using MongoDB.Bson;
using RaspberryTemperatureHumidityApi.Model;
using MongoDB.Bson.Serialization;

namespace Raspberry.Temperature.Humidity.Api.Repository
{
    public static class MongoDbRepository
    {
        private static string connectionString = "mongodb://192.168.5.208:27017";
        private static string databaseName = "RoomStatsDatabase";
        private static readonly MongoClient _client;

        static MongoDbRepository()
        {
            _client = new MongoClient(connectionString);
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

            if (documents[0].Contains("_id"))
            {
                // Access the value of the _id field
                BsonValue idValue = documents[0]["_id"];

                // Check if the value is a valid ObjectId
                if (idValue.IsObjectId)
                {
                    string idString = idValue.AsObjectId.ToString();
                    Console.WriteLine($"Found valid ObjectId: {idString}");
                }
            }

            List<RoomStats> allRoomStats = documents
            .Select(bsonDocument => BsonSerializer.Deserialize<RoomStats>(bsonDocument))
            .ToList();

            return allRoomStats;
        }
    }
}
