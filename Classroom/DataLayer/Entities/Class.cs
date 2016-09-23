namespace Classroom.DataLayer.Entities
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Class
    {
        public int Id { get; set; }
        [Required]
        public string ClassName { get; set; }
        public string Location { get; set; }
        public string TeacherName { get; set; }

        public virtual ICollection<Enrolment> Enrolments { get; set; }
    }
}