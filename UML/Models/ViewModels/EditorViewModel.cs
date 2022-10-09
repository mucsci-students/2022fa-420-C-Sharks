using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace UML.Models.ViewModels
{
    public class EditorViewModel
    {
        [BsonRequired]
        public string userid { get; set; }
        [BsonRequired]
        public string mySavedModel { set; get; }
    }
}