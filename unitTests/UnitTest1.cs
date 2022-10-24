using Microsoft.CodeAnalysis.Differencing;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.DataCollection;
using MongoDB.Bson;
using MongoDB.Driver;
using UML.Models;
using UML.Models.ViewModels;

namespace unitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void testEditorViewModel()
        {
            //View Model
            EditorViewModel evm = new UML.Models.ViewModels.EditorViewModel(){userid = "1234", mySavedModel = "model", DiagramName = "name"};
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
            DiagramModel dm = new UML.Models.DiagramModel() { id = oid.ToString(), UserID = "1234", Name = "uname", screen = sma, relations = srma };
            Assert.AreNotEqual(dm.id.ToString, oid.ToString);
            Assert.AreEqual(dm.Name, "uname");
            Assert.AreEqual(dm.screen.Length, sma.Length);
            Assert.AreEqual(dm.relations.Length, srma.Length);
            Assert.AreEqual(dm.UserID, "1234");
        }

        [TestMethod]
        public void testErrorViewModel()
        {
            //View Model
            ErrorViewModel ervm = new UML.Models.ErrorViewModel() {RequestId = "id"};
            Assert.AreEqual(ervm.RequestId, "id");
        }

        [TestMethod]
        public void testLoginViewModel()
        {
            //View Model
            LoginViewModel lvm = new UML.Models.ViewModels.LoginViewModel() {Username = "uname", Password = "pwd"};
            Assert.AreEqual(lvm.Username, "uname");
            Assert.AreEqual(lvm.Password, "pwd");
        }
        [TestMethod]
        public void testGoJsModel()
        {
            GoJsModel model = new GoJsModel() { @class = "class1", nodeDataArray = new List<<UML.Models.ScreenModel>(3), linkDataArray = new List<UML.Models.SingleRelationsModel>(4)};
            Assert.AreEqual(model.@class, "class1");
            Assert.AreEqual(model.nodeDataArray.Length(), 3);
            Assert.AreEqual(model.linkDataArray.Length(), 4);
        }

        [TestMethod]
        public void testRelationsModel()
        {
            SingleRelationsModel[] sra = new SingleRelationsModel[2];
            RelationsModel rm = new RelationsModel() {singleRelation = sra };
            Assert.AreEqual(rm.singleRelation.Length, sra.Length);
        }

        [TestMethod]
        public void testScrrenModel()
        {
            ScreenModel sm = new ScreenModel() { color = "red", key = "1", loc = "1", text = "text", fields = new Fields[4], methodBinding = Methods[5], className = "name", visible = "true"};
            Assert.AreEqual(sm.color, "red");
            Assert.AreEqual(sm.key, "1");
            Assert.AreEqual(sm.text, "text");
            Assert.AreEqual(sm.loc, "1");
            Assert.AreEqual(sm.fields.Length, 4);
            Assert.AreEqual(sm.methodBinding.Length, 5);
            Assert.AreEqual(sm.className, "name");
            Assert.AreEqual(sm.visible, "true");
        }

        [TestMethod]
        public void testSingleRelationsModel()
        {
            SingleRelationsModel srm = new SingleRelationsModel() { from = "1", to = "2"};
            Assert.AreEqual(srm.from, "1");
            Assert.AreEqual(srm.to, "2");
        }

        [TestMethod]
        public void testUserModel()
        {
            UserModel um = new UserModel() { Password = "1234", username = "uname", _id = "1"};
            Assert.AreEqual(um.Password, "1234");
            Assert.AreEqual(um._id, "1");
            Assert.AreEqual(um.username, "uname");
        }
    }

}