
using DL.Entities;
using DL.Infrastructure.Constants;
using DL.Repositories.Abstract;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;

namespace DL.Repositories.Realization.MongoDbRepostories
{
    public class MongoDbUserRolePairsRepository : IUserRoleRepository
    {
        private readonly IMongoDatabase _database;

        private readonly MongoClient _client;

        public MongoDbUserRolePairsRepository()
        {
            string connectionString = MongoDbConstansts.ConnectionString;

            _client = new MongoClient(connectionString);

            _database = _client.GetDatabase(MongoDbConstansts.DbName);
        }

        private IMongoCollection<UserRoleEntity> Collection => _database.GetCollection<UserRoleEntity>(MongoDbConstansts.UserRolePairsCollectionName);

        public int Create(UserRoleEntity model)
        {
            model.Id = GenerateId();

            Collection.InsertOne(model);

            return model.Id;
        }

        public void Delete(int id)
        {
            var filter = Builders<UserRoleEntity>.Filter.Eq("_id", id);

            var result = Collection.DeleteOne(filter);
        }

        public List<UserRoleEntity> Read()
        {
            var filter = new BsonDocument();

            var selectedEntities = Collection.Find(filter).ToList();

            return selectedEntities;
        }

        public UserRoleEntity ReadById(int id)
        {
            var filter = new BsonDocument("_id", id);

            return Collection.Find(filter).Limit(1).ToList()[0];
        }

        public void Update(UserRoleEntity model)
        {
            var filter = Builders<UserRoleEntity>.Filter.Eq("_id", model.Id);

            var update = Builders<UserRoleEntity>.Update
                .Set("RoleId", model.RoleId)
                .Set("UserId", model.UserId);

            var result = Collection.UpdateOne(filter, update);
        }

        private int GenerateId()
        {
            var filter = new BsonDocument();

            var latestNoteId = Collection.Find(filter)
                .Sort(new SortDefinitionBuilder<UserRoleEntity>()
                .Descending("$natural"))
                .Limit(1).FirstOrDefault()?.Id ?? 0;

            return ++latestNoteId;
        }
    }
}
