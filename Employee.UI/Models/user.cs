using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Employee.UI.Models
{
    public class user
    {
        public int AuthenticationID { get; set; }

        public string FullName { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string Role { get; set; }

        public string Token { get; set; }

    }
}