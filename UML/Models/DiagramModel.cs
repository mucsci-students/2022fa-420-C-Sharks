using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace UML.Models
{
    public class DiagramModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }
        public string UserID { get; set; }
        public string Name { get; set; }
        public ScreenModel[] screen { get; set; }
        public SingleRelationsModel[] relations { get; set; }
    }
}