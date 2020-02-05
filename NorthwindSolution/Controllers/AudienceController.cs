using NorthwindSolution.AuthServerEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace NorthwindSolution.Controllers
{
    [RoutePrefix("api/Audience")]
    public class AudienceController : ApiController
    {
        [HttpPost]
        [Route("AddAudience")]
        public IHttpActionResult AddAudience(AudienceModel value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Audience audience = InMemoryAudineceStore.AddAudience(value.Name);

            return Ok<Audience>(audience);
        }

        
    }
}