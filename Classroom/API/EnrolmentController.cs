namespace Classroom.API
{
    using Models.View;
    using DataLayer.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Web;
    using System.Web.Http;
    using System.Net;

    public class EnrolmentController : ApiController
    {        
        private readonly IClassService _classService;
        private readonly IEnrolmentService _enrolmentService;
        private readonly IStudentService _studentService;

        public EnrolmentController
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

        [HttpPost]
        public HttpResponseMessage Create(EnrolmentModel model)
        {
            var student = _enrolmentService.CreateEnrolment(model.StudentId, model.ClassId);
            var resultModel = new EnrolmentModel
            {
                ClassId = model.ClassId,
                StudentId = student.Id,
                StudentFirstName = student.FirstName,
                StudentLastName = student.LastName
            };

            return Request.CreateResponse(HttpStatusCode.OK, resultModel);
        }
    }
}