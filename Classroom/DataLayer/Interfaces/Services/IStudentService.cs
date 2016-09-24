namespace Classroom.DataLayer.Interfaces
{
    using Entities;
    using System.Collections.Generic;

    public interface IStudentService
    {
        List<Student> GetStudents();
        Student GetStudentById(int studentId);
        List<Student> GetStudentsOfClass(int classId);
        List<Student> GetEnrollableStudents(List<Student> existingStudents, int classId);
        void CreateStudent(string firstName, string lastName);
    }
}
