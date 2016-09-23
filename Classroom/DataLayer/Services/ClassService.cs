namespace Classroom.DataLayer.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using Entities;
    using Repositories;

    public class ClassService
    {
        private ClassRepository _classRepository;

        public ClassService() { }
        
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