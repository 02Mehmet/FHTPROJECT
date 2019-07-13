using DataAccessLayer.Model;
using Employee.UI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Employee.UI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var usr = session.helper.get_user(HttpContext.ApplicationInstance.Context);
            if (usr != null)
            {
                var repo = new ApiRepository();
                var jsondata = repo.CallApi("https://localhost:44395/api/authentication/GetAll", usr.Token);
                var t = JsonConvert.DeserializeObject<List<User>>(jsondata);
                return View(t);
            }
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}