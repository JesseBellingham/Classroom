using Autofac;
using Autofac.Integration.Mvc;
using Classroom.App_Start;
using Classroom.DataLayer;
using Classroom.DataLayer.Interfaces;
using Classroom.DataLayer.Interfaces.Repositories;
using Classroom.DataLayer.Repositories;
using Classroom.DataLayer.Services;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Classroom
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);            
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            RegisterDependencies();

        }

        private void RegisterDependencies()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterType<ClassroomDataContext>().InstancePerRequest();
            builder.RegisterType<ClassRepository>().As<IClassRepository>();
            builder.RegisterType<StudentRepository>().As<IStudentRepository>();
            builder.RegisterType<EnrolmentRepository>().As<IEnrolmentRepository>();
            builder.RegisterType<ClassService>().As<IClassService>();
            builder.RegisterType<StudentService>().As<IStudentService>();
            builder.RegisterType<EnrolmentService>().As<IEnrolmentService>();

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}
