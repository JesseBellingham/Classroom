namespace Classroom.DataLayer.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using Entities;
    using System.Linq.Expressions;

    public class StudentRepository
    {
        private ClassroomDataContext _dataContext;
        public StudentRepository() { }

        public IQueryable<Student> GetStudents(Expression<Func<Student, bool>> filter)
        {
            return _dataContext.Students.Where(filter);
        }

        public IQueryable<Student> GetStudents()
        {
            return _dataContext.Students;
        }

        public void CreateStudent(Student student)
        {
            _dataContext.Students.Add(student);
            _dataContext.SaveChanges();
        }
    }
}