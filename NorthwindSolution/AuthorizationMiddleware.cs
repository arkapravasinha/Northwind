using Microsoft.Owin.Security.OAuth;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Owin.Security;
using NorthwindSolution.AuthServerEntities;
using System.Collections.Generic;

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

            var properties = new AuthenticationProperties(new Dictionary<string, string>
                {
                    {
                         "audience", (context.ClientId == null) ? string.Empty : context.ClientId
                    }
                });

            var ticket = new AuthenticationTicket(identity, properties);

            context.Validated(ticket);
        }

        public override  Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {

            //Normal bearer token Authentication
            // context.Validated();

            string clientId = string.Empty;
            string clientSecret = string.Empty;
            string symmetricKeyAsBase64 = string.Empty;

            if (!context.TryGetBasicCredentials(out clientId, out clientSecret))
            {
                context.TryGetFormCredentials(out clientId, out clientSecret);
            }

            if (context.ClientId == null)
            {
                context.SetError("invalid_clientId", "client_Id is not set");
                return  Task.FromResult<object>(null);
            }

            var audience = InMemoryAudineceStore.GetAudience(context.ClientId);

            if (audience == null)
            {
                context.SetError("invalid_clientId", string.Format("Invalid client_id '{0}'", context.ClientId));
                return Task.FromResult<object>(null);
            }

            context.Validated();
            return Task.FromResult<object>(null);

        }
    }
}