namespace Classroom.App_Start
{
    using Autofac;
    using Autofac.Integration.WebApi;
    using API;
    using DataLayer.Interfaces;
    using DataLayer.Services;
    using Newtonsoft.Json.Serialization;
    using System.Reflection;
    using System.Web.Http;
    using DataLayer.Repositories;
    using DataLayer.Interfaces.Repositories;
    using DataLayer;
    using Autofac.Integration.Mvc;

    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute
            (
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute
            (
                name: "GetClassData",
                routeTemplate: "api/{controller}/{action}",
                defaults: new { controller = "Home", action = "GetClassData" }
            );

            config.Routes.MapHttpRoute
            (
                name: "CreateNewClass",
                routeTemplate: "api/{controller}/{action}",
                defaults: new { controller = "Home", action = "CreateNewClass" }
            );

            config.Routes.MapHttpRoute
            (
                name: "GetStudentsForEnrolment",
                routeTemplate: "api/{controller}/{action}/{classId}",
                defaults: new { controller = "Home", action = "GetStudentsForEnrolment" },
                constraints: new { classId = @"\d+"}
            );

            config.Routes.MapHttpRoute
            (
                name: "EnrolStudent",
                routeTemplate: "api/{controller}/{action}",
                defaults: new { controller = "Home", action = "EnrolStudent" }
            );

            #region Dependency Injection
            var builder = new ContainerBuilder();
            
            builder.RegisterType<ClassroomDataContext>().InstancePerRequest();
            builder.RegisterType<ClassRepository>().As<IClassRepository>();
            builder.RegisterType<StudentRepository>().As<IStudentRepository>();
            builder.RegisterType<EnrolmentRepository>().As<IEnrolmentRepository>();
            builder.RegisterType<ClassService>().As<IClassService>();
            builder.RegisterType<StudentService>().As<IStudentService>();
            builder.RegisterType<EnrolmentService>().As<IEnrolmentService>();

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterModule(new AutofacWebTypesModule());

            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            #endregion
        }
    }
}