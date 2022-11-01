using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLI.Models
{
    public class ExportRelation
    {
        public string? fromName { get; set; }
        public string? toName { get; set; }
        public string toArrow { get; set; }
    }
}
