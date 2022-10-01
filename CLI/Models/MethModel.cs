using CLI.Models;

namespace UML.Models
{
    public class Methods
    {
        public string name { get; set; }
        public string return_type { get; set; }
        public Fields[] parameters { get; set; }
    }
}