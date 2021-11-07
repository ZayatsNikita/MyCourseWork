using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace DL.Entities
{
    public class ServiceEntity
    {
        [BsonId]
        public int Id { get; set; }

        public decimal Price { get; set; }
        
        public string Description { get; set; }
        
        public string Title { get; set; }
    }
}
