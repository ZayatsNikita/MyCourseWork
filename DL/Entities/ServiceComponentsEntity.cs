using MongoDB.Bson.Serialization.Attributes;

namespace DL.Entities
{
    public class ServiceComponentsEntity
    {
        [BsonId]
        public int Id { get; set; }
        public int ServiceId { get; set; }
        public int ComponetId { get; set; }
    }
}
