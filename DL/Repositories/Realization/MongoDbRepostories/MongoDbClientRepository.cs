using DL.Entities;
using DL.Infrastructure.Constants;
using DL.Repositories.Abstract;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;

namespace DL.Repositories.Realization.MongoDbRepostories
{
    public class MongoDbClientRepository : IClientEntiryRepo
    {
        private readonly IMongoDatabase _database;

        private readonly MongoClient _client;

        public MongoDbClientRepository()
        {
            string connectionString = "mongodb://localhost:27017";
            
            _client = new MongoClient(connectionString);  

            _database = _client.GetDatabase("TestDatabase");
        }

        private IMongoCollection<ClientEntity> Collection => _database.GetCollection<ClientEntity>(MongoDbConstansts.ClientsCollectionName);

        public int Create(ClientEntity model)
        {
            model.Id = GenerateId();

            Collection.InsertOne(model);

            return model.Id;
        }

        public void Delete(int id)
        {
            var filter = Builders<ClientEntity>.Filter.Eq("_id", id);
            
            var result = Collection.DeleteOne(filter);
        }

        public List<ClientEntity> Read()
        {
            var filter = new BsonDocument();
            var clients = Collection.Find(filter).ToList();
            return clients;
        }

        public ClientEntity ReadById(int id)
        {
            var filter = new BsonDocument("_id", id);
            
            var client = Collection.Find(filter).Limit(1).ToList();
            
            return client[0];
        }

        public void Update(ClientEntity model)
        {
            var filter = Builders<ClientEntity>.Filter.Eq("_id", model.Id);
           
            var update = Builders<ClientEntity>.Update.Set("Title", model.Title).Set("ContactInformation", model.ContactInformation);

            var result = Collection.UpdateOne(filter, update);
        }

        private int GenerateId()
        {
            var filter = new BsonDocument();

            var latestNoteId = Collection.Find(filter)
                .Sort(new SortDefinitionBuilder<ClientEntity>()
                .Descending("$natural"))
                .Limit(1).FirstOrDefault()?.Id ?? 0;

            return ++latestNoteId;
        }
    }
}
