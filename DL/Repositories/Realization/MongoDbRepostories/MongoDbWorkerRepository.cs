using DL.Entities;
using DL.Repositories.Abstract;
using MongoDB.Bson;
using MongoDB.Driver;
using DL.Infrastructure.Constants;
using System.Collections.Generic;

namespace DL.Repositories.Realization.MongoDbRepostories
{
    public class MongoDbWorkerRepository : IWorkerEntityRepo
    {
        private readonly IMongoDatabase _database;

        private readonly MongoClient _client;

        public MongoDbWorkerRepository()
        {
            string connectionString = MongoDbConstansts.ConnectionString;

            _client = new MongoClient(connectionString);

            _database = _client.GetDatabase(MongoDbConstansts.DbName);
        }

        private IMongoCollection<BsonDocument> Collection => _database.GetCollection<BsonDocument>(MongoDbConstansts.WorkersCollectionName);

        public int Create(WorkerEntity model)
        {
            var document = model.ToBsonDocument();

            Collection.InsertOne(document);

            return model.PassportNumber;
        }

        public void Delete(int id)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("_id", id);

            var result = Collection.DeleteOne(filter);
        }

        public List<WorkerEntity> Read()
        {
            var filter = new BsonDocument();

            var selectedEntities = Collection.Find(filter).ToList();

            var workers = new List<WorkerEntity>();

            foreach (var item in selectedEntities)
            {
                var passwordNumber = item["_id"].AsInt32;

                var personalData = item["PersonalData"].AsString;

                workers.Add(new WorkerEntity { PassportNumber = passwordNumber, PersonalData = personalData });
            }

            return workers;
        }

        public WorkerEntity ReadById(int id)
        {
            var filter = new BsonDocument("_id", id);

            var selectedEntity = Collection.Find(filter).Limit(1).ToList()[0];
            
            var passwordNumber = selectedEntity["_id"].AsInt32;

            var personalData = selectedEntity["PersonalData"].AsString;

            return new WorkerEntity { PassportNumber = passwordNumber, PersonalData = personalData };
        }

        public void Update(WorkerEntity model)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("_id", model.PassportNumber);

            var update = Builders<BsonDocument>.Update
                .Set("PersonalData", model.PersonalData);

            var result = Collection.UpdateOne(filter, update);

        }
    }
}
