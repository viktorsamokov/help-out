using AutoMapper;
using HMDI.Dtos;
using HMDI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HMDI.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
          CreateMap<ApplicationUser, LoggedInUser>();
        }
    }
}
