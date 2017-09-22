using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CookFormMaster;

namespace CookMasterApi.Controllers
{
    public class CookController : ApiController
    {
        // GET: api/Cook
        public string Get()
        {
            CookFormManager cookFormManager = new CookFormManager();
            return cookFormManager.Test();
        }

        // GET: api/Cook/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Cook
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Cook/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Cook/5
        public void Delete(int id)
        {
        }
    }
}
