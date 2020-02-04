using Microsoft.Owin.Security.OAuth;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Owin.Security;

namespace NorthwindSolution
{
    internal class AuthorizationMiddleware : OAuthAuthorizationServerProvider
    {
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            
            var userName = context.UserName;
            var passWord = context.Password;

            bool isCorrectCredentials = (userName == "Admin" && passWord == "Admin") ? true : false;

            if (!isCorrectCredentials)
            {
                return;
            }

            var identity = new ClaimsIdentity(AuthenticationTypes.Password);
            identity.AddClaim(new Claim(ClaimTypes.Role,"admin"));
            identity.AddClaim(new Claim(ClaimTypes.Email, "admin@admin.com"));
            identity.AddClaim(new Claim(ClaimTypes.Name, "Admin Boss"));

            var properties = new AuthenticationProperties();
            var ticket = new AuthenticationTicket(identity, properties);

            context.Validated(ticket);
        }

        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            //if (context.ClientId=="MVC1")
            //{
                context.Validated();
            //}

            //return;
        }
    }
}