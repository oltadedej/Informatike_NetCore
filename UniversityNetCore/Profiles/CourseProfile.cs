using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniversityDB.Domain;
using UniversityDB.Models;

namespace UniversityNetCore.Profiles
{
    public class CourseProfile:Profile
    {
        public CourseProfile()
        {
            CreateMap<Course, CourseModel>()
           .ReverseMap();
        }
    }
}
