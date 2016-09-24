using Classroom.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Classroom.Models.View
{
    public class StudentEnrolmentModel
    {
        public List<EnrolmentModel> ExistingStudents { get; set; }
        public List<EnrolmentModel> EnrollableStudents { get; set; }
    }
}