using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Model
{
    public class LoginModelView
    {
        public int AuthenticationID { get; set; }

        public string FullName { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }
        public string Role { get; set; }

        public string Token { get; set; }


    }
}