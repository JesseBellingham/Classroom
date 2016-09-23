namespace Classroom.DataLayer.Entities
{
    public class Enrolment
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public int StudentId { get; set; }

        public virtual Class Class { get; set; }
        public virtual Student Student { get; set; }
    }
}