﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace UniversityDb_Infor.Domain
{
  public  class Course
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CourseId { get; set; }
        public string CourseTitle { get; set; }
        public int Credits { get; set; }

        public ICollection<Enrollment> Enrollments { get; set; }


    }
}
