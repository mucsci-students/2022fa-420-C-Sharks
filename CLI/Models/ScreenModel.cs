using Microsoft.Extensions.Primitives;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using UML.Models;

namespace CLI.Models
{
    public class ScreenModel
    {

        public string name { get; set; }
        public Fields[] fields { get; set; }
        public Methods[] methods { get; set; }
        public string Loc { get; set; }
        [BsonId]
        public string key { get; set; }
    }
}
