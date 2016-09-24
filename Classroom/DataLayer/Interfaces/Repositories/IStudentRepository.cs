namespace Classroom.DataLayer.Interfaces.Repositories
{
    using Entities;
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    public interface IStudentRepository
    {
        IQueryable<Student> GetStudents(Expression<Func<Student, bool>> filter);
        IQueryable<Student> GetStudents();
        void CreateStudent(Student student);
    }
}
