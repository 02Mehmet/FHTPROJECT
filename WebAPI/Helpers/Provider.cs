using DataAccessLayer.Context;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace WebAPI
{
    public class Provider : OAuthAuthorizationServerProvider
    {
        public EmployeeContext employeeContext = new EmployeeContext();

        public Provider()
        {
            var users=employeeContext.Users.ToList();
        }

        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var data = employeeContext.Users.Where(x => x.UserName == context.UserName && x.Password == context.Password).FirstOrDefault();
            
            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            if (context.UserName == data.UserName && context.Password == data.Password)
            {
                identity.AddClaim(new Claim(ClaimTypes.Role, data.Role));
                identity.AddClaim(new Claim(ClaimTypes.Name, data.FullName));                
                context.Validated(identity);
            }           
            else
            {
                context.SetError("Invalid_grant", "Provided username or password is incorrect");
            }
        }
    }
}