using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HelloWorldMongoDB.Helpers
{
    public class MongoHelper<T>
    {
        private readonly IMongoCollection<T> _collection;

        public MongoHelper(string connectionString, string databaseName, string collectionName)
        {
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase(databaseName);
            _collection = database.GetCollection<T>(collectionName);
        }

        // Create
        public async Task CreateAsync(T document)
        {
            await _collection.InsertOneAsync(document);
        }

        // Read
        public async Task<T> GetByIdAsync(string id)
        {
            var filter = Builders<T>.Filter.Eq("_id", ObjectId.Parse(id));
            return await _collection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }

        // Update
        public async Task<bool> UpdateAsync(string id, T document)
        {
            var filter = Builders<T>.Filter.Eq("_id", ObjectId.Parse(id));
            var result = await _collection.ReplaceOneAsync(filter, document);

            return result.ModifiedCount > 0;
        }

        // Delete
        public async Task<bool> DeleteAsync(string id)
        {
            var filter = Builders<T>.Filter.Eq("_id", ObjectId.Parse(id));
            var result = await _collection.DeleteOneAsync(filter);

            return result.DeletedCount > 0;
        }
    }
}
