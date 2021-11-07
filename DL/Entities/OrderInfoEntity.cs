using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace DL.Entities
{
    public class OrderInfoEntity
    {
        [BsonId]
        public int Id { get; set; }

        public int OrderNumber { get; set; }
        
        public int CountOfServicesRendered { get; set; }
        
        public int ServiceId { get; set; }
    }
}
