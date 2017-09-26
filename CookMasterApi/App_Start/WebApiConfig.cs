using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace CookMasterApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "CookLogin",
                routeTemplate: "cook/login",
                defaults: new { controller = "Cook", action = "Login" }
            );

            config.Routes.MapHttpRoute(
                name: "CookPublish",
                routeTemplate: "cook/publish",
                defaults: new { controller = "Cook", action = "Publish" }
            );

            config.Routes.MapHttpRoute(
                name: "CookStat",
                routeTemplate: "cook/stat/{days}",
                defaults: new { controller = "Cook", action = "Stat" }
            );

            config.Routes.MapHttpRoute(
                name: "CookDishes",
                routeTemplate: "cook/dishes",
                defaults: new { controller = "Cook", action = "Dishes" }
            );
        }
    }
}
