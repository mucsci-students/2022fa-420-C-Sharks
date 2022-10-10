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
using NuGet.ProjectModel;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace UML.Controllers
{
	public class EditorController : Controller
	{
        [HttpGet]
        public ActionResult Index(UserModel? model)
		{
            // Open database connection and look for the user given.
            string connectionString = "mongodb+srv://CShark:5wulj7CrF1FTBpwi@umldb.7hgm9e0.mongodb.net/?retryWrites=true&w=majority";
            string databaseName = "uml_db";
            string collectionName = "diagrams";
            var client = new MongoClient(connectionString);
            var db = client.GetDatabase(databaseName);
            var collection = db.GetCollection<DiagramModel>(collectionName);
            string mySavedModel = "";
            if (model.DiagramName != null)
            {
                List<DiagramModel> dgrams = collection.Find(x => x.Name == model.DiagramName).ToList();
                List<ScreenModel> ScnMdlHOLD = dgrams[0].screen.ToList();
                List<SingleRelationsModel> SingleRelations = dgrams[0].relations.ToList();

                mySavedModel = "{ \"class\": \"GraphLinksModel\",\n\"nodeDataArray\": [ \n{\"text\":\"";
                foreach (var item in ScnMdlHOLD)
                {
                    mySavedModel += item.text + "\",\"loc\":\"" + item.Loc + "\",\"color\":\"" + item.color + "\",\"fields\":[{";

                    if (item.fields != null)
                    {
                        if (item.fields.Count() <= 1)
                        {
                            foreach (var field in item.fields)
                            {
                                mySavedModel += "\" fieldType\":\"" + field.type + "\",\"fieldName\":\"" + field.name + "\"}],\"methodBinding\":[{";
                            }
                        }
                        else
                        {
                            var index = 0;
                            while (item.fields.Count() > 1)
                            {
                                mySavedModel += "\"fieldType\":\"" + item.fields[index].type + "\",\"fieldName\":\"" + item.fields[index].name + "\"},";
                                index++;
                            }
                            mySavedModel += "\"fieldType\":\"" + item.fields[index].type + "\",\"fieldName\":\"" + item.fields[index].name + "\"}],\"methodBinding\":[{";
                        }
                        if (item.methodBinding != null)
                        {
                            if (item.methodBinding.Count() <= 1)
                            {
                                foreach (var method in item.methodBinding)
                                {
                                    mySavedModel += "\"methodName\":\"" + method.name + "\",\"return_type\":\"" + method.return_type + "\",\"methodParams\":[{";
                                    if (method.@params.Count() <= 1)
                                    {
                                        foreach (var parm in method.@params)
                                        {
                                            mySavedModel += "\"name\":\"" + parm.name + "\",\"type\":\"" + parm.type + "\"}]}],";
                                        }
                                    }
                                    else
                                    {
                                        var index2 = 0;
                                        while (method.@params.Count() > 1)
                                        {
                                            mySavedModel += "\"name\":\"" + method.@params[index2].name + "\",\"type\":\"" + method.@params[index2].type + "\"},";
                                            index2++;
                                        }
                                        mySavedModel += "\"name\":\"" + method.@params[index2].name + "\",\"type\":\"" + method.@params[index2].type + "\"}]}],";
                                    }

                                }
                            }
                            else
                            {
                                var index3 = 0;
                                while (item.methodBinding.Count() > 1)
                                {
                                    mySavedModel += "\"methodName\":\"" + item.methodBinding[index3].name + "\",\"return_type\":\"" + item.methodBinding[index3].return_type + "\",\"methodParams\":[{";
                                    if (item.methodBinding[index3].@params.Count() <= 1)
                                    {
                                        foreach (var parm in item.methodBinding[index3].@params)
                                        {
                                            mySavedModel += "\"name\":\"" + parm.name + "\",\"type\":\"" + parm.type + "\"}]}],";
                                        }
                                    }
                                    else
                                    {
                                        var index2 = 0;
                                        while (item.methodBinding[index3].@params.Count() > 1)
                                        {
                                            mySavedModel += "\"name\":\"" + item.methodBinding[index3].@params[index2].name + "\",\"type\":\"" + item.methodBinding[index3].@params[index2].type + "\"},";
                                            index2++;
                                        }
                                        mySavedModel += "\"name\":\"" + item.methodBinding[index3].@params[index2].name + "\",\"type\":\"" + item.methodBinding[index3].@params[index2].type + "\"}]}],";
                                    }

                                }
                            }
                            mySavedModel += "\"className\":\"" + item.className + "\",\"key:" + item.key + "}],";
                        }
                        
                    }
                }
                    
                mySavedModel += "\"linkDataArray\": [{";
                var index4 = 0;
                if (SingleRelations.Count <= 1)
                {
                    foreach (var item in SingleRelations)
                    {
                        mySavedModel += "\"from\":" + item.from + ",\"to\":" + item.to + ",\"toArrow\":\"" + item.toArrow + "\"}]}";
                    }

                    while (SingleRelations.Count > 1)
                    {
                        mySavedModel += "\"from\":" + SingleRelations[index4].from + ",\"to\":" + SingleRelations[index4].to + ",\"toArrow\":\"" + SingleRelations[index4].toArrow + "\"},";
                        index4++;
                    }
                }
                else
                {
                    mySavedModel += "\"from\":" + SingleRelations[index4].from + ",\"to\":" + SingleRelations[index4].to + ",\"toArrow\":\"" + SingleRelations[index4].toArrow + "\"}]}";
                }
            }
            else
            {
                return View(new EditorViewModel { userid = model._id, mySavedModel = "" });
            }

            


            
            
            
            /*string mySavedModel = "{ \"class\": \"GraphLinksModel\",\n\"nodeDataArray\": [ \n{";
            foreach(var item in dgrams)
            {
                foreach(var node in dgrams[0].screen)
                {
                    mySavedModel += "\"text\":"+node.text+"\",";
                    mySavedModel += "\"loc\":" + node.Loc + "\",";
                    mySavedModel += "\"color\":" + node.color + "\",";
                    //foreach(var )
                }
            }



            /*
            string mySavedModel = Newtonsoft.Json.JsonConvert.SerializeObject(dgrams, Formatting.Indented);
            mySavedModel = mySavedModel.Replace("\"Name\": \"" + dgrams[0].Name + "\",", null);
            mySavedModel = mySavedModel.Replace("\"UserID\": \""+ dgrams[0].UserID + "\",", null);
            mySavedModel = mySavedModel.Replace("\"id\": \"" + dgrams[0].id + "\",", null);
            mySavedModel = mySavedModel.Replace("screen", "nodeDataArray");
            mySavedModel = mySavedModel.Replace("relations", "linkDataArray");
            */

            return View(new EditorViewModel{ userid = model._id, mySavedModel = mySavedModel});
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
            string connectionString = "mongodb+srv://CShark:5wulj7CrF1FTBpwi@umldb.7hgm9e0.mongodb.net/?retryWrites=true&w=majority";
            string databaseName = "uml_db";
            string collectionName = "diagrams";
            var client = new MongoClient(connectionString);
            var db = client.GetDatabase(databaseName);
            var collection = db.GetCollection<DiagramModel>(collectionName);
			var json = JObject.Parse(model.mySavedModel);
            var text = json["nodeDataArray"];
            var field = text["fields"];
            var methods = text["methodBinding"];
            var @params = methods["methodParams"];
            List<Fields> @param = new List<Fields>();

            List<ScreenModel> ScreModHLD = new List<ScreenModel> { };
            List<Fields> fields = new List<Fields>();
            List<Methods> methodBinding = new List<Methods>();
            if (text != null)
            {
                foreach (JObject item in text)
                {
                    
                    foreach (JObject item2 in field)
                    {
                        fields.Add(new Fields { name = item2.GetValue("fieldName").ToString(), type = item2.GetValue("fieldType").ToString() });                       
                    }
                        foreach (JObject item3 in methods)
                        {
                            foreach (JObject item4 in @params)
                            {
                                @param.Add(new Fields { name = item4.GetValue("name").ToString(), type = item4.GetValue("type").ToString() });
                            }
                            methodBinding.Add(new Methods { name = item3.GetValue("methodName").ToString(), return_type = item3.GetValue("return_type").ToString(), @params = param.ToArray() });
                        }
                        ScreModHLD.Add(new ScreenModel { text = item.GetValue("className").ToString(), Loc = item.GetValue("loc").ToString(), color = item.GetValue("color").ToString(), key = item.GetValue("key").ToString(), fields = fields.ToArray(), methodBinding = methodBinding.ToArray() });
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
                    SingRelHLD.Add(new SingleRelationsModel { to = item.GetValue("to").ToString(), from = item.GetValue("from").ToString(),  });
                }
            }
            else
            {
                // if toFrom is null
                return 2;
            }
            
			var Diagram = new DiagramModel { UserID = model.userid, Name = model.DiagramName, screen = ScreModHLD.ToArray(), relations = SingRelHLD.ToArray() };
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
