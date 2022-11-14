using System.Collections.Generic;
using FakeItEasy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Differencing;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.DataCollection;
using MongoDB.Bson;
using MongoDB.Driver;
using UML.Controllers;
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
            DiagramModel dm = new UML.Models.DiagramModel() { id = oid.ToString(), UserID = "1234", Name = "uname", screen = sma, relations = srma };
            Assert.AreEqual(dm.id, oid.ToString());
            Assert.AreEqual(dm.Name, "uname");
            Assert.AreEqual(dm.screen.Length, sma.Length);
            Assert.AreEqual(dm.relations.Length, srma.Length);
            Assert.AreEqual(dm.UserID, "1234");
        }

        [TestMethod]
        public void testErrorViewModel()
        {
            //View Model
            ErrorViewModel ervm = new ErrorViewModel() { RequestId = "id"};

            Assert.AreEqual(ervm.RequestId, "id");
            Assert.AreNotEqual(ervm.ShowRequestId, null);
        }

        [TestMethod]
        public void testLoginViewModel()
        {
            //View Model
            LoginViewModel lvm = new UML.Models.ViewModels.LoginViewModel() { Username = "uname", Password = "pwd" };
            Assert.AreEqual(lvm.Username, "uname");
            Assert.AreEqual(lvm.Password, "pwd");
        }
        [TestMethod]
        public void testGoJsModel()
        {
            GoJsModel model = new GoJsModel() { @class = "class1", nodeDataArray = new List<UML.Models.ScreenModel> (), linkDataArray = new List<UML.Models.SingleRelationsModel>() };
            Assert.AreEqual(model.@class, "class1");
            Assert.AreEqual(model.nodeDataArray.Count, 0);
            Assert.AreEqual(model.linkDataArray.Count, 0);
        }

        [TestMethod]
        public void testRelationsModel()
        {
            SingleRelationsModel[] sra = new SingleRelationsModel[2];
            RelationsModel rm = new RelationsModel() { singleRelation = sra };
            Assert.AreEqual(rm.singleRelation.Length, sra.Length);
        }

        [TestMethod]
        public void testScrrenModel()
        {
            ScreenModel sm = new ScreenModel() { color = "red", key = "1", loc = "1", text = "text", fields = new Fields[4], methodBinding = new Methods[5], className = "name", visible = "true" };
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
            SingleRelationsModel srm = new SingleRelationsModel() { from = "1", to = "2", fromName = "Class1", toName = "Class2", toArrow = "Inheritance" };
            Assert.AreEqual(srm.from, "1");
            Assert.AreEqual(srm.to, "2");
            Assert.AreEqual(srm.fromName, "Class1");
            Assert.AreEqual(srm.toName, "Class2");
            Assert.AreEqual(srm.toArrow, "Inheritance");
        }

        [TestMethod]
        public void testUserModel()
        {
            List<DiagramModel> dlm = new List<DiagramModel>();
            UserModel um = new UserModel() { Password = "1234", Username = "uname", _id = "1", Diagrams = dlm, DiagramName = "dname", DiagramID ="123" };

            Assert.AreEqual(um.Password, "1234");
            Assert.AreEqual(um._id, "1");
            Assert.AreEqual(um.Username, "uname");
            Assert.AreEqual(um.Diagrams, dlm);
            Assert.AreEqual(um.DiagramName, "dname");
            Assert.AreEqual("123", um.DiagramID);
        }
        [TestMethod]
        public void testFields()
        {
            Fields fs = new Fields() { fieldName = "fname", fieldType = "ftype" };
            Assert.AreEqual(fs.fieldName, "fname");
            Assert.AreEqual(fs.fieldType, "ftype");
        }

        [TestMethod]
        public void testMethods()
        {
            Methods md = new Methods() { methodName = "mname", methodParams = new Parameters[3], return_type = "String" };
            Assert.AreEqual(md.return_type, "String");
            Assert.AreEqual(md.methodName, "mname");
            Assert.AreEqual(md.methodParams.Length,3);
        }

        [TestMethod]
        public void testParameters()
        {
            Parameters pm = new Parameters() { name = "pname", type = "String" };
            Assert.AreEqual(pm.name, "pname");
            Assert.AreEqual(pm.type, "String");
        }
    }
    [TestClass]
    public class TestControllers
    {

        private HomeController homeController;
        private EditorController editorController;
        private ILogger<HomeController> logger;
        public TestControllers()
        {
            //Dependencies
            logger = A.Fake<ILogger<HomeController>>();
            homeController = new HomeController(logger);

            editorController = new EditorController();
        }

        [TestMethod]
        public void testHomeController()
        {
            var result = homeController.Index() as ViewResult;
            var result1 = homeController.CLIDownload() as ViewResult;
            var result2 = homeController.Help() as ViewResult;
            var result3 = homeController.Privacy() as ViewResult;
            // assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result1);
            Assert.IsNotNull(result2);
            Assert.IsNotNull(result3);
        }


        [TestMethod]
        public void testEditorController()
        {
            UserModel model = A.Fake<UserModel>();
            var result = editorController.Index(model) as ViewResult;

            // assert
            Assert.IsNotNull(result);
        }
    }
}