namespace Classroom.App_Start
{
    using Newtonsoft.Json.Serialization;
    using System.Web.Http;

    class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            config.MapHttpAttributeRoutes();

            //config.Routes.MapHttpRoute
            //(
            //    name:"DefaultApi",                
            //    routeTemplate: "api/{controller}/{id}",
            //    defaults: new { id = RouteParameter.Optional }
            //);

            //config.Routes.MapHttpRoute
            //(
            //    name: "GetClassData",
            //    routeTemplate: "api/getclassdata/{id}",
            //    defaults: new { controller = "HomeController", id = RouteParameter.Optional }
            //);
        }
    }
}