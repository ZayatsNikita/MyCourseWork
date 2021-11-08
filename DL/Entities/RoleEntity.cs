using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace DL.Entities
{
    public class RoleEntity : IIdEntity
    {
        [BsonId]
        public int Id { get; set; }
        
        public string Title{ get; set; }
        
        public string Description { get; set; }
    }
}
