namespace Classroom.DataLayer.Interfaces
{
    using Entities;
    using System.Collections.Generic;

    public interface IEnrolmentService
    {
        List<Enrolment> GetEnrolments();
        List<Enrolment> GetEnrolmentsOfStudent(int studentId);
        void CreateEnrolment(int studentId, int classId);
        List<Enrolment> GetEnrolmentsOfClass(int classId);
    }
}
