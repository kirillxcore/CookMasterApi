using CookMasterApiModel;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

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

            var data = Serialize(request);

            using (var webClient = new HttpClient())
            {
                var response = webClient.PostAsync("http://10.195.0.121:61698/cook/login", new StringContent(data, Encoding.UTF8, "application/json")).Result;
                return response.StatusCode == HttpStatusCode.OK;
            }
       }

        public static List<DishItem> GetDishes()
        {
            using (var webClient = new HttpClient())
            {
                var response = webClient.PostAsync("http://10.195.0.121:61698/cook/dishes",
                    new StringContent("", Encoding.UTF8, "application/json")).Result;
                return Deserialize<DishesResponse>(response.Content.ReadAsStringAsync().Result).Dishes;
            }
        }

        public static bool Publish(List<string> ids)
        {
            var request = new PublishRequest
            {
                Ids = ids
            };

            var data = Serialize(request);

            using (var webClient = new HttpClient())
            {
                var response = webClient.PostAsync("http://10.195.0.121:61698/cook/publish", new StringContent(data, Encoding.UTF8, "application/json")).Result;
                return response.StatusCode == HttpStatusCode.OK;
            }
        }

        public static List<DishItemStat> Stat(int date)
        {
            var request = date;

            using (var webClient = new HttpClient())
            {
                var response = webClient.PostAsync("http://10.195.0.121:61698/cook/stat/" + request,
                    new StringContent("", Encoding.UTF8, "application/json")).Result;
                return Deserialize<StatResponse>(response.Content.ReadAsStringAsync().Result).Stat;
            }
        }

        private static string Serialize<T>(T request)
        {
            var serializer = new JsonSerializer();
            var sb = new StringBuilder();
            using (var sw = new StringWriter(sb))
            {
                serializer.Serialize(sw, request);
            }
            var data = sb.ToString();
            return data;
        }

        private static T Deserialize<T>(string request)
        {
            var serializer = new JsonSerializer();
            var reader = new JsonTextReader(new StringReader(request));
            var data = serializer.Deserialize<T>(reader);
            return data;
        }
    }
}
