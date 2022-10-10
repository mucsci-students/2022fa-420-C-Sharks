using Microsoft.Extensions.Primitives;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace UML.Models
{
    public class ScreenModel
    {
        
        public string text { get; set; }
        public string Loc { get; set; }
        public string color { get; set; }
        [BsonId]
        public string key { get; set; }
        public Fields[] fields { get; set; }
        public Methods[] methodBinding { get; set; }
        public string visible { get; set; }
        public string className { get; set; }

    }
}
