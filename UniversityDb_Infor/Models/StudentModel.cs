﻿using System;
using System.Collections.Generic;
using System.Text;

namespace UniversityDb_Infor.Models
{
   public class StudentModel
    {
        public int IdStudenti { get; set; }
        public string Emer { get; set; }
        public String Mbiemer { get; set; }
        public DateTime EnrollmentDate { get; set; }
    }
}