

namespace Classroom.DataLayer.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using Entities;
    using System.Linq.Expressions;

    public class ClassRepository
    {
        private ClassroomDataContext _dataContext;

        public ClassRepository() { }

        public IQueryable<Class> GetClasses(Expression<Func<Class, bool>> filter)
        {
            return _dataContext.Classes.Where(filter);
        }

        public IQueryable<Class> GetClasses()
        {
            return _dataContext.Classes;
        }
    }
}