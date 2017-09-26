using CookMasterApiModel;
using System.Collections.Generic;

namespace CookAndroid.Repo
{
    public static class APIClient
    {
        public static bool Login(string name, string password)
        {
            var request = new LoginRequest
            {
                Login = name,
                Password = password
            };

            // Do something

            var response = name.Contains("f");
            return response;
        }

        public static List<DishItem> GetDishes()
        {
            var request = new DishesResponse();

            // Do something

            var response = new List<DishItem>
            {
                new DishItem
                {
                    Name = "Meat",
                    IsVegan = false,
                    ImageUrl = "https://www.google.ru/images/branding/googlelogo/2x/googlelogo_color_120x44dp.png"
                },
                new DishItem
                {
                    Name = "Salad",
                    IsVegan = false,
                    ImageUrl = "https://www.google.ru/images/branding/googlelogo/2x/googlelogo_color_120x44dp.png"
                },
                new DishItem
                {
                    Name = "Soap",
                    IsVegan = true,
                    ImageUrl = "https://www.google.ru/images/branding/googlelogo/2x/googlelogo_color_120x44dp.png"
                }
            };
            return response;
        }

        public static bool Publish(List<string> ids)
        {
            var request = new PublishRequest
            {
                Ids = ids
            };

            // Do something

            var response = true;
            return response;
        }

        public static List<DishItemStat> Stat(int date)
        {
            var request = date;

            // Do something

            var response = new List<DishItemStat>
            {
                new DishItemStat
                {
                    Item = new DishItem{
                        Name = "Meat",
                        IsVegan = true,
                    ImageUrl = "https://www.google.ru/images/branding/googlelogo/2x/googlelogo_color_120x44dp.png"
                    },
                    Count = 7
                },
                new DishItemStat
                {
                    Item = new DishItem{
                        Name = "Salad",
                        IsVegan = false,
                    ImageUrl = "https://www.google.ru/images/branding/googlelogo/2x/googlelogo_color_120x44dp.png"
                    },
                    Count = 3
                },
                new DishItemStat
                {
                    Item = new DishItem{
                        Name = "Soap",
                        IsVegan = false,
                    ImageUrl = "https://www.google.ru/images/branding/googlelogo/2x/googlelogo_color_120x44dp.png"
                    },
                    Count = 5
                }
            };
            return response;
        }
    }
}
