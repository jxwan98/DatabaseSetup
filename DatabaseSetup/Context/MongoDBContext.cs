using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatabaseSetup.Context
{
    public class MongoDBContext : IMongoDBContext
    {
        private IMongoDatabase _globalDB { get; set; }
        private IMongoDatabase _userDB { get; set; }
        private MongoClient _mongoClient { get; set; }
        public IClientSessionHandle Session { get; set; }

        public MongoDBContext(IPopupDatabaseSettings settings)
        {
            var mongoSettings = MongoClientSettings.FromConnectionString(settings.ConnectionString);
            mongoSettings.ConnectTimeout = new TimeSpan(0, 0, 3);
            _mongoClient = new MongoClient(mongoSettings);
            _globalDB = _mongoClient.GetDatabase(settings.GlobalDatabaseName);
            _userDB = _mongoClient.GetDatabase(settings.UserDatabaseName);
        }

        public IMongoCollection<T> GetCollectionFromGlobalDB<T>(string name)
        {
            return _globalDB.GetCollection<T>(name);
        }

        public IMongoCollection<T> GetCollectionFromUserDB<T>(string name)
        {
            return _userDB.GetCollection<T>(name);
        }

    }
}
