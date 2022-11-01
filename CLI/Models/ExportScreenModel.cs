using Microsoft.Extensions.Primitives;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CLI.Models
{
    public class ExportScreenModel
    {

        public string name { get; set; }
        public Fields[] fields { get; set; }
        public Methods[] methods { get; set; }
    }
}