using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using Newtonsoft.Json.Linq;
using NuGet.Protocol;
using System.Net;
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

            JObject json = JObject.Parse(model.mySavedModel);
			var bson = json.ToBson();

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


            string connectionString = "mongodb+srv://CShark:5wulj7CrF1FTBpwi@umldb.7hgm9e0.mongodb.net/?retryWrites=true&w=majority";
			string databaseName = "uml_db";
			string collectionName = "diagrams";

			var client = new MongoClient(connectionString);
			var db = client.GetDatabase(databaseName);
			var collection = db.GetCollection<DiagramModel>(collectionName);

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
	}
}
