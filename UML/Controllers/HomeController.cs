﻿
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.View;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using UML.Models;
using UML.Models.ViewModels;

namespace UML.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View(new LoginViewModel());
        }
        [HttpPost]
        public IActionResult Index(LoginViewModel model) 
        {
            if (!ModelState.IsValid) 
            {
                TempData["Message"] = "Model invalid. Please try again.";
                return View();
            }
            string connectionString = "mongodb+srv://CShark:5wulj7CrF1FTBpwi@umldb.7hgm9e0.mongodb.net/?retryWrites=true&w=majority";
            string databaseName = "uml_db";
            string collectionName = "users";
            var client = new MongoClient(connectionString);
            var db = client.GetDatabase(databaseName);
            var collection = db.GetCollection<UserModel>(collectionName);
            // Look through DB and give me a list of usernames that match what the user provided.
            List<UserModel> userList = collection.Find(x => x.Username == model.Username).ToList();
            //var userID = userList[0]._id;
            // Convert that list to json
            //var json = collection.Find(x => x.Username == model.Username).ToJson();
            var index = userList.Count;
            while (index > 0)
            {
                if (userList[index-1].Password == model.Password)
                {
                    return RedirectToAction("ListDiagrams", userList[index-1]);
                }
                index--;

            }
            if ((userList.Count > 0) && (userList[0].Password == model.Password))
            {

                // Grab the Unique id for the user to pass
                var userID = userList[0]._id;
                var user = new UserModel { _id = userID, Username = userList[0].Username, Password = userList[0].Password };
                ViewData["UserModel"] = user;
                //var client2 = new MongoClient(connectionString);
                //var db2 = client2.GetDatabase(databaseName);
                //var collection2 = db2.GetCollection<DiagramModel>("diagrams");
                //var results2 = collection2.Find(x => x.Username == userID).ToList();
                //ViewData["list"] = results2;
                return RedirectToAction("ListDiagrams", user);
            }
            if ((userList.Count > 0) && !(userList[0].Password == model.Password))
            {
                TempData["Message"] = "Incorrect password";
                return View();
            }
            else 
            {
                TempData["Message"] = "User not found.";
                return View();
            }
            
        }
        [HttpGet]
        public ActionResult ListDiagrams(UserModel model)
        {
            if(!ModelState.IsValid)
            {
                return RedirectToAction("signup");
            }

            string connectionString = "mongodb+srv://CShark:5wulj7CrF1FTBpwi@umldb.7hgm9e0.mongodb.net/?retryWrites=true&w=majority";
            var databaseName = "diagrams";
            var client = new MongoClient(connectionString);
            var db = client.GetDatabase(databaseName);
            var collection = db.GetCollection<DiagramModel>("diagrams");
            var results = collection.Find(x => x.Username == model._id).ToList();
            ViewBag.list = results;
            ViewBag.id = model._id;
            ViewBag.username = model.Username;
            ViewBag.password = model.Password;
            return View();
        }



        [HttpGet]
        public ActionResult Signup()
        {
            return View(new UserModel());
        }
        [HttpPost]
        public ActionResult Signup(UserModel model)
        {
            string connectionString = "mongodb+srv://CShark:5wulj7CrF1FTBpwi@umldb.7hgm9e0.mongodb.net/?retryWrites=true&w=majority";
            string databaseName = "uml_db";
            string collectionName = "users";
            var client = new MongoClient(connectionString);
            var db = client.GetDatabase(databaseName);
            var collection = db.GetCollection<UserModel>(collectionName);
            var results = collection.Find(x => x.Username == model.Username).ToList();
            if (results.Count == 0)
            {
                collection.InsertOne(model);
                //TODO update collection and give to index
                return RedirectToAction("Index");
            }
            else
            {
                TempData["Message"] = "Username Already Taken";
                return View(model);
            }

        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}