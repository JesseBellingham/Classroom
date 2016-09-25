using Classroom.DataLayer.Interfaces;
using Classroom.Models.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace Classroom.API
{
    public class ClassController : ApiController
    {
        private readonly IClassService _classService;
        private readonly IEnrolmentService _enrolmentService;
        private readonly IStudentService _studentService;

        public ClassController
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
        public HttpResponseMessage Get()
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
                            StudentId = enrolment.Id,
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

        public HttpResponseMessage Create(ClassModel model)
        {
            var newClassModel = new DataLayer.DomainModels.ClassModel
            {
                ClassName = model.ClassName,
                Location = model.Location,
                TeacherName = model.TeacherName
            };
            var classId = _classService.CreateNewClass(newClassModel);
            return Request.CreateResponse(HttpStatusCode.OK, classId);
        }

        public HttpResponseMessage Update(ClassModel model)
        {
            var classModel = new DataLayer.DomainModels.ClassModel
            {
                ClassId = model.ClassId,
                ClassName = model.ClassName,
                TeacherName = model.TeacherName,
                Location = model.Location
            };
            var success = _classService.UpdateClass(classModel);
            return Request.CreateResponse(HttpStatusCode.OK, success);
        }
    }
}