using System;
using System.Collections.Generic;

namespace Classroom.DataLayer.Repositories
{
    using System.Linq;
    using Entities;
    using System.Linq.Expressions;

    public class EnrolmentRepository
    {
        private ClassroomDataContext _dataContext;
        public EnrolmentRepository() { }

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