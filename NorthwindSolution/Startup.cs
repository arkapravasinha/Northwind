using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Security.OAuth;
using Microsoft.Owin.Security.Jwt;

[assembly: OwinStartup(typeof(NorthwindSolution.Startup))]

namespace NorthwindSolution
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888

            var options = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromHours(1),
                Provider = new AuthorizationMiddleware(),
                AccessTokenFormat = new JwtCustomFormat()
            };

            app.UseOAuthAuthorizationServer(options);

            //normal bearer token
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions() );

            //Jwt bearer token
            app.UseJwtBearerAuthentication(new JwtBearerAuthenticationOptions() { 
            
                AllowedAudiences=new[] {"all"},
                IssuerSecurityKeyProviders=new[] { new SymmetricKeyIssuerSecurityKeyProvider("arka", Convert.FromBase64String("secret")) },
                AuthenticationMode=Microsoft.Owin.Security.AuthenticationMode.Active
            });
        }
    }
}
