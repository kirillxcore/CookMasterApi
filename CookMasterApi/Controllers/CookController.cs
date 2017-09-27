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

            var dishItems = _dbService.GetDishes();
            Dictionary<int,int> counts = new Dictionary<int, int>();
            foreach (var dish in dishItems)
            {
                counts.Add(int.Parse(dish.Id), 0);
            }

            response.Stat = new List<DishItemStat>();

            for (int i = days; i > 0 ; i--)
            {
                foreach (var menu in _dbService.GetMenus(DefaultCookerId, DateTime.Now.Date.AddDays(2 - i)))
                {
                    var formAnswers = CookFormManager.Instance.GetFormResult(menu.FormId);
                    var dishesRelationsByMenu = _dbService.GetDishesRelationsByMenu(menu.Id);
                    foreach (var formAnswer in formAnswers)
                    {
                        foreach (var value in formAnswer.FormValues)
                        {
                            if (dishesRelationsByMenu.ContainsKey(value.FormId))
                            {
                                counts[dishesRelationsByMenu[value.FormId]]++;
                            }
                        }
                    }

                    foreach (var dishCount in counts)
                    {
                        if (dishCount.Value > 0)
                        {
                            response.Stat.Add(new DishItemStat
                            {
                                Date = DateTime.Now.Date.AddDays(2 - i),
                                Count = dishCount.Value,
                                Item = dishItems.Single(d => d.Id == dishCount.Key.ToString())
                            });
                        }
                    }

                    counts = new Dictionary<int, int>();
                    foreach (var dish in dishItems)
                    {
                        counts.Add(int.Parse(dish.Id), 0);
                    }
                }
            }

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
