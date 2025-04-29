using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Noya.Models
{
    namespace Noya.Models
    {
        public class LoginResponse
        {
            public int UserId { get; set; }
            public string Name { get; set; }
            public string Email { get; set; }
            public string Role { get; set; } // Admin, User, etc.
        }
    }


}
