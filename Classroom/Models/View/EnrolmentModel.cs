using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Classroom.Models.View
{
    public class EnrolmentModel
    {
        public int StudentId { get; set; }
        public int ClassId { get; set; }
        public string StudentFirstName { get; set; }
        public string StudentLastName { get; set; }
        public double StudentGPA { get; set; }
        public int StudentAge { get; set; }
    }
}