namespace Classroom.DataLayer.Repositories
{
    using System;
    using System.Linq;
    using Entities;
    using System.Linq.Expressions;
    using Interfaces.Repositories;

    public class EnrolmentRepository : IEnrolmentRepository
    {
        private readonly ClassroomDataContext _dataContext;

        public EnrolmentRepository(ClassroomDataContext dataContext)
        {
            if (dataContext == null)
            {
                throw new ArgumentNullException("dataContext");
            }

            _dataContext = dataContext;
        }

        public IQueryable<Enrolment> GetEnrolments()
        {
            return _dataContext.Enrolments;
        }

        public IQueryable<Enrolment> GetEnrolments(Expression<Func<Enrolment, bool>> filter)
        {
            return _dataContext.Enrolments.Where(filter);
        }

        public IQueryable<Enrolment> GetEnrolmentsOfStudent(int studentId)
        {
            return _dataContext.Enrolments.Where(enrolment => enrolment.StudentId == studentId);
        }

        public void CreateEnrolment(Enrolment enrolment)
        {
            _dataContext.Enrolments.Add(enrolment);
            _dataContext.SaveChanges();
        }
    }
}