using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace UML.Models
{
    public class RelationsModel
    {
        [BsonId]
        public string serial { get; set; }
        public SingleRelationModel[] singleRelation { get; set; }
    }
}
