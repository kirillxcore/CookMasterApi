using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CookFormMaster;
using CookMasterApiModel;

namespace CookMasterApi.Controllers
{
    public class CookController : ApiController
    {
        [HttpPost]
        [ActionName("Login")]
        public HttpResponseMessage Login([FromBody] LoginRequest request)
        {
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpPost]
        [ActionName("Publish")]
        public HttpResponseMessage Publish([FromBody] PublishRequest request)
        {
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpPost]
        [ActionName("Stat")]
        public HttpResponseMessage Stat(int days)
        {
            StatResponse response = new StatResponse();

            return Request.CreateResponse(HttpStatusCode.OK, response);

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpPost]
        [ActionName("Dishes")]
        public HttpResponseMessage Dishes()
        {
            DishesResponse response = new DishesResponse();

            return Request.CreateResponse(HttpStatusCode.OK, response);

            return Request.CreateResponse(HttpStatusCode.OK);
        }

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
