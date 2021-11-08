using MongoDB.Bson.Serialization.Attributes;

namespace DL.Entities
{
    public class ServiceComponentsEntity : IIdEntity
    {
        [BsonId]
        public int Id { get; set; }
        public int ServiceId { get; set; }
        public int ComponetId { get; set; }
    }
}
