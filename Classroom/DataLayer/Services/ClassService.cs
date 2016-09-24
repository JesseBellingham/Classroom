namespace Classroom.DataLayer.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using Entities;
    using Repositories;
    using Interfaces;
    using Interfaces.Repositories;
    using System;

    public class ClassService : IClassService
    {
        private readonly IClassRepository _classRepository;

        public ClassService(IClassRepository classRepository)
        {
            if (classRepository == null)
            {
                throw new ArgumentNullException("classRepository");
            }

            _classRepository = classRepository;
        }
        
        public List<Class> GetClasses()
        {
            return _classRepository.GetClasses().ToList();
        }

        public Class GetClassByName(string className)
        {
            return _classRepository.GetClasses(c => string.Equals(c.ClassName, className)).SingleOrDefault();
        }

        public Class GetClassById(int id)
        {
            return _classRepository.GetClasses(c => c.Id == id).SingleOrDefault();
        }
    }
}