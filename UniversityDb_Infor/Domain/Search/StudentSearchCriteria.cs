using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversityDb_Infor.Models;

namespace UniversityDb_Infor.Domain.Search
{
 //  [SearchCriteriaAllowed]
  public  class StudentSearchCriteria
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }


        public IQueryable<Student> ApplySearchCriteriaFilter(IQueryable<Student> source)
        {
            source = source.Where((Student x) => (String.IsNullOrWhiteSpace(FirstName) || x.Emer.Equals(FirstName))
                                            && (String.IsNullOrWhiteSpace(LastName) || x.Mbiemer.Equals(LastName))

                     );

            return source;
        }
    }
}
