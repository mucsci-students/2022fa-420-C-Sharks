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
using Newtonsoft.Json;
using System.Linq;
using NuGet.ProjectModel;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace UML.Controllers
{
	public class EditorController : Controller
	{
        [HttpGet]
        public ActionResult Index(UserModel? model)
        {
            // Open database connection and look for the DiagramID given.
            string connectionString = "mongodb+srv://CShark:5wulj7CrF1FTBpwi@umldb.7hgm9e0.mongodb.net/?retryWrites=true&w=majority";
            string databaseName = "uml_db";
            string collectionName = "diagrams";
            var client = new MongoClient(connectionString);
            var db = client.GetDatabase(databaseName);
            var collection = db.GetCollection<DiagramModel>(collectionName);
            // If we're given a preexisting diagram to find
            if (model.DiagramID != null)
            {
                // Gets diagram from DiagramID
                List<DiagramModel> dgrams = collection.Find(x => x.id == model.DiagramID).ToList();
                List<ScreenModel> ScnMdlHOLD = dgrams[0].screen.ToList();
                List<SingleRelationsModel> SingleRelations = dgrams[0].relations.ToList();
                
                // Sets up model to be passed to view
                var Jsmodel = new GoJsModel();
                Jsmodel.@class = "GraphLinksModel";
                Jsmodel.nodeDataArray = ScnMdlHOLD;
                Jsmodel.linkDataArray = SingleRelations;
                var options = new JsonSerializerOptions { WriteIndented = true };
                string jsonString = JsonSerializer.Serialize(Jsmodel, options);
                EditorViewModel viewModel = new EditorViewModel { userid = model._id, mySavedModel = jsonString, DiagramName = model.DiagramName };
                return View(viewModel);
            }
            // Else we're making a new diagram
            else
            {
                return View(new EditorViewModel { userid = model._id, mySavedModel = "" });
            }
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


        // changed ConvertNsave to take in a view model
        // and added some null checks
		public static int ConvertNsave(EditorViewModel model)
		{ 
            // connect to database and get collection of DiagramModels
            string connectionString = "mongodb+srv://CShark:5wulj7CrF1FTBpwi@umldb.7hgm9e0.mongodb.net/?retryWrites=true&w=majority";
            string databaseName = "uml_db";
            string collectionName = "diagrams";
            var client = new MongoClient(connectionString);
            var db = client.GetDatabase(databaseName);
            var collection = db.GetCollection<DiagramModel>(collectionName);
			var json = JObject.Parse(model.mySavedModel);
            var text = json["nodeDataArray"];
            //var field = json["fields"];
            //var methods = json["methodBinding"];
            //var @params = json["methodParams"];
            List<Parameters> @param = new List<Parameters>();

            List<ScreenModel> ScreModHLD = new List<ScreenModel> { };
            List<Fields> fields = new List<Fields>();
            List<Methods> methodBinding = new List<Methods>();
            if (text != null)
            {
                //int screModCount = 0;
                // Looping through nodes
                foreach (JObject item in text)
                {
                    var field = item["fields"];
                    if (field != null)
                    {   
                        // clear previous nodes fields.
                        fields.Clear();
                        //int fieldCount = 0;
                        // Looping through fields in node
                        foreach (JObject item2 in field)
                        {
                            // Fields model was added/changed to make constructing goJSModel easier.
                            fields.Add(new Fields { fieldName = item2.GetValue("fieldName").ToString(), fieldType = item2.GetValue("fieldType").ToString() });
                            //fieldCount++;
                        }
                    }
                    var methods = item["methodBinding"];
                    if(methods != null)
                    {
<<<<<<< HEAD
                        // clear previous method
=======
>>>>>>> 050f753e59c252872a1ba16d77be699a48406474
                        methodBinding.Clear();
                        //int methodCount = 0;
                        // Looping through methods in node
                        foreach (JObject item3 in methods)
                        {
                            var @params = item3["methodParams"];
                            if(@params != null)
                            {
<<<<<<< HEAD
                                // clear previous method's params
=======
>>>>>>> 050f753e59c252872a1ba16d77be699a48406474
                                @param.Clear();
                                //int paramsCount = 0;
                                // Looping through params in methods in node
                                foreach (JObject item4 in @params)
                                {
                                    // Parameters added/changed to make constructing goJSModel easier
                                    @param.Add(new Parameters { name = item4.GetValue("name").ToString(), type = item4.GetValue("type").ToString() });
                                    //paramsCount++;
                                }
                            }
                            // Methods model was added/changed to make constructing goJSModel easier
                            methodBinding.Add(new Methods { methodName = item3.GetValue("methodName").ToString(), return_type = item3.GetValue("return_type").ToString(), methodParams = param.ToArray() });
                            //methodCount++;
                        }
                        ScreModHLD.Add(new ScreenModel { text = "new node", loc = item.GetValue("loc").ToString(), color = item.GetValue("color").ToString(), key = item.GetValue("key").ToString(), fields = fields.ToArray(), methodBinding = methodBinding.ToArray(), className = item.GetValue("className").ToString(), visible = item.GetValue("visible").ToString()});
                        //screModCount++;
                    }      
                }
            }
            var toFrom = json["linkDataArray"];
			List <SingleRelationsModel> SingRelHLD = new List<SingleRelationsModel> { };
            if (toFrom != null)
            {
                foreach (JObject item in toFrom)
                {
<<<<<<< HEAD
                    if(item.GetValue("fill") != null)
                    // fill and toArrow added/changed to be able to handle drawing the arrows in js screen
=======
>>>>>>> 050f753e59c252872a1ba16d77be699a48406474
                    SingRelHLD.Add(new SingleRelationsModel { to = item.GetValue("to").ToString(), from = item.GetValue("from").ToString(), toArrow = item.GetValue("toArrow").ToString(), fill = item.GetValue("fill").ToString() }); ;
                }
            }
            else
            {
                // if toFrom is null
                return 2;
            }
            
			var Diagram = new DiagramModel { UserID = model.userid, Name = model.DiagramName, screen = ScreModHLD.ToArray(), relations = SingRelHLD.ToArray() };
            
            collection.InsertOne(Diagram);
            // if Diagram added
            return 0;
        }
    }
}
