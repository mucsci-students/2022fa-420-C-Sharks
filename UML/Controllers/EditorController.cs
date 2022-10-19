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
            if (model.DiagramName != null)
            {
                List<DiagramModel> dgrams = collection.Find(x => x.Name == model.DiagramName).ToList();
                List<ScreenModel> ScnMdlHOLD = dgrams[0].screen.ToList();
                List<SingleRelationsModel> SingleRelations = dgrams[0].relations.ToList();
                var Jsmodel = new GoJsModel();
                Jsmodel.@class = "GraphLinksModel";
                Jsmodel.nodeDataArray = ScnMdlHOLD;
                Jsmodel.linkDataArray = SingleRelations;
                EditorViewModel viewModel = new EditorViewModel { userid = model._id, mySavedModel = Jsmodel.ToJson(), DiagramName = model.DiagramName };
                
                return View(viewModel);
            }
            else
            {
                return View(new EditorViewModel { userid = model._id, mySavedModel = "" });
            }
        }
                /*  string mySavedModel = "";
                if (model.DiagramName != null)
                {
                    List<DiagramModel> dgrams = collection.Find(x => x.Name == model.DiagramName).ToList();
                    List<ScreenModel> ScnMdlHOLD = dgrams[0].screen.ToList();
                    List<SingleRelationsModel> SingleRelations = dgrams[0].relations.ToList();

                    mySavedModel = "{ \"class\": \"GraphLinksModel\",\r\n \"nodeDataArray\": [ \r\n ";

                    foreach (var item in ScnMdlHOLD)
                    {
                        mySavedModel += "{\"text\":\"" + item.text + "\",\"loc\":\"" + item.Loc + "\",\"color\":\"" + item.color + "\"},";

                        if (item.fields != null)
                        {
                            if (item.fields.Length <= 1)
                            {
                                foreach (var field in item.fields)
                                {
                                    mySavedModel += "\"fields\":[{\" fieldType\":\"" + field.type + "\",\"fieldName\":\"" + field.name + "\"}],\"methodBinding\":[{";
                                }
                            }
                            else
                            {
                                var index = 0;
                                while (item.fields.Length > 1)
                                {
                                    mySavedModel += "\"fields\":[{\"fieldType\":\"" + item.fields[index].type + "\",\"fieldName\":\"" + item.fields[index].name + "\"},";
                                    index++;
                                }
                                mySavedModel += "\"fields\":[{\"fieldType\":\"" + item.fields[index].type + "\",\"fieldName\":\"" + item.fields[index].name + "\"}],\"methodBinding\":[";
                            }
                            if (item.methodBinding != null)
                            {
                                if (item.methodBinding.Count() <= 1)
                                {
                                    foreach (var method in item.methodBinding)
                                    {
                                        mySavedModel += "{\"methodName\":\"" + method.name + "\",\"return_type\":\"" + method.return_type + "\",\"methodParams\":[";
                                        if (method.@params.Count() <= 1)
                                        {
                                            foreach (var parm in method.@params)
                                            {
                                                mySavedModel += "{\"name\":\"" + parm.name + "\",\"type\":\"" + parm.type + "\"}]}],";
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

                return View(new EditorViewModel{ userid = model._id, mySavedModel = mySavedModel});
            }
            */
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
            //var field = json["fields"];
            //var methods = json["methodBinding"];
            //var @params = json["methodParams"];
            List<Parameters> @param = new List<Parameters>();

            List<ScreenModel> ScreModHLD = new List<ScreenModel> { };
            List<Fields> fields = new List<Fields>();
            List<Methods> methodBinding = new List<Methods>();
            if (text != null)
            {
                foreach (JObject item in text)
                {
                    var field = item["fields"];
                    if (field != null)
                    {
                        foreach (JObject item2 in field)
                        {
                            fields.Add(new Fields { fieldName = item2.GetValue("fieldName").ToString(), fieldType = item2.GetValue("fieldType").ToString() });
                        }
                    }
                    var methods = item["methodBinding"];
                    if(methods != null)
                    {
                        foreach (JObject item3 in methods)
                        {
                            var @params = item3["methodParams"];
                            if(@params != null)
                            {
                                foreach (JObject item4 in @params)
                                {
                                    @param.Add(new Parameters { name = item4.GetValue("name").ToString(), type = item4.GetValue("type").ToString() });
                                }
                            }                          
                            methodBinding.Add(new Methods { methodName = item3.GetValue("methodName").ToString(), return_type = item3.GetValue("return_type").ToString(), methodParams = param.ToArray() });
                        }
                        ScreModHLD.Add(new ScreenModel { text = "new node", Loc = item.GetValue("Loc").ToString(), color = item.GetValue("color").ToString(), key = item.GetValue("key").ToString(), fields = fields.ToArray(), methodBinding = methodBinding.ToArray(), className = item.GetValue("className").ToString(), visible = item.GetValue("visible").ToString()});
                    }      
                }
            }
            var toFrom = json["linkDataArray"];
			List <SingleRelationsModel> SingRelHLD = new List<SingleRelationsModel> { };
            if (toFrom != null)
            {
                foreach (JObject item in toFrom)
                {
                    SingRelHLD.Add(new SingleRelationsModel { to = item.GetValue("to").ToString(), from = item.GetValue("from").ToString(), toArrow = item.GetValue("toArrow").ToString() }); ;
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
        }
    }
}
