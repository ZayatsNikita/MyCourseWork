using DL.Entities;
using DL.Repositories.Abstract;
using MongoDB.Bson;
using MongoDB.Driver;
using DL.Infrastructure.Constants;
using System.Collections.Generic;


namespace DL.Repositories.Realization.MongoDbRepostories
{
    public class MongoDbOrdersRepository : IOrderEntityRepository
    {
        private readonly IMongoDatabase _database;

        private readonly MongoClient _client;

        public MongoDbOrdersRepository()
        {
            string connectionString = MongoDbConstansts.ConnectionString;

            _client = new MongoClient(connectionString);

            _database = _client.GetDatabase(MongoDbConstansts.DbName);
        }

        private IMongoCollection<OrderEntity> Collection => _database.GetCollection<OrderEntity>(MongoDbConstansts.OrdersCollectionName);

        public int Create(OrderEntity model)
        {
            model.Id = GenerateId();

            Collection.InsertOne(model);

            return model.Id;
        }

        public void Delete(int id)
        {
            var filter = Builders<OrderEntity>.Filter.Eq("_id", id);

            var result = Collection.DeleteOne(filter);
        }

        public List<OrderEntity> Read()
        {
            var filter = new BsonDocument();

            var orders = Collection.Find(filter).ToList();

            return orders;
        }

        public OrderEntity ReadById(int id)
        {
            var filter = new BsonDocument("_id", id);

            var orders = Collection.Find(filter).Limit(1).ToList();

            return orders[0];
        }

        public List<OrderEntity> ReadComplitedOrders()
        {
            var filter = Builders<OrderEntity>.Filter.Ne("CompletionDate", BsonNull.Value);

            var orders = Collection.Find(filter).ToList();

            return orders;
        }

        public List<OrderEntity> ReadOutstandingOrders()
        {
            var filter = Builders<OrderEntity>.Filter.Eq("CompletionDate", BsonNull.Value);

            var orders = Collection.Find(filter).ToList();

            return orders;
        }

        public void Update(OrderEntity model)
        {
            var filter = Builders<OrderEntity>.Filter.Eq("_id", model.Id);

            var update = Builders<OrderEntity>.Update
                .Set("ClientId", model.ClientId)
                .Set("ManagerId", model.ManagerId)
                .Set("MasterId", model.MasterId)
                .Set("StartDate", model.StartDate)
                .Set("CompletionDate", model.CompletionDate);

            var result = Collection.UpdateOne(filter, update);
        }

        private int GenerateId()
        {
            var filter = new BsonDocument();

            var latestNoteId = Collection.Find(filter)
                .Sort(new SortDefinitionBuilder<OrderEntity>()
                .Descending("$natural"))
                .Limit(1).FirstOrDefault()?.Id ?? 0;

            return ++latestNoteId;
        }
    }
}
