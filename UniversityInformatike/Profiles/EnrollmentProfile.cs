﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniversityDb_Infor.Domain;
using UniversityDb_Infor.Models;

namespace UniversityInformatike.Profiles
{
    public class EnrollmentProfile : Profile
    {
        public EnrollmentProfile()
        {
            CreateMap<Enrollment, EnrollmentModel>().ReverseMap();
        }
    }
}
