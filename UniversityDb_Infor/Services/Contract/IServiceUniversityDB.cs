using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UniversityDb_Infor.Domain.Search;
using UniversityDb_Infor.Models;
using UniversityDb_Infor.Utils;

namespace UniversityDb_Infor.Services.Contract
{
  public  interface IServiceUniversityDB
    {
        Task<bool> DeleteStudentAsync(int id);
        Task<StudentModel> UpdateStudentAsync(int idPhoneBook, StudentModel item);
        Task<StudentModel> CreateStudentAsync(StudentModel item);
        Task<StudentModel> GetStudentAsync(int id);
        Task<IEnumerable<StudentModel>> SearchStudentsAsync(StudentSearchCriteria searchCriteria);
        Task<EnEntityExistsStatus> StudentExistsAsync(int Id);

    }
}
