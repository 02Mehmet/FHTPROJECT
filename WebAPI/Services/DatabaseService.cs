using DataAccessLayer.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI
{
    public static class DatabaseService
    {
        public static EmployeeContext AuthenticationAuth = new EmployeeContext();
    }
}