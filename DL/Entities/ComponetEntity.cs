using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace DL.Entities
{
    public class ComponetEntity : IIdEntity
    {
        [BsonId]
        public int Id { get; set; }

        public decimal Price { get; set; }
        
        public string Title { get; set; }
        
        public string ProductionStandards { get; set; }
    }
}
