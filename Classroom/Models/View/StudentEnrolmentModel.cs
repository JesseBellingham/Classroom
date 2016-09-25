namespace Classroom.Models.View
{
    using System.Collections.Generic;

    public class StudentEnrolmentModel
    {
        public List<EnrolmentModel> ExistingStudents { get; set; }
        public List<EnrolmentModel> EnrollableStudents { get; set; }
    }
}