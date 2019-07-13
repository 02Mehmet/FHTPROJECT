using DataAccessLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPI.Repository;

namespace WebAPI.Controllers
{
    [Authorize]
    public class EmployeeController : ApiController
    {
        private EmployeeRepository repository = new EmployeeRepository();
        public EmployeeController()
        {

        }

        [Authorize(Roles = "Admin,User")]
        [HttpGet]
        [Route("api/employee/GetAll")]
        public IHttpActionResult GetAll()
        {
            return Ok(repository.GetEmployees());
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("api/employee/PutEmployee")]
        public IHttpActionResult PutEmployee(Employee employee)
        {
            return Ok(repository.UpdateEmployee(employee));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("api/employee/PostRegister")]
        /// <summary>Gets the sha256 NON-encrypted username and password and adds username to DB</summary>
        public bool PostRegister(Employee employee)
        {
            return repository.CreateNewEmployee(employee);
        }
    }
}
