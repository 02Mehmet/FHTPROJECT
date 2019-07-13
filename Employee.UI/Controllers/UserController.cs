using DataAccessLayer.Context;
using DataAccessLayer.Model;
using Employee.UI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Employee.UI.Controllers
{
    public class UserController : Controller
    {
        private readonly HttpClient httpClient = new HttpClient();

        private const string BaseUri = "https://localhost:44395/";

        // GET: User
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Login(user user)
        {
            var repo = new ApiRepository();

            var token = repo.GetToken("https://localhost:44395/", user.UserName, user.Password);

            var tokenDeserialize = JsonConvert.DeserializeObject<Token>(token);
            var accessToken = tokenDeserialize.AccessToken;

            if (accessToken != string.Empty)
            {
                user.Token = accessToken;
                Uri u = new Uri("https://localhost:44395/api/authentication/GetByID");
                var payload = "{\"Username\":\"" + user.UserName + "\",\"Password\":\"" + user.Password + "\"}";

                HttpContent c = new StringContent(payload, Encoding.UTF8, "application/json");
                using (var client = new HttpClient())
                {
                    HttpRequestMessage request = new HttpRequestMessage
                    {
                        Method = HttpMethod.Post,
                        RequestUri = u,
                        Content = c
                    };
                    HttpResponseMessage result = await client.SendAsync(request);
                    var usr = JsonConvert.DeserializeObject<user>(result.Content.ReadAsStringAsync().Result);
                    user.AuthenticationID = usr.AuthenticationID;
                    user.Role = usr.Role;
                    user.FullName = usr.FullName;
                    session.helper.add_user(HttpContext.ApplicationInstance.Context, user);
                }
                return RedirectToAction("Index", "Employee");
            }
            else
            {
                string hata = "Kullanıcı adı veya şifre yanlış";
            }

            //var cal = repo.CallApi("", token);
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(User user)
        {
            httpClient.BaseAddress = new Uri(BaseUri);
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add
            (new MediaTypeWithQualityHeaderValue("application/json"));
            var response = httpClient.PostAsJsonAsync("api/authentication/PostRegister", user);

            return RedirectToAction("Login", "User");
        }
    }
}