using Microsoft.Extensions.Primitives;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace UML.Models
{
    public class ScreenModel
    {
        [BsonId]
        public string name { get; set; }
        public ShapeType type { get; set; }
        public string Loc { get; set; }
        public string[] Attributes { get; set; }
        public SingleRelationModel[] Relations { get; set; }

    }
}
