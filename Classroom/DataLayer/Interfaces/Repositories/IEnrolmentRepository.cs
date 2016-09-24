namespace Classroom.DataLayer.Interfaces.Repositories
{
    using Entities;
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    public interface IEnrolmentRepository
    {
        IQueryable<Enrolment> GetEnrolments();
        IQueryable<Enrolment> GetEnrolments(Expression<Func<Enrolment, bool>> filter);
        IQueryable<Enrolment> GetEnrolmentsOfStudent(int studentId);
        void CreateEnrolment(Enrolment enrolment);
    }
}
