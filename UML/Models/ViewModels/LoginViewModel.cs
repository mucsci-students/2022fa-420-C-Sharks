using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace UML.Models.ViewModels
{
    public class LoginViewModel
    {
        [BsonRequired]
        public string Username { get; set; }
        [BsonRequired]
        public string Password { get; set; }
    }
}