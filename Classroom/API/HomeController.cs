namespace Classroom.API
{
    using Models.View;
    using DataLayer.Services;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using System.Collections.Generic;
    using System.Web.Mvc;    
    using Route = System.Web.Http.RouteAttribute;

    public class HomeController : ApiController
    {
        private ClassService _classService;

        [Route("/api/getclassdata")]
        public JsonResult GetClassData()
        {
            var classes = _classService.GetClasses();
            var models = new List<ClassDataModel>();

            foreach (var classroom in classes)
            {
                models.Add
                (
                    new ClassDataModel
                    {
                        ClassId = classroom.Id,
                        ClassName = classroom.ClassName,
                        TeacherName = classroom.TeacherName
                    }
                );
            }

            return Json(models);
        }
    }
}
