using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniversityDB.Domain;
using UniversityDB.Models;

namespace UniversityNetCore.Profiles
{
    public class StudentProfile:Profile
    {
        public StudentProfile()
        {
            CreateMap<Student, StudentModel>()
                .ReverseMap();
        }
    }
}
