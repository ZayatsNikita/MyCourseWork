using DL.Entities;
using DL.Repositories.Abstract;
using MongoDB.Bson;
using MongoDB.Driver;
using DL.Infrastructure.Constants;
using System.Collections.Generic;

namespace DL.Repositories.Realization.MongoDbRepostories
{
    public class MongoDbOrderInfoRepository : IOrderInfoEntityRepository
    {
        private readonly IMongoDatabase _database;

        private readonly MongoClient _client;

        public MongoDbOrderInfoRepository()
        {
            string connectionString = MongoDbConstansts.ConnectionString;

            _client = new MongoClient(connectionString);

            _database = _client.GetDatabase(MongoDbConstansts.DbName);
        }

        private IMongoCollection<OrderInfoEntity> Collection => _database.GetCollection<OrderInfoEntity>(MongoDbConstansts.OrderInfoCollectionName);

        public int Create(OrderInfoEntity model)
        {
            model.Id = GenerateId();

            Collection.InsertOne(model);

            return model.Id;
        }

        public void Delete(int id)
        {
            var filter = Builders<OrderInfoEntity>.Filter.Eq("_id", id);

            var result = Collection.DeleteOne(filter);
        }

        public List<OrderInfoEntity> Read()
        {
            var filter = new BsonDocument();

            var selectedEntities = Collection.Find(filter).ToList();

            return selectedEntities;
        }

        public OrderInfoEntity ReadById(int id)
        {
            var filter = new BsonDocument("_id", id);

            var selectedEntities = Collection.Find(filter).Limit(1).ToList();

            return selectedEntities[0];
        }

        public void Update(OrderInfoEntity model)
        {
            var filter = Builders<OrderInfoEntity>.Filter.Eq("_id", model.Id);

            var orderDbRef = new MongoDBRef(MongoDbConstansts.OrdersCollectionName, model.OrderNumber);

            var componentServicePairDbRef = new MongoDBRef(MongoDbConstansts.ComponentServicePairsCollectionName, model.ServiceId);

            var update = Builders<OrderInfoEntity>.Update
                .Set("OrderNumber", orderDbRef)
                .Set("ServiceId", componentServicePairDbRef)
                .Set("CountOfServicesRendered", model.CountOfServicesRendered);

            var result = Collection.UpdateOne(filter, update);
        }

        private int GenerateId()
        {
            var filter = new BsonDocument();

            var latestNoteId = Collection.Find(filter)
                .Sort(new SortDefinitionBuilder<OrderInfoEntity>()
                .Descending("$natural"))
                .Limit(1).FirstOrDefault()?.Id ?? 0;

            return ++latestNoteId;
        }
    }
}
