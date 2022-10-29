using Microsoft.CodeAnalysis.Differencing;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.DataCollection;
using MongoDB.Bson;
using CLI.Models;
using CLI.Models.ViewModels;

namespace unitTests
{
    [TestClass]
    public class CLIUnitTest
    {
        [TestMethod]
        public void testEditorViewModel()
        {
            //View Model
            EditorViewModel evm = new UML.Models.ViewModels.EditorViewModel() { userid = "1234", mySavedModel = "model", DiagramName = "name" };
            Assert.AreEqual(evm.userid, "1234");
            Assert.AreEqual(evm.mySavedModel, "model");
            Assert.AreEqual(evm.DiagramName, "name");
        }

        [TestMethod]
        public void testDiagramModel()
        {
            ObjectId oid = ObjectId.GenerateNewId();
            ScreenModel[] sma = new ScreenModel[2];
            SingleRelationsModel[] srma = new SingleRelationsModel[3];
            //View Model
            DiagramModel dm = new UML.Models.DiagramModel() { id = oid, Username = "uname", screen = sma, relations = srma };
            Assert.AreNotEqual(dm.id.ToString, oid.ToString);
            Assert.AreEqual(dm.Username, "uname");
            Assert.AreEqual(dm.screen.Length, sma.Length);
            Assert.AreEqual(dm.relations.Length, srma.Length);
        }

        [TestMethod]
        public void testErrorViewModel()
        {
            //View Model
            ErrorViewModel ervm = new UML.Models.ErrorViewModel() { RequestId = "id" };
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
            ScreenModel sm = new ScreenModel() { color = "red", key = "1", Loc = "1", text = "text" };
            Assert.AreEqual(sm.color, "red");
            Assert.AreEqual(sm.key, "1");
            Assert.AreEqual(sm.text, "text");
            Assert.AreEqual(sm.Loc, "1");
        }

        [TestMethod]
        public void testSingleRelationsModel()
        {
            SingleRelationsModel srm = new SingleRelationsModel() { from = "1", to = "2" };
            Assert.AreEqual(srm.from, "1");
            Assert.AreEqual(srm.to, "2");
        }

        [TestMethod]
        public void testUserModel()
        {
            UserModel um = new UserModel() { Password = "1234", username = "uname", _id = "1" };
            Assert.AreEqual(um.Password, "1234");
            Assert.AreEqual(um._id, "1");
            Assert.AreEqual(um.username, "uname");
        }
    }

}