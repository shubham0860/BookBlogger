using Microsoft.Web.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;

namespace BookBlogger.Web
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            //config. IgnoreRoute(“{ resource}.axd /{ *pathInfo}”);

            //routes.MapMvcAttributeRoutes();
            // Web API routes
            config.MapHttpAttributeRoutes();



            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { controller = "books", id = RouteParameter.Optional }
            );


            EnableCorsAttribute cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);
        }
    }
}
