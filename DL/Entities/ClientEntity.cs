using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace DL.Entities
{
    public class ClientEntity : IIdEntity
    {
        [BsonId]
        public int Id { get; set; }

        public string ContactInformation { get; set; }
        
        public string Title { get; set; }
    }
}
