using DL.Entities;
using DL.Repositories.Abstract;
using MongoDB.Bson;
using MongoDB.Driver;
using DL.Infrastructure.Constants;
using System.Collections.Generic;

namespace DL.Repositories.Realization.MongoDbRepostories
{
    public class MongoDbUserRepository : IUserEntityRepo
    {
        private const string UserFieldName = "User";

        private readonly IMongoDatabase _database;

        private readonly MongoClient _client;

        public MongoDbUserRepository()
        {
            string connectionString = MongoDbConstansts.ConnectionString;

            _client = new MongoClient(connectionString);

            _database = _client.GetDatabase(MongoDbConstansts.DbName);
        }

        private IMongoCollection<BsonDocument> CollectionForUser => _database.GetCollection<BsonDocument>(MongoDbConstansts.WorkersCollectionName);

        public int Create(UserEntity model)
        {
            var filter = new BsonDocument("_id", model.WorkerId);

            var bsonWorker = CollectionForUser.Find(filter).Limit(1).ToList()[0];

            var bsonElement = new BsonDocument
            {
                {"Password", model.Password },
                {"Login", model.Login },
                { "Id", model.WorkerId },
                { "WorkerId", model.WorkerId }
            };

            bsonWorker.Add(UserFieldName, bsonElement);

            CollectionForUser.ReplaceOne(filter, bsonWorker);

            return model.WorkerId;
        }

        public void Delete(int id)
        {
            var filter = new BsonDocument("_id", id);

            var document = CollectionForUser.Find(filter).ToList()[0];

            var update = Builders<BsonDocument>.Update
                .Unset(UserFieldName);

            var result = CollectionForUser.UpdateOne(filter, update);
        }

        public List<UserEntity> Read()
        {
            var filter = Builders<BsonDocument>.Filter.Ne(UserFieldName, BsonNull.Value);

            var workersAndUsers = CollectionForUser.Find(filter).ToList();

            List<UserEntity> users = new List<UserEntity>();

            foreach (var item in workersAndUsers)
            {
                var userBson = item[UserFieldName].AsBsonDocument;

                var login = userBson["Login"].AsString;

                var password = userBson["Password"].AsString;

                var workerId = item["_id"].AsInt32;

                var user = new UserEntity
                {
                    Id = workerId,
                    Login = login,
                    Password = password,
                    WorkerId = workerId,
                };

                users.Add(user);
            }

            return users;
        }

        public UserEntity ReadById(int id)
        {
            var filter = new BsonDocument("_id", id);

            var workerUserPair = CollectionForUser.Find(filter).ToList()[0];

            var userBson = workerUserPair[UserFieldName].AsBsonDocument;

            var login = userBson["Login"].AsString;

            var password = userBson["Password"].AsString;

            var workerId = workerUserPair["_id"].AsInt32;

            var user = new UserEntity
            {
                Id = workerId,
                Login = login,
                Password = password,
                WorkerId = workerId,
            };

            return user;
        }

        public void Update(UserEntity model)
        {
            var filter = new BsonDocument("_id", model.WorkerId);

            var bsonWorker = CollectionForUser.Find(filter).Limit(1).ToList()[0];

            var bsonElement = new BsonDocument
            {
                {"Password", model.Password },
                {"Login", model.Login },
                { "Id", bsonWorker.GetValue("_id").AsInt32 },
                { "WorkerId", bsonWorker.GetValue("_id").AsInt32 }
            };

            bsonWorker.Add(UserFieldName, bsonElement);

            CollectionForUser.ReplaceOne(filter, bsonWorker);
        }
    }
}
