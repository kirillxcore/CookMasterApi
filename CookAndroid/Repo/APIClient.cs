using System;
using CookMasterApiModel;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CookAndroid.Repo
{
    public static class APIClient
    {
        public static string HostName = "http://10.195.0.121:61698";

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
                webClient.Timeout = TimeSpan.FromSeconds(5);
                try
                {
                    var response = webClient.PostAsync(HostName + "/cook/login",
                        new StringContent(data, Encoding.UTF8, "application/json")).Result;
                    return response.StatusCode == HttpStatusCode.OK;
                }
                catch
                {
                    return false;
                }
            }
       }

        public static async Task<List<DishItem>> GetDishes()
        {
            using (var webClient = new HttpClient())
            {
                try
                {
                    var response = await webClient.PostAsync(HostName + "/cook/dishes",
                        new StringContent("", Encoding.UTF8, "application/json"));
                    var text = await response.Content.ReadAsStringAsync();
                    return Deserialize<DishesResponse>(text).Dishes;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public static async Task<bool> Publish(List<string> ids)
        {
            var request = new PublishRequest
            {
                Ids = ids
            };

            var data = Serialize(request);

            using (var webClient = new HttpClient())
            {
                var response = await webClient.PostAsync(HostName + "/cook/publish",
                    new StringContent(data, Encoding.UTF8, "application/json"));
                return response.IsSuccessStatusCode;
            }
        }

        public static async Task<List<DishItemStat>> Stat(int date)
        {
            var request = date;
            using (var webClient = new HttpClient())
            {
                try
                {
                    var response = await webClient.PostAsync(HostName + "/cook/stat/" + request,
                        new StringContent("", Encoding.UTF8, "application/json"));
                    var text = await response.Content.ReadAsStringAsync();
                    return Deserialize<StatResponse>(text).Stat;
                }
                catch (Exception)
                {
                    return null;
                }
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
