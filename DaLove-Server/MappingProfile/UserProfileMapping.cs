using AutoMapper;
using DaLove_Server.Data.Domain;
using DaLove_Server.Data.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DaLove_Server.MappingProfile
{
    public class UserProfileMapping : Profile
    {
        public UserProfileMapping()
        {
            CreateMap<UserProfileCreateDto, UserProfile>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom<UserIdResolver>());

            CreateMap<UserProfile, UserProfileGetDto>();
                
        }
    }
}
