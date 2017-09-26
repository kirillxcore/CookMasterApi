using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CookFormMaster;
using CookFormMaster.Models;
using CookMasterApiModel;
using CookRepository;

namespace CookMasterApi.Controllers
{
    public class CookController : ApiController
    {
        readonly DbService _dbService = new DbService();
        private readonly CookFormManager _cookFormManager = new CookFormManager();

        [HttpPost]
        [ActionName("Login")]
        public HttpResponseMessage Login([FromBody] LoginRequest request)
        {
            var cooker = _dbService.GetCooker(request.Login, request.Password);
            if (cooker == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpPost]
        [ActionName("Publish")]
        public HttpResponseMessage Publish([FromBody] PublishRequest request)
        {
            var dishes = _dbService.GetDishes();
            Menu menu = new Menu
            {
                days = new List<MenuDay> { new MenuDay
                {
                    title = DateTime.Now.Date.AddDays(1).DayOfWeek+' '+DateTime.Now.Date.AddDays(1).ToShortDateString(),
                    categories = new List<Category>()
                }},
                title = "Меню на выбор",
                description = _dbService.GetCookerbyId(2).Name,
                wishes = new Wishes
                {
                    title = "Пожелания",
                    values = new List<string>
                    {
                        "Больше мяса",
                        "Больше сладкого"
                    }
                }
            };

            foreach (var id in request.Ids)
            {
                var dishItem = dishes.Single(x => x.Id == id);
            }

          // _cookFormManager.CreateForm(menu)

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpPost]
        [ActionName("Stat")]
        public HttpResponseMessage Stat(int days)
        {
            StatResponse response = new StatResponse();

            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        [HttpPost]
        [ActionName("Dishes")]
        public HttpResponseMessage Dishes()
        {
            DishesResponse response = new DishesResponse
            {
                Dishes = _dbService.GetDishes()
            };

            return Request.CreateResponse(HttpStatusCode.OK, response);
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
