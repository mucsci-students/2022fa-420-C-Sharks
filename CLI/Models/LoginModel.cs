using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLI.Models
{
    public class LoginModel
    {
        public UserModel? User { get; set; }
        public int status { get; set; }
    }
}
