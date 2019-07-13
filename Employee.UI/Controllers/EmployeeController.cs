using Employee.UI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using DataAccessLayer.Model;
namespace Employee.UI.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly HttpClient httpClient = new HttpClient();

        private const string BaseUri = "https://localhost:44395/";

        // GET: Employee
        public ActionResult Index()
        {
            var emp = session.helper.get_user(HttpContext.ApplicationInstance.Context);
            if(emp==null)
            {
                return RedirectToAction("Login", "User");
            }
            if (emp != null)
            {
                var repo = new ApiRepository();
                var jsondata = repo.CallApi("https://localhost:44395/api/employee/GetAll", emp.Token);
                var employees = JsonConvert.DeserializeObject<List<EmployeeModel>>(jsondata);
                if(employees!=null)
                {
                    return View(employees);
                }               
            }
            return View();
        }

        public ActionResult Create()
        {
            var usr = session.helper.get_user(HttpContext.ApplicationInstance.Context);
            if (usr == null)
                return RedirectToAction("Login", "User");
            return View();
        }

        [HttpPost]
        public ActionResult Create(EmployeeModel employeeModel)
        {
            var usr = session.helper.get_user(HttpContext.ApplicationInstance.Context);
            if (usr != null)
            {
                var aylıkMaas = Convert.ToDouble(employeeModel.MonthlySalary);
                var yıllıkMaas = aylıkMaas * 12;
                employeeModel.AnnualSalary = yıllıkMaas.ToString();               

                httpClient.BaseAddress = new Uri(BaseUri);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add
                (new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + usr.Token);
                var response = httpClient.PostAsJsonAsync("api/employee/PostRegister", employeeModel);
            }
            return RedirectToAction("Index", "Employee");
        }


    }
}