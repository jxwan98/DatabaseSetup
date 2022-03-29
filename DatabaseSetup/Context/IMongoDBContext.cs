using MongoDB.Driver;

namespace DatabaseSetup.Context
{
    public interface IMongoDBContext
    {
        IMongoCollection<T> GetCollectionFromGlobalDB<T>(string name);
        IMongoCollection<T> GetCollectionFromUserDB<T>(string name);
    }
}