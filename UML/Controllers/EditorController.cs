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

		public ActionResult Index()
		{
			
			return View(new EditorViewModel());
		}

		[HttpPost]
		public ActionResult Index(EditorViewModel model)
        {

            if (model == null)
			{
				return View();
			}

			//model.mySavedModel = model.mySavedModel.Replace('\"',' ');

            


			/*
			char slash = '\\';
			while (index <= rawData.Length)
			{
				if (!rawData[index].Equals(slash))
				{
					rawData = rawData.		
				}
				index++;
			}
			*/
			ConvertNsave(model.mySavedModel);

            
			Console.WriteLine("HERE");

			/*var RelationsData = new RelationsModel { };

            foreach (var item in returnedData.nodeDataArray)
			{
				var index = 0;
				DiagramData.screen[index] = item;
			}
			foreach (var item in returnedData.linkDataArray)
			{
				var index = 0;
				RelationsData.singleRelation[index] = item;
			}
			*/
			return View();

        }
		// Post location?
		public static void ConvertNsave(string input)
		{
            string connectionString = "mongodb+srv://CShark:5wulj7CrF1FTBpwi@umldb.7hgm9e0.mongodb.net/?retryWrites=true&w=majority";
            string databaseName = "uml_db";
            string collectionName = "diagrams";
            var client = new MongoClient(connectionString);
            var db = client.GetDatabase(databaseName);
            var collection = db.GetCollection<DiagramModel>(collectionName);
			var json = JObject.Parse(input);
			var text = json["nodeDataArray"];
            List<ScreenModel> ScreModHLD = new List<ScreenModel> { };
            foreach (JObject item in text)
			{
				ScreModHLD.Add(new ScreenModel { text = item.GetValue("text").ToString(), Loc = item.GetValue("loc").ToString(), color = item.GetValue("color").ToString(), key = item.GetValue("key").ToString() }); 
			}
            var toFrom = json["linkDataArray"];
			List <SingleRelationsModel> SingRelHLD = new List<SingleRelationsModel> { };
			foreach (JObject item in toFrom)
			{
				SingRelHLD.Add(new SingleRelationsModel { to = item.GetValue("to").ToString(), from = item.GetValue("from").ToString() });
			}
			var Diagram = new DiagramModel { Username = "Test", screen = ScreModHLD.ToArray(),relations = SingRelHLD.ToArray()};
            //don't care + didn't ask + ratio + you fell off + cope + seethe + mald + dilate + L + hoes mad + W + cry about it + stay mad + touch grass + pound sand + skill issue + quote tweet + get real + no bitches?
            collection.InsertOne(Diagram);
            //await collection2.InsertOneAsync(relations);
            //string test = colorHLD.ElementAt(2);
            //string test2 = ToHLD.ElementAt(0);
            //Console.WriteLine("HERE");
            //Console.WriteLine("HERE");
        }
    }
}
