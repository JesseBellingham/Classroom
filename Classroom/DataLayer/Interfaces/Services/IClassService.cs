namespace Classroom.DataLayer.Interfaces
{
    using Entities;
    using System.Collections.Generic;

    public interface IClassService
    {
        List<Class> GetClasses();
        Class GetClassByName(string className);
        Class GetClassById(int id);
    }
}
