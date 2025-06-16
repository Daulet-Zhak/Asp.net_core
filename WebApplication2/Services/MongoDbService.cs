using MongoDB.Driver;

namespace WebApplication2.Services
{
    public class MongoDbService
    {
        private readonly IConfiguration _config;
        private readonly IMongoDatabase? _database;
        public MongoDbService(IConfiguration configuration) {
            
            _config = configuration;
            var client = new MongoClient(_config.GetConnectionString("DBConnection"));
            _database = client.GetDatabase("taskdb");

        }
        public IMongoDatabase Database { get { return _database; } }
    }
}
