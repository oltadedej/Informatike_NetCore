using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace UniversityDb_Infor.Domain
{
  public  class Student
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
       public int StudentId { get; set; }
       public string Emer { get; set; }
       public String Mbiemer { get; set; }
       public DateTime EnrollmentDate { get; set;}

        public ICollection<Enrollment> Enrollments { get; set; }

    }
}
