namespace Classroom.DataLayer.Services
{
    using System;
    using System.Linq;
    using Entities;
    using Repositories;
    using System.Collections.Generic;

    public class EnrolmentService
    {
        private EnrolmentRepository _enrolmentRepository;
        private StudentService _studentService;
        private ClassService _classService;

        public EnrolmentService() { }

        public List<Enrolment> GetEnrolments()
        {
            return _enrolmentRepository.GetEnrolments().ToList();
        }

        public List<Enrolment> GetEnrolmentsOfStudent(int studentId)
        {
            return
                _enrolmentRepository.GetEnrolments(enrolment => enrolment.StudentId == studentId)
                .ToList();
        }

        public void CreateEnrolment(int studentId, int classId)
        {
            var student = _studentService.GetStudentById(studentId);
            var classToEnrolInto = _classService.GetClassById(classId);

            var enrolment = new Enrolment
            {
                Student = student,
                Class = classToEnrolInto
            };
            _enrolmentRepository.CreateEnrolment(enrolment);
        }
    }
}