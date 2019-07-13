using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using DataAccessLayer.Context;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Repository repository = new Repository();
            var token=repository.GetToken("https://localhost:44395/","user","user");
            Console.WriteLine(token);

            var result = repository.CallApi("https://localhost:44395/api/authentication/GetAll", token);
            
            EmployeeContext employeeContext = new EmployeeContext();

            var xxxx=employeeContext.Employees.FirstOrDefault();


        }
    }
}
