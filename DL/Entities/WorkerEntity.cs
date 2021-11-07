using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace DL.Entities
{
    public class WorkerEntity
    {
        public string PersonalData { get; set; }

        [BsonId]
        public int PassportNumber { get; set; }
    }
}
