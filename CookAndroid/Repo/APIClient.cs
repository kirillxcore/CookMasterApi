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
                Name = name,
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
                    IsVegan = false
                },
                new DishItem
                {
                    Name = "Salad",
                    IsVegan = false
                },
                new DishItem
                {
                    Name = "Soap",
                    IsVegan = true
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
                        IsVegan = true
                    },
                    Count = 7
                },
                new DishItemStat
                {
                    Item = new DishItem{
                        Name = "Salad",
                        IsVegan = false
                    },
                    Count = 3
                },
                new DishItemStat
                {
                    Item = new DishItem{
                        Name = "Soap",
                        IsVegan = false
                    },
                    Count = 5
                }
            };
            return response;
        }
    }
}
