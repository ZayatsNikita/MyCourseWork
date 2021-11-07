using DL.Entities;
using DL.Repositories.Abstract;
using MongoDB.Bson;
using MongoDB.Driver;
using DL.Infrastructure.Constants;
using System.Collections.Generic;


namespace DL.Repositories.Realization.MongoDbRepostories
{
    public class MongoDbComponentServicePairRepository : IСomponetServiceEntityRepo
    {
        private readonly IMongoDatabase _database;

        private readonly MongoClient _client;

        public MongoDbComponentServicePairRepository()
        {
            string connectionString = MongoDbConstansts.ConnectionString;

            _client = new MongoClient(connectionString);

            _database = _client.GetDatabase(MongoDbConstansts.DbName);
        }

        private IMongoCollection<ServiceComponentsEntity> Collection => _database.GetCollection<ServiceComponentsEntity>(MongoDbConstansts.ComponentServicePairsCollectionName);

        public int Create(ServiceComponentsEntity model)
        {
            model.Id = GenerateId();

            Collection.InsertOne(model);

            return model.Id;
        }

        public void Delete(int id)
        {
            var filter = Builders<ServiceComponentsEntity>.Filter.Eq("_id", id);

            var result = Collection.DeleteOne(filter);
        }

        public List<ServiceComponentsEntity> Read()
        {
            var filter = new BsonDocument();

            var selectedEntities = Collection.Find(filter).ToList();

            return selectedEntities;
        }

        public ServiceComponentsEntity ReadById(int id)
        {
            var filter = new BsonDocument("_id", id);

            var selectedEntities = Collection.Find(filter).Limit(1).ToList();

            return selectedEntities[0];
        }

        public void Update(ServiceComponentsEntity model)
        {
            var filter = Builders<ServiceComponentsEntity>.Filter.Eq("_id", model.Id);

            var serviceDbRef = new MongoDBRef(MongoDbConstansts.ComponentsCollectionName, model.ComponetId);

            var componentDbRef = new MongoDBRef(MongoDbConstansts.ServicesCollectionName, model.ServiceId);

            var update = Builders<ServiceComponentsEntity>.Update
                .Set("ClientId", serviceDbRef)
                .Set("ManagerId", componentDbRef);

            var result = Collection.UpdateOne(filter, update);
        }

        private int GenerateId()
        {
            var filter = new BsonDocument();

            var latestNoteId = Collection.Find(filter)
                .Sort(new SortDefinitionBuilder<ServiceComponentsEntity>()
                .Descending("$natural"))
                .Limit(1).FirstOrDefault()?.Id ?? 0;

            return ++latestNoteId;
        }
    }
}
