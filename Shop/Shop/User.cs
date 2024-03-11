using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop
{
    public class User
    {
        public int Id { get; set; } 
        public string Login { get; set; }
        public string Password { get; set; }
        public int Balance { get; set; }
        public int Role { get; set; }
    }
}
