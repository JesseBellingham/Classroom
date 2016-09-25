namespace Classroom.DataLayer.Interfaces
{
    using DomainModels;
    using Entities;
    using System.Collections.Generic;

    public interface IClassService
    {
        List<Class> GetClasses();
        Class GetClassByName(string className);
        Class GetClassById(int id);
        int CreateNewClass(ClassModel model);
        bool UpdateClass(ClassModel model);
    }
}
