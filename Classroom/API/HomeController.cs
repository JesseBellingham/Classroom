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
                var enrolments = _enrolmentService.GetEnrolmentsOfClass(classroom.Id);
                var enrolmentModels = new List<EnrolmentModel>();

                foreach (var enrolment in enrolments)
                {
                    enrolmentModels.Add
                    (
                        new EnrolmentModel
                        {
                            Id = enrolment.Id,
                            ClassId = enrolment.ClassId,
                            StudentFirstName = enrolment.Student.FirstName,
                            StudentLastName = enrolment.Student.LastName,
                            StudentAge = enrolment.Student.Age,
                            StudentGPA = enrolment.Student.GPA
                        }
                    );
                }
                models.Add
                (
                    new ClassDataModel
                    {
                        ClassId = classroom.Id,
                        ClassName = classroom.ClassName,
                        TeacherName = classroom.TeacherName,
                        Location = classroom.Location,
                        Enrolments = enrolmentModels
                    }
                );
            }
            return Request.CreateResponse(HttpStatusCode.OK, models);
        }
        
        public HttpResponseMessage CreateNewClass(NewClassModel model)
        {
            var classId = _classService.CreateNewClass(model.ClassName, model.Location, model.TeacherName);
            return Request.CreateResponse(HttpStatusCode.OK, classId);
        }
    }
}
