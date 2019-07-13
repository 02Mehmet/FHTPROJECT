using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using DataAccessLayer;
using DataAccessLayer.Context;
using DataAccessLayer.Model;
using Newtonsoft.Json;
using WebAPI.Model;

namespace WebAPI.Repository
{
    public class AuthenticationRepository
    {
        public readonly EmployeeContext employeeContext = new EmployeeContext();

        public readonly List<User> CachedAuthenticates = new List<User>();

        public AuthenticationRepository()
        {
            CachedAuthenticates.AddRange(employeeContext.Users.ToList());
        }
        public User GetByID(LoginModelView loginModelView)
        {
            return DatabaseService.AuthenticationAuth.Users.Where(x=>x.UserName==loginModelView.UserName&&x.Password==loginModelView.Password).FirstOrDefault();
        }
        public List<User> GetUsers()
        {
            return DatabaseService.AuthenticationAuth.Users.ToList();
        }

        public bool CreateNewUser(User user)
        {
            try
            {
                var authenticationUser = CachedAuthenticates.FirstOrDefault(x => x.UserName == user.UserName);
                if (authenticationUser != null)
                    return true;
                authenticationUser = new User()
                {
                    FullName = user.FullName,
                    UserName = user.UserName,
                    Password = user.Password,
                    Role = user.Role
                };
                employeeContext.Users.Add(authenticationUser);
                CachedAuthenticates.Add(authenticationUser);
                employeeContext.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.InnerException);
                return false;
            }
        }

        public User Authenticate(LoginModelView loginModelView)
        {
            var user=employeeContext.Users.Where(a => a.UserName == loginModelView.UserName && a.Password == loginModelView.Password).FirstOrDefault();
            if (user == null)
                return null;
            return user;
        }

        public string GetToken(string url, string userName, string password)
        {
            var pairs = new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string, string>( "grant_type", "password" ),
                        new KeyValuePair<string, string>( "username", userName ),
                        new KeyValuePair<string, string> ( "Password", password )
                    };
            var content = new FormUrlEncodedContent(pairs);
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
            using (var client = new HttpClient())
            {
                var response = client.PostAsync(url + "Token", content).Result;
                return response.Content.ReadAsStringAsync().Result;
            }
        }

        public string CallApi(string url, string token)
        {
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
            using (var client = new HttpClient())
            {
                if (!string.IsNullOrWhiteSpace(token))
                {
                    var t = JsonConvert.DeserializeObject<Token>(token);

                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + t.AccessToken);
                }
                var response = client.GetAsync(url).Result;
                return response.Content.ReadAsStringAsync().Result;
            }
        }
    }
}