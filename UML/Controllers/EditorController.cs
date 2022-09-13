using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using UML.Models;

namespace UML.Controllers
{
	public class EditorController : Controller
	{
		public IActionResult Index(UserModel user)
		{
			// here we create a blank Diagram Model to be filled in on the Editor View
			var DiagramData =
				new DiagramModel
				{
					_id = "",
					Username = user.username,
					screen = new ScreenModel[] { new ScreenModel { name = "", type = 0, Loc = "", Attributes = { },
						Relations = new SingleRelationModel[] { new SingleRelationModel { type = 0, to = "", from = "" } } } }
				};

			ViewBag.DiagramData = DiagramData;
			return View(ViewBag);
		}

		[HttpPost]
        public IActionResult Save()
        {
			string connectionString = "mongodb+srv://CShark:5wulj7CrF1FTBpwi@umldb.7hgm9e0.mongodb.net/?retryWrites=true&w=majority";
			string databaseName = "uml_db";
			string collectionName = "diagrams";

			var client = new MongoClient(connectionString);
			var db = client.GetDatabase(databaseName);
			var collection = db.GetCollection<DiagramModel>(collectionName);

			

			return View();

		}
		// Post location?
	}
}
