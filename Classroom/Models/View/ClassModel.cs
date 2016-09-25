using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Classroom.Models.View
{
    public class ClassModel
    {
        public int ClassId { get; set; }
        public string ClassName { get; set; }
        public string Location { get; set; }
        public string TeacherName { get; set; }
    }
}