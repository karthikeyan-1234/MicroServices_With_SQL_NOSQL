using ERPModels;

using MongoDB.Driver;

namespace InventoryAPI.Infrastructure.Utilities
{
    public class MongoDBHelper<T> where T : class
    {
        private string ?connectionString;
        private string ?databaseName;
        public static IMongoDatabase ?mongoDB;

        public MongoDBHelper(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("InventoryMongoConn");

        }

        public IMongoCollection<T> GetMongoDBCollection(string collectionName)
        {
            connectionString = "mongodb://localhost:27017";
            databaseName = "InventoryDB";
            var client = new MongoClient(connectionString);
            mongoDB = client.GetDatabase(databaseName);
            return mongoDB.GetCollection<T>(collectionName);
        }
    }
}
