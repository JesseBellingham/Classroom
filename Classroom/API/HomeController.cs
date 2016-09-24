namespace Classroom.API
{
    using Models.View;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using DataLayer.Interfaces;
    using System;

    public class HomeController : ApiController
    {
        private readonly IClassService _classService;
        private readonly IEnrolmentService _enrolmentService;
        private readonly IStudentService _studentService;

        public HomeController
        (
            IClassService classService,
            IEnrolmentService enrolmentService,
            IStudentService studentService
        )
        {
            if (classService == null)
            {
                throw new ArgumentNullException("classService");
            }
            if (enrolmentService == null)
            {
                throw new ArgumentNullException("enrolmentService");
            }
            if (studentService == null)
            {
                throw new ArgumentNullException("studentService");
            }

            _classService = classService;
            _enrolmentService = enrolmentService;
            _studentService = studentService;
        }

        [HttpGet]
        public HttpResponseMessage GetClassData()
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
                        TeacherName = classroom.TeacherName,
                        Location = classroom.Location
                    }
                );
            }
            //var result = JsonConvert.SerializeObject(models);
            return Request.CreateResponse(HttpStatusCode.OK, models);//Json(models);
        }

        //private async Task<ClassDataModel> GetClasses()
        //{
        //    var classes = _classService.GetClasses();
        //    var models = new List<ClassDataModel>();

        //    foreach (var classroom in classes)
        //    {
        //        models.Add
        //        (
        //            new ClassDataModel
        //            {
        //                ClassId = classroom.Id,
        //                ClassName = classroom.ClassName,
        //                TeacherName = classroom.TeacherName
        //            }
        //        );
        //    }
        //}

        //[ResponseType(typeof(List<ClassDataModel>))]
        //public async Task<IHttpActionResult> Get()
        //{
        //    var classes = _classService.GetClasses();
        //    var models = new List<ClassDataModel>();

        //    foreach (var classroom in classes)
        //    {
        //        models.Add
        //        (
        //            new ClassDataModel
        //            {
        //                ClassId = classroom.Id,
        //                ClassName = classroom.ClassName,
        //                TeacherName = classroom.TeacherName
        //            }
        //        );
        //    }
        //    //var result = JsonConvert.SerializeObject(models);
        //    return this.Ok(models); //Json(result);//Json(models);
        //}
    }
}
