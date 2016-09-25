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

            //config.Routes.MapHttpRoute
            //(
            //    name: "DefaultApi",
            //    routeTemplate: "api/{controller}/{id}",
            //    defaults: new { id = RouteParameter.Optional }
            //);

            #region Classes
            config.Routes.MapHttpRoute
            (
                name: "GetClasses",
                routeTemplate: "api/{controller}/{action}",
                defaults: new { controller = "Class", action = "Get" }
            );

            config.Routes.MapHttpRoute
            (
                name: "CreateClass",
                routeTemplate: "api/{controller}/{action}",
                defaults: new { controller = "Class", action = "Create" }
            );

            config.Routes.MapHttpRoute
            (
                name: "UpdateClass",
                routeTemplate: "api/{controller}/{action}",
                defaults: new { controller = "Class", action = "Update" }
            );
            #endregion
            #region Enrolments
            config.Routes.MapHttpRoute
            (
                name: "GetStudentEnrolments",
                routeTemplate: "api/{controller}/{action}/{classId}",
                defaults: new { controller = "StudentEnrolment", action = "Get" },
                constraints: new { classId = @"\d+"}
            );

            config.Routes.MapHttpRoute
            (
                name: "CreateEnrolment",
                routeTemplate: "api/{controller}/{action}",
                defaults: new { controller = "Enrolment", action = "Create" }
            );
            #endregion
            #region Students
            config.Routes.MapHttpRoute
            (
                name: "CreateStudent",
                routeTemplate: "api/{controller}/{action}",
                defaults: new { controller = "Student", action = "Create" }
            );
            #endregion
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