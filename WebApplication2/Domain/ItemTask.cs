using MongoDB.Bson.Serialization.Attributes;

namespace WebApplication2.Domain
{
    public enum ItemTaskStatus { Pending, InProgress, Done }
    public class ItemTask
        
    {
        [BsonId]
        [BsonElement("_id"), BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ItemTaskStatus Status { get; set; }
        public DateTime Deadline { get; set; }
        public string Username { get; set; }
        [BsonElement("userId")]
        public string UserId { get; set; }
    }
}
