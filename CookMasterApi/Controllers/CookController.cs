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
        private const int DefaultCookerId = 2;

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
            try
            {
                var dishes = _dbService.GetDishes();

                Menu menu = new Menu
                {
                    Days = new List<MenuDay>
                    {
                        new MenuDay
                        {
                            Title = DateTime.Now.Date.AddDays(1).DayOfWeek.ToString() + ' ' + DateTime.Now.Date.AddDays(1).ToShortDateString(),
                            Categories = new List<Category>()
                        }
                    },
                    Title = "Меню на выбор",
                    Description = _dbService.GetCookerbyId(DefaultCookerId).Name,
                    Wishes = new Wishes
                    {
                        Title = "Пожелания",
                        Values = new List<string>
                        {
                            "Больше мяса",
                            "Больше сладкого"
                        }
                    }
                };

                dishes = dishes.Where(d => request.Ids.Contains(d.Id)).ToList();

                foreach (var category in _dbService.GetCategories())
                {
                    var catDishes = dishes.Where(d => d.CategoryId == category.Id);
                    if (catDishes.Any())
                    {
                        menu.Days[0].Categories.Add(new Category
                        {
                            Dishes = catDishes.Select(d => new Dish
                            {
                                Id = int.Parse(d.Id),
                                Title = d.Name,
                                Description = d.Description,
                                Image = d.ImageUrl
                            }).ToList(),
                            Title = category.Title
                        });
                    }
                }

                var formResponse = CookFormManager.Instance.CreateForm(menu, "cookmaster2018@gmail.com");

                _dbService.CreateMenu(formResponse.PublishUrl, formResponse.FormId,
                    formResponse.RelationsBetweenIds.Select(x => new Tuple<int, int>(x.Id, x.FormId)).ToList(), DefaultCookerId, DateTime.Now.Date.AddDays(1));
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, e.Message);
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpPost]
        [ActionName("Stat")]
        public HttpResponseMessage Stat(int days)
        {
            StatResponse response = new StatResponse();
            response.Stat = new List<DishItemStat>
            {
                new DishItemStat
                {
                    Date = DateTime.Now.Date.AddDays(1),
                    Count = 20,
                    Item = new DishItem()
                    {
                        Id = "1",
                        Description = "sdfsadfsdfsadfsadfsadf sadf asdf sadf asd f",
                        Name = "Soap 222!",
                        ImageUrl = "https://static.gamespot.com/uploads/original/554/5540228/2735230-8418504311-I-Had-.jpeg",
                        CategoryId = 1,
                        IsVegan = false
                    }
                },
                new DishItemStat
                {
                    Date = DateTime.Now.Date.AddDays(1),
                    Count = 10,
                    Item = new DishItem()
                    {
                        Id = "1",
                        Description = "sdfsadfasdfasdfasdf sadf asdf sadf asd f",
                        Name = "Soap!",
                        ImageUrl = "https://static.gamespot.com/uploads/original/554/5540228/2735230-8418504311-I-Had-.jpeg",
                        CategoryId = 1,
                        IsVegan = false
                    }
                },
                new DishItemStat
                {
                    Date = DateTime.Now.Date.AddDays(1),
                    Count = 2,
                    Item = new DishItem()
                    {
                        Id = "1",
                        Description = "sdfsafsadfsadfsdfsadf asd f",
                        Name = "Soap 333!",
                        ImageUrl = "https://static.gamespot.com/uploads/original/554/5540228/2735230-8418504311-I-Had-.jpeg",
                        CategoryId = 1,
                        IsVegan = false
                    }
                }
            };

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
    }
}
