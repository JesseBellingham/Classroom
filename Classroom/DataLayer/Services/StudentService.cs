namespace Classroom.DataLayer.Services
{
    using Entities;
    using System.Collections.Generic;
    using Repositories;
    using System.Linq;
    using Interfaces;
    using Interfaces.Repositories;
    using System;

    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;

        public StudentService(IStudentRepository studentRepository)
        {
            if (studentRepository == null)
            {
                throw new ArgumentNullException("studentRepository");
            }

            _studentRepository = studentRepository;
        }

        public List<Student> GetStudents()
        {
            return _studentRepository.GetStudents().ToList();
        }

        public Student GetStudentById(int studentId)
        {
            return _studentRepository.GetStudents(student => student.Id == studentId).SingleOrDefault();
        }

        public List<Student> GetStudentsOfClass(string className)
        {
            return
                _studentRepository.GetStudents
                (
                    student =>
                        student.Enrolments.Any(enrolment => string.Equals(enrolment.Class.ClassName, className))
                ).ToList();
        }

        public void CreateStudent(string firstName, string lastName)
        {
            var student = new Student
            {
                FirstName = firstName,
                LastName = lastName
            };

            _studentRepository.CreateStudent(student);
        }
    }
}