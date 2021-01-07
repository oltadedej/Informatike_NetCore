using System;
using System.Collections.Generic;
using System.Text;

namespace UniversityDb_Infor.Domain
{
   public class ApplicationsAuthorized
    {
        public int idApplication { get; set; }
        public int idMethod { get; set; }
        public bool isAuthorized { get; set; }
    }
}
