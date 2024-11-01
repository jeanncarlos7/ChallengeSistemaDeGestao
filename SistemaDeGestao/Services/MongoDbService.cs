using Microsoft.Extensions.Options;
using SistemaDeGestao.Settings;
using MongoDB.Driver;
using SistemaDeGestao.Models;

namespace SistemaDeGestao.Services
{
    public class MongoDbService
    {
        private readonly IMongoCollection<ClienteModel> _collection;

        public MongoDbService(IOptions<MongoDbSettings> mongoDbSettings)
        {
            var mongoClient = new MongoClient(mongoDbSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(mongoDbSettings.Value.DatabaseName);
            _collection = mongoDatabase.GetCollection<ClienteModel>(mongoDbSettings.Value.CollectionName);
        }

        public async Task<List<ClienteModel>> GetAsync() =>
            await _collection.Find(_ => true).ToListAsync();

        public async Task<ClienteModel> GetByIdAsync(string id) =>
            await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(ClienteModel newEntity) =>
            await _collection.InsertOneAsync(newEntity);

        public async Task UpdateAsync(string id, ClienteModel updatedEntity) =>
            await _collection.ReplaceOneAsync(x => x.Id == id, updatedEntity);

        public async Task RemoveAsync(string id) =>
            await _collection.DeleteOneAsync(x => x.Id == id);
    }
}
