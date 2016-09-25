namespace Classroom.Models.View
{
    using System.Collections.Generic;

    public class ClassDataModel
    {
        public int ClassId { get; set; }
        public string ClassName { get; set; }
        public string TeacherName { get; set; }
        public string Location { get; set; }

        public List<EnrolmentModel> Enrolments { get; set; }
    }
}