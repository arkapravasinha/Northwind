using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Security.OAuth;
using Microsoft.Owin.Security.Jwt;
using Microsoft.Owin.Security.DataHandler.Encoder;

[assembly: OwinStartup(typeof(NorthwindSolution.Startup))]

namespace NorthwindSolution
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888

            var issuer = "http://localhost:50407/";
            var audience = "099153c2625149bc8ecb3e85e03f0022";
            var secret = TextEncodings.Base64Url.Decode("IxrAjDoa2FqElO7IhrSrUJELhUckePEPVpaePlS_Xaw");

            var options = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromHours(1),
                Provider = new AuthorizationMiddleware(),
                AccessTokenFormat = new JwtCustomFormat(issuer)
            };

            app.UseOAuthAuthorizationServer(options);

            //normal bearer token
            // app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions() );

            //Jwt bearer token
            app.UseJwtBearerAuthentication(new JwtBearerAuthenticationOptions()
            {

                AllowedAudiences = new[] { audience },
                IssuerSecurityKeyProviders = new[] { new SymmetricKeyIssuerSecurityKeyProvider(issuer,secret) },
                AuthenticationMode = Microsoft.Owin.Security.AuthenticationMode.Active
            });
        }
    }
}
