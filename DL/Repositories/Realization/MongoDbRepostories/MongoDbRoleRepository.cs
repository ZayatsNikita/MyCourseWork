using DL.Entities;
using System.Linq;
using DL.Repositories.Abstract;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using DL.Infrastructure.Constants;

namespace DL.Repositories.Realization.MongoDbRepostories
{
    public class MongoDbRoleRepository : IRoleEntityRepository
    {
        private readonly IMongoDatabase _database;

        private readonly MongoClient _client;

        public MongoDbRoleRepository()
        {
            string connectionString = "mongodb://localhost:27017";

            _client = new MongoClient(connectionString);

            _database = _client.GetDatabase("TestDatabase");
        }

        private IMongoCollection<RoleEntity> Collection => _database.GetCollection<RoleEntity>(MongoDbConstansts.RolesCollectionName);

        public int Create(RoleEntity model)
        {
            model.Id = GenerateId();

            Collection.InsertOne(model);

            return model.Id;
        }

        public void Delete(int id)
        {
            var filter = Builders<RoleEntity>.Filter.Eq("_id", id);

            var result = Collection.DeleteOne(filter);
        }

        public List<RoleEntity> Read()
        {
            var filter = new BsonDocument();
            
            var roles = Collection.Find(filter).ToList();
            
            return roles;
        }

        public RoleEntity ReadById(int id)
        {
            var filter = new BsonDocument("_id", id);

            var roles = Collection.Find(filter).Limit(1).ToList();

            return roles[0];
        }

        public void Update(RoleEntity model)
        {
            var filter = Builders<RoleEntity>.Filter.Eq("_id", model.Id);

            var update = Builders<RoleEntity>.Update
                .Set("Title", model.Title)
                .Set("Description", model.Description);

            var result = Collection.UpdateOne(filter, update);
        }

        private int GenerateId()
        {
            var filter = new BsonDocument();

            var latestNoteId = Collection.Find(filter)
                .Sort(new SortDefinitionBuilder<RoleEntity>()
                .Descending("$natural"))
                .Limit(1).FirstOrDefault()?.Id ?? 0;

            return ++latestNoteId;
        }
    }
}
