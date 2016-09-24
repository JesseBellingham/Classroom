namespace Classroom.DataLayer.Interfaces.Repositories
{
    using Entities;
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    public interface IClassRepository
    {
        IQueryable<Class> GetClasses(Expression<Func<Class, bool>> filter);
        IQueryable<Class> GetClasses();
        int CreateClass(Class newClass);
    }
}
