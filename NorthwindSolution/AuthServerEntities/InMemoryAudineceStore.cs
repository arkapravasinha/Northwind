using Microsoft.Owin.Security.DataHandler.Encoder;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace NorthwindSolution.AuthServerEntities
{
    public static  class InMemoryAudineceStore
    {
        public static ConcurrentDictionary<string, Audience> Audiencelist = new ConcurrentDictionary<string, Audience>();

        static InMemoryAudineceStore()
        {
            Audiencelist.TryAdd("099153c2625149bc8ecb3e85e03f0022", new Audience()
            {
                ClientID= "099153c2625149bc8ecb3e85e03f0022",
                Base64Secret= "IxrAjDoa2FqElO7IhrSrUJELhUckePEPVpaePlS_Xaw",
                ClientName="mvcapp"
            });
        }

        public static Audience AddAudience(string name)
        {
            var clientId = Guid.NewGuid().ToString("N");
            var key = new byte[32];
            RNGCryptoServiceProvider.Create().GetBytes(key);
            var secret = TextEncodings.Base64Url.Encode(key);

            var audience = new Audience() { ClientID=clientId,ClientName=name,Base64Secret=secret };
            Audiencelist.TryAdd(clientId, audience);
            return audience;
        }

        public static Audience GetAudience(string clientid)
        {
            var audience = new Audience();
            if (Audiencelist.TryGetValue(clientid, out audience))
            {
                return audience;
            }

            return null;
        }
    }
}