namespace UML.Models
{
    public class GoJsModel
    {
        /*
        public class LinkDataArray
        {
            public int from { get; set; }
            public int to { get; set; }
            public string toArrow { get; set; }
        }
        /*
        public class NodeDataArray
        {
            public string text { get; set; }
            public string loc { get; set; }
            public string color { get; set; }
            public List<object> fields { get; set; }
            public List<object> methodBinding { get; set; }
            public string className { get; set; }
            public bool visible { get; set; }
            public int key { get; set; }
        }
        */


        public string @class { get; set; }
            public List<ScreenModel> nodeDataArray { get; set; }
            public List<SingleRelationsModel> linkDataArray { get; set; }
       
    }
}
