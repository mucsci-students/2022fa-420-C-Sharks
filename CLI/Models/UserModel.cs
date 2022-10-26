using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CLI.Models
{
    public class UserModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }
        [BsonRequired]
        public string Username { get; set; }
        [BsonRequired]
        public string? Password { get; set; }
        public List<DiagramModel>? Diagrams { get; set; }
        public string? DiagramName { set; get; }
        public string? DiagramID { set; get; }
    }
}
