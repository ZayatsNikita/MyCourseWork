using DL.Entities;
using DL.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DL.Repositories.Realization.MongoDbRepostories
{
    public class MongoDbClientRepository : IClientEntiryRepo
    {
        private readonly IMongoDatabase _database;

        private readonly MongoClient _client;

        public MongoDbClientRepository()
        {
            string connectionString = "mongodb://localhost:27017";
            
            _client = new MongoClient(connectionString); = 

            _database = _client.GetDatabase("TestDatabase"); ;
        }

        private void TestMethod()
        {
            using (var cursonr = _client.ListDatabases())
            {

            }
        }

        public int Create(ClientEntity model)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<ClientEntity> Read()
        {
            throw new NotImplementedException();
        }

        public ClientEntity ReadById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(ClientEntity model)
        {
            throw new NotImplementedException();
        }
    }
}
