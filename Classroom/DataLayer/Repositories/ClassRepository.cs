

namespace Classroom.DataLayer.Repositories
{
    using System;
    using System.Linq;
    using Entities;
    using System.Linq.Expressions;
    using Interfaces.Repositories;

    public class ClassRepository : IClassRepository
    {
        private readonly ClassroomDataContext _dataContext;

        public ClassRepository(ClassroomDataContext dataContext)
        {
            if (dataContext == null)
            {
                throw new ArgumentNullException("dataContext");
            }

            _dataContext = dataContext;
        }

        public IQueryable<Class> GetClasses(Expression<Func<Class, bool>> filter)
        {            
            return _dataContext.Classes.Where(filter);
        }

        public IQueryable<Class> GetClasses()
        {
            return _dataContext.Classes;
        }

        public int CreateClass(Class newClass)
        {
            _dataContext.Classes.Add(newClass);
            _dataContext.SaveChanges();
            return newClass.Id;
        }

        public void UpdateClass(Class existingClass)
        {
            var classToUpdate = _dataContext.Classes.Where(c => c.Id == existingClass.Id).Single();
            classToUpdate.ClassName = existingClass.ClassName;
            classToUpdate.Location = existingClass.Location;
            classToUpdate.TeacherName = existingClass.TeacherName;

            _dataContext.SaveChanges();
        }
    }
}