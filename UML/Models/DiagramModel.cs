using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace UML.Models
{
    public class DiagramModel
    {
        [BsonId]
        public string serial { get; set; }
        public string username { get; set; }
        public ScreenModel[] screen { get; set; }
    }
}
