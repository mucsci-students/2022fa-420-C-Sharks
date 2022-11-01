using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CLI.Models.ViewModels
{
    public class LoginViewModel
    {
        [BsonRequired]
        public string Username { get; set; }
        [BsonRequired]
        public string Password { get; set; }
    }
}
