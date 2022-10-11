using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace CLI.Models
{
    public class DiagramModel
    {
        [BsonId]
        public ObjectId id { get; set; }
        public string Username { get; set; }
        public ScreenModel[] screen { get; set; }
        public SingleRelationsModel[] relations { get; set; }
    }
}