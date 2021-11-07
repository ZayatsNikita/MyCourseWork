using DL.Entities;
using DL.Infrastructure.Constants;
using DL.Repositories.Abstract;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;

namespace DL.Repositories.Realization.MongoDbRepostories
{

    public class MongoDbComponentRepository : IComponetEntityRepository
    {
        private readonly IMongoDatabase _database;

        private readonly MongoClient _client;

        public MongoDbComponentRepository()
        {
            string connectionString = "mongodb://localhost:27017";

            _client = new MongoClient(connectionString);

            _database = _client.GetDatabase("TestDatabase");
        }

        private IMongoCollection<ComponetEntity> Collection => _database.GetCollection<ComponetEntity>(MongoDbConstansts.ComponentsCollectionName);

        public int Create(ComponetEntity model)
        {
            model.Id = GenerateId();

            Collection.InsertOne(model);

            return model.Id;
        }

        public void Delete(int id)
        {
            var filter = Builders<ComponetEntity>.Filter.Eq("_id", id);

            var result = Collection.DeleteOne(filter);
        }

        public List<ComponetEntity> Read()
        {
            var filter = new BsonDocument();
            
            var components = Collection.Find(filter).ToList();
            
            return components;
        }

        public ComponetEntity ReadById(int id)
        {
            var filter = new BsonDocument("_id", id);

            var component = Collection.Find(filter).Limit(1).ToList();

            return component[0];
        }

        public void Update(ComponetEntity model)
        {
            var filter = Builders<ComponetEntity>.Filter.Eq("_id", model.Id);

            var update = Builders<ComponetEntity>.Update
                .Set("Price", model.Price)
                .Set("Title", model.Title)
                .Set("ProductionStandards", model.ProductionStandards);

            var result = Collection.UpdateOne(filter, update);
        }

        private int GenerateId()
        {
            var filter = new BsonDocument();

            var latestNoteId = Collection.Find(filter)
                .Sort(new SortDefinitionBuilder<ComponetEntity>()
                .Descending("$natural"))
                .Limit(1).FirstOrDefault()?.Id ?? 0;

            return ++latestNoteId;
        }
    }

}
