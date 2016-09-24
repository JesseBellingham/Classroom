namespace Classroom.DataLayer
{
    using Entities;
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;

    public class ClassroomDataContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Enrolment> Enrolments { get; set; }
        public DbSet<Class> Classes { get; set; }

        public ClassroomDataContext() { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}