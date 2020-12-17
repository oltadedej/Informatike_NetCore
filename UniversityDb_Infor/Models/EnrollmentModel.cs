using System;
using System.Collections.Generic;
using System.Text;

namespace UniversityDb_Infor.Models
{
    public class EnrollmentModel
    {
        public int IdEnrollment { get; set; }

        public int IdStudent { get; set; }
        public int IdCourse { get; set; }
        public int? Grade { get; set; }

    }
}
