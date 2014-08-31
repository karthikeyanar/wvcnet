using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web;
using System.Web.Http;

namespace WVC.API.Controllers
{
    [Authorize]
    public class ValuesController : ApiController
    {

        // GET /values
        [Authorize]
        public IEnumerable<string> Get()
        {
            var authentication = HttpContext.Current.GetOwinContext().Authentication;
            var ticket = authentication.AuthenticateAsync("Application").Result;
            var claimsIdentity = User.Identity as ClaimsIdentity;

            return new string[] { "value1", "value2" };
        }

        // GET /values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST /values
        public void Post([FromBody]string value)
        {
        }

        // PUT /values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE /values/5
        public void Delete(int id)
        {
        }
    }
}
