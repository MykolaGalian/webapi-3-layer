using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Ninject.Web.WebApi;
using Ninject.Web.WebApi.OwinHost;
using WebApi.App_Start;

namespace WebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //ReferenceLoop Ignore
            //config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling =
            //    Newtonsoft.Json.ReferenceLoopHandling.Ignore;

            // Web API configuration and services

            // Web API routes
           

            config.MapHttpAttributeRoutes();
            

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
