using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using Newtonsoft.Json.Linq;
using NuGet.Protocol;
using System;
using System.Net;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using UML.Models;
using UML.Models.ViewModels;

namespace UML.Controllers
{
	public class EditorController : Controller
	{
        [HttpGet]
		public ActionResult Index(UserModel model)
		{
            string connectionString = "mongodb+srv://CShark:5wulj7CrF1FTBpwi@umldb.7hgm9e0.mongodb.net/?retryWrites=true&w=majority";
            string databaseName = "uml_db";
            string collectionName = "diagrams";
            var client = new MongoClient(connectionString);
            var db = client.GetDatabase(databaseName);
            var collection = db.GetCollection<DiagramModel>(collectionName);
            var results = collection.Find(x => x.Username == model._id).ToList();
            ViewBag.id = model._id;
            return View(new EditorViewModel());
		}

		[HttpPost]
		public ActionResult Index(EditorViewModel model)
        {

            if (model == null)
			{
				return View();
			}

			int status = ConvertNsave(model);
            if (status > 0)
            {
                // maybe not give the model back?
                if (status == 1)
                {
                    TempData["Message"] = "Error parsing object list please try again";
                    return View(model);
                }
                if (status == 2)
                {
                    TempData["Message"] = "Error parsing link relations please try again";
                    return View(model);
                }
            }
			return View(model);

        }

		public static int ConvertNsave(EditorViewModel model)
		{
            string connectionString = "mongodb+srv://CShark:5wulj7CrF1FTBpwi@umldb.7hgm9e0.mongodb.net/?retryWrites=true&w=majority";
            string databaseName = "uml_db";
            string collectionName = "diagrams";
            var client = new MongoClient(connectionString);
            var db = client.GetDatabase(databaseName);
            var collection = db.GetCollection<DiagramModel>(collectionName);
			var json = JObject.Parse(model.mySavedModel);
			var text = json["nodeDataArray"];
            List<ScreenModel> ScreModHLD = new List<ScreenModel> { };
            if (text != null)
            {
                foreach (JObject item in text)
                {
                    ScreModHLD.Add(new ScreenModel { text = item.GetValue("text").ToString(), Loc = item.GetValue("loc").ToString(), color = item.GetValue("color").ToString(), key = item.GetValue("key").ToString() });
                }
            }
            else
            {
                // if text is null
                return 1;
            }
            var toFrom = json["linkDataArray"];
			List <SingleRelationsModel> SingRelHLD = new List<SingleRelationsModel> { };
            if (toFrom != null)
            {
                foreach (JObject item in toFrom)
                {
                    SingRelHLD.Add(new SingleRelationsModel { to = item.GetValue("to").ToString(), from = item.GetValue("from").ToString() });
                }
            }
            else
            {
                // if toFrom is null
                return 2;
            }
            // TODO add name for diagram
			var Diagram = new DiagramModel { Username = model.userid, screen = ScreModHLD.ToArray(),relations = SingRelHLD.ToArray()};
            //don't care + didn't ask + ratio + you fell off + cope + seethe + mald + dilate + L + hoes mad + W + cry about it + stay mad + touch grass + pound sand + skill issue + quote tweet + get real + no bitches?
            collection.InsertOne(Diagram);
            // if Diagram added
            return 0;
            //await collection2.InsertOneAsync(relations);
            //string test = colorHLD.ElementAt(2);
            //string test2 = ToHLD.ElementAt(0);
            //Console.WriteLine("HERE");
            //Console.WriteLine("HERE");
        }
    }
}
