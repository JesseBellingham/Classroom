namespace Classroom.DataLayer.Entities
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Student
    {
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public int Age { get; set; }
        public double GPA { get; set; }

        public virtual ICollection<Enrolment> Enrolments { get; set; }
    }
}