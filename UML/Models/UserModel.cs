using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace UML.Models
{
    public class UserModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }
        public string username { get; set; }
        public string Password { get; set; }

    }
}
