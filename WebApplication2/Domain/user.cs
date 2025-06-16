using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace WebApplication2.Domain
{
    public class User
    {
        [BsonId]
        [BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("username"), BsonRepresentation(BsonType.String)]
        public string Username { get; set; }

        [BsonElement("password"), BsonRepresentation(BsonType.String)]
        public string PasswordHash { get; set; }
    }
}
