using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace UniversityDb_Infor.Domain
{
   public class Enrollment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdEnrollment { get; set; }

        public int IdStudent { get; set; }
        public int IdCourse { get; set; }

        public int?  Grade { get; set; }
        public Student Student { get; set; }
        public Course Course { get; set; }
        

    }
}
