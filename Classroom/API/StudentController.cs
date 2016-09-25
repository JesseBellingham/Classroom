namespace Classroom.API
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Web;
    using System.Web.Http;
    using Models.View;
    using DataLayer.Interfaces;
    using System.Net;

    public class StudentController : ApiController
    {
        private readonly IClassService _classService;
        private readonly IEnrolmentService _enrolmentService;
        private readonly IStudentService _studentService;

        public StudentController
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

        public HttpResponseMessage Get()
        {
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        public HttpResponseMessage Create(EnrolmentModel model)
        {
            var student =
                _studentService.CreateStudent
                (
                    model.StudentFirstName,
                    model.StudentLastName,
                    model.StudentGPA,
                    model.StudentAge
                );

            var response = student == null ?
                Request.CreateResponse(HttpStatusCode.BadRequest) :
                Request.CreateResponse(HttpStatusCode.OK, student);

            return response;
        }
    }
}