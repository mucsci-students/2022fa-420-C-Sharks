using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace UML.Models
{
    public class ScreenModel
    {
        [BsonId]
        public string name { get; set; }
        public ShapeType type { get; set; }
        public string xy { get; set; }
    }
}
