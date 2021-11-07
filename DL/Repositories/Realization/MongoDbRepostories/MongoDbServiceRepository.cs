using DL.Entities;
using DL.Infrastructure.Constants;
using DL.Repositories.Abstract;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;

namespace DL.Repositories.Realization.MongoDbRepostories
{
    public class MongoDbServiceRepository : IServiceEntityRepository
    {
        private readonly IMongoDatabase _database;

        private readonly MongoClient _client;

        public MongoDbServiceRepository()
        {
            string connectionString = "mongodb://localhost:27017";

            _client = new MongoClient(connectionString);

            _database = _client.GetDatabase("TestDatabase");
        }

        private IMongoCollection<ServiceEntity> Collection => _database.GetCollection<ServiceEntity>(MongoDbConstansts.ServicesCollectionName);

        public int Create(ServiceEntity model)
        {
            model.Id = GenerateId();

            Collection.InsertOne(model);

            return model.Id;
        }

        public void Delete(int id)
        {
            var filter = Builders<ServiceEntity>.Filter.Eq("_id", id);

            var result = Collection.DeleteOne(filter);
        }

        public List<ServiceEntity> Read()
        {
            var filter = new BsonDocument();

            var services = Collection.Find(filter).ToList();

            return services;
        }

        public ServiceEntity ReadById(int id)
        {
            var filter = new BsonDocument("_id", id);

            var service = Collection.Find(filter).Limit(1).ToList();

            return service[0];
        }

        public void Update(ServiceEntity model)
        {
            var filter = Builders<ServiceEntity>.Filter.Eq("_id", model.Id);

            var update = Builders<ServiceEntity>.Update
                .Set("Price", model.Price)
                .Set("Description", model.Description)
                .Set("Title", model.Title);

            var result = Collection.UpdateOne(filter, update);
        }

        private int GenerateId()
        {
            var filter = new BsonDocument();

            var latestNoteId = Collection.Find(filter)
                .Sort(new SortDefinitionBuilder<ServiceEntity>()
                .Descending("$natural"))
                .Limit(1).FirstOrDefault()?.Id ?? 0;

            return ++latestNoteId;
        }
    }
}
