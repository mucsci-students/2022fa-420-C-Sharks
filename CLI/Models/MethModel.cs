using CLI.Models;
namespace CLI.Models
{
    public class Methods
    {
        public string methodName { get; set; }
        public string return_type { get; set; }
        public Parameters[]? methodParams { get; set; }
    }
}