namespace Classroom.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using DataLayer.Entities;
    using System.Collections.Generic;

    internal sealed class Configuration : DbMigrationsConfiguration<DataLayer.ClassroomDataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DataLayer.ClassroomDataContext context)
        {
            var students = new List<Student>
            {
                new Student
                {
                    FirstName = "Carson",
                    LastName = "Alexander",
                    GPA = 3.5,
                    Age = 21
                },
                new Student
                {
                    FirstName = "Meredith",
                    LastName = "Alonso",
                    GPA = 3.7,
                    Age = 26
                },
                new Student
                {
                    FirstName = "Arthur",
                    LastName = "Johnson",
                    GPA = 3.3,
                    Age = 20
                },
                new Student
                {
                    FirstName = "Myrtle",
                    LastName = "Mooney",
                    GPA = 3.8,
                    Age = 38
                }
            };
            students.ForEach(s => context.Students.AddOrUpdate(p => p.LastName, s));
            context.SaveChanges();

            var classes = new List<Class>
            {
                new Class
                {
                    ClassName = "Chemistry",
                    Location = "Building 7 Room 25",
                    TeacherName = "Miss Jackson"
                },
                new Class
                {
                    ClassName = "Biology",
                    Location = "Building 13 Room 25",
                    TeacherName = "Mr Mann"
                },
                new Class
                {
                    ClassName = "Economics",
                    Location = "Building 4 Room 13",
                    TeacherName = "Mr Jeffreys"
                }
            };
            classes.ForEach(c => context.Classes.AddOrUpdate(p => p.ClassName, c));
            context.SaveChanges();

            var enrolments = new List<Enrolment>
            {
                new Enrolment
                {
                    StudentId = students.Single(s => s.LastName == "Alexander").Id,
                    CourseId = classes.Single(c => c.ClassName == "Chemistry" ).Id
                },
                 new Enrolment
                 {
                    StudentId = students.Single(s => s.LastName == "Johnson").Id,
                    CourseId = classes.Single(c => c.ClassName == "Biology" ).Id
                 },
                 new Enrolment
                 {
                    StudentId = students.Single(s => s.LastName == "Mooney").Id,
                    CourseId = classes.Single(c => c.ClassName == "Economics" ).Id
                 }
            };

            foreach (var enrolment in enrolments)
            {
                var enrolmentExists =
                    context.Enrolments.Any
                    (
                        e =>
                             e.Student.Id == enrolment.StudentId &&
                             e.Class.Id == e.CourseId
                    );
                if (!enrolmentExists)
                {
                    context.Enrolments.Add(enrolment);
                }
            }
            context.SaveChanges();
        }
    }
}
