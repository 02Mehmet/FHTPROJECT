using DataAccessLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPI.Model;
using WebAPI.Repository;

namespace WebAPI.Controllers
{
    [Authorize]
    public class AuthenticationController : ApiController
    {
        private AuthenticationRepository repository = new AuthenticationRepository();
        public AuthenticationController()
        {
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/authentication/GetByID")]
        public IHttpActionResult GetByID(LoginModelView loginModelView)
        {
            return Ok(repository.GetByID(loginModelView));
        }

        [Authorize(Roles ="user,admin")]
        [HttpGet]
        [Route("api/authentication/GetAll")]
        public IHttpActionResult GetAll()
        {
            return Ok(repository.GetUsers());
        }

        [HttpPost]
        [Route("api/authentication/PostLogin")]
        public IHttpActionResult PostLogin(LoginModelView loginModelView)
        {
            return Ok(repository.Authenticate(loginModelView));
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/authentication/PostRegister")]
        public bool PostRegister(User user)
        {
            return repository.CreateNewUser(user);
        }
    }
}
