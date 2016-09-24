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

        public List<Student> GetStudentsOfClass(int classId)
        {
            return
                _studentRepository.GetStudents
                (
                    student =>
                        student.Enrolments.Any(enrolment => enrolment.ClassId == classId)
                ).ToList();
        }

        public List<Student> GetEnrollableStudents(List<Student>existingStudents, int classId)
        {
            //var existingStudents = GetStudentsOfClass(classId);
            var names = existingStudents.Select(es => es.LastName);
            return
                _studentRepository.GetStudents
                (
                    student =>
                        // I find !Any easier to read in this context than All()
                        // I don't think there is any real difference in the IL produced
                        !student.Enrolments.Any(enrolment => enrolment.ClassId == classId) &&
                        !names.Any(name => string.Equals(name, student.LastName))
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