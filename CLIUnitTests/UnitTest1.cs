using Microsoft.VisualStudio.TestPlatform.ObjectModel.DataCollection;
using MongoDB.Bson;
using CLI.Models;
using CLI.Models.ViewModels;
namespace TestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void testLoginModel()
        {
            UserModel um = new UserModel() { Password = "1234", Username = "uname", _id = "1", DiagramName = "dname", DiagramID = "123" };
            LoginModel lm = new LoginModel() { status = 0, User = um};
            Assert.IsNotNull(lm.User);
            Assert.AreEqual(lm.status, 0);
        }

        [TestMethod]
        public void testEditorViewModel()
        {
            //View Model
            EditorViewModel evm = new EditorViewModel() { mySavedModel = "model" };
            Assert.AreEqual(evm.mySavedModel, "model");
        }
        [TestMethod]
        public void testDiagramModel()
        {
            ObjectId oid = ObjectId.GenerateNewId();
            ScreenModel[] sma = new ScreenModel[2];
            SingleRelationsModel[] srma = new SingleRelationsModel[3];
            //View Model
            DiagramModel dm = new DiagramModel() { id = oid, Username = "uname", screen = sma, relations = srma };
            Assert.AreNotEqual(dm.id.ToString, oid.ToString);
            Assert.AreEqual(dm.Username, "uname");
            Assert.AreEqual(dm.screen.Length, sma.Length);
            Assert.AreEqual(dm.relations.Length, srma.Length);
        }

        [TestMethod]
        public void testErrorViewModel()
        {
            //View Model
            ErrorViewModel ervm = new ErrorViewModel() { RequestId = "id" };
            Assert.AreEqual(ervm.RequestId, "id");
        }

        [TestMethod]
        public void testGoJsModel()
        {
            GoJsModel model = new GoJsModel() { Class = "class1", nodeDataArray = "nodedata", linkDataArray = "links" };
            Assert.AreEqual(model.Class, "class1");
            Assert.AreEqual(model.nodeDataArray, "nodedata");
            Assert.AreEqual(model.linkDataArray, "links");
        }

        [TestMethod]
        public void testRelationsModel()
        {
            SingleRelationsModel[] sra = new SingleRelationsModel[2];
            RelationsModel rm = new RelationsModel() { singleRelation = sra };
            Assert.AreEqual(rm.singleRelation.Length, sra.Length);
        }

        [TestMethod]
        public void testScreenModel()
        {
            ScreenModel sm = new ScreenModel() { Loc = "1"};
            Assert.AreEqual(sm.Loc, "1");
        }

        [TestMethod]
        public void testSingleRelationsModel()
        {
            SingleRelationsModel srm = new SingleRelationsModel() { source = "1", destination = "2", type = "t" };
            Assert.AreEqual(srm.source, "1");
            Assert.AreEqual(srm.destination, "2");
            Assert.AreEqual(srm.type, "t");
        }

        [TestMethod]
        public void testUserModel()
        {
            UserModel um = new UserModel() { Password = "1234", Username = "uname", _id = "1" };
            Assert.AreEqual(um.Password, "1234");
            Assert.AreEqual(um._id, "1");
            Assert.AreEqual(um.Username, "uname");
        }
    }
}